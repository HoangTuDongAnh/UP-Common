using System;
using System.Collections.Generic;

namespace HoangTuDongAnh.UP.Common.Patterns.StateMachine
{
    /// <summary>
    /// Typed state machine with transitions.
    /// - Any-state transitions
    /// - Priority-based transitions
    /// - Timed transitions (StateTime)
    /// - Optional registry (key -> state)
    /// </summary>
    public sealed class StateMachine<TContext>
    {
        public IState<TContext> CurrentState { get; private set; }
        public IState<TContext> PreviousState { get; private set; }

        /// <summary>
        /// Seconds since current state was entered.
        /// </summary>
        public float StateTime => _stateTime;

        /// <summary>
        /// Fired after state changed. (from, to)
        /// </summary>
        public event Action<IState<TContext>, IState<TContext>> OnStateChanged;

        /// <summary>
        /// Optional debug log.
        /// </summary>
        public bool EnableDebugLog { get; set; } = false;

        /// <summary>
        /// Optional tag used in debug log.
        /// </summary>
        public string DebugTag { get; set; } = "StateMachine";

        private readonly TContext _ctx;

        private readonly Dictionary<IState<TContext>, List<StateTransition<TContext>>> _transitions = new();
        private readonly List<StateTransition<TContext>> _anyTransitions = new();
        private readonly List<StateTransition<TContext>> _cached = new(16);

        // Optional state registry (key -> state)
        private readonly Dictionary<string, IState<TContext>> _stateRegistry = new();

        private bool _isTransitioning;
        private float _stateTime;

        public StateMachine(TContext context)
        {
            _ctx = context;
        }

        // -------------------------
        // Registry (optional)
        // -------------------------

        /// <summary>
        /// Register a state by key (optional helper).
        /// </summary>
        public void RegisterState(string key, IState<TContext> state)
        {
            if (string.IsNullOrEmpty(key) || state == null) return;
            _stateRegistry[key] = state;
        }

        /// <summary>
        /// Change state by key (optional helper).
        /// </summary>
        public bool ChangeState(string key, bool forceReenter = false)
        {
            if (!_stateRegistry.TryGetValue(key, out var state)) return false;
            return ChangeState(state, forceReenter);
        }

        public bool TryGetState(string key, out IState<TContext> state)
        {
            return _stateRegistry.TryGetValue(key, out state);
        }

        // -------------------------
        // Setup
        // -------------------------

        /// <summary>
        /// Set initial state (calls OnEnter).
        /// </summary>
        public void Initialize(IState<TContext> initialState, bool forceReenter = true)
        {
            PreviousState = null;
            CurrentState = null;
            _stateTime = 0f;

            ChangeState(initialState, forceReenter);
        }

        // -------------------------
        // Transitions
        // -------------------------

        /// <summary>
        /// Add a transition from a specific state.
        /// </summary>
        public void AddTransition(IState<TContext> from, IState<TContext> to, Func<TContext, bool> condition, int priority = 0)
        {
            if (from == null || to == null || condition == null) return;

            if (!_transitions.TryGetValue(from, out var list))
            {
                list = new List<StateTransition<TContext>>(4);
                _transitions.Add(from, list);
            }

            list.Add(new StateTransition<TContext>(from, to, condition, priority));
            list.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }

        /// <summary>
        /// Add a transition that can trigger from any state.
        /// </summary>
        public void AddAnyTransition(IState<TContext> to, Func<TContext, bool> condition, int priority = 0)
        {
            if (to == null || condition == null) return;

            _anyTransitions.Add(new StateTransition<TContext>(from: null, to: to, condition: condition, priority: priority));
            _anyTransitions.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }

        /// <summary>
        /// Transition after current state has been active for N seconds.
        /// </summary>
        public void AddTimedTransition(IState<TContext> from, IState<TContext> to, float seconds, int priority = 0)
        {
            if (from == null || to == null) return;
            if (seconds <= 0f) return;

            AddTransition(from, to, ctx => StateTime >= seconds, priority);
        }

        // -------------------------
        // State changes
        // -------------------------

        /// <summary>
        /// Change to a new state.
        /// </summary>
        public bool ChangeState(IState<TContext> newState, bool forceReenter = false)
        {
            if (newState == null) return ClearState();
            if (!forceReenter && ReferenceEquals(newState, CurrentState)) return false;
            if (_isTransitioning) return false;

            _isTransitioning = true;

            var from = CurrentState;

            if (CurrentState != null)
            {
                CurrentState.OnExit(_ctx);
                PreviousState = CurrentState;
            }

            CurrentState = newState;
            _stateTime = 0f;

            CurrentState.OnEnter(_ctx);

            _isTransitioning = false;

            if (EnableDebugLog) LogChange(from, CurrentState);
            OnStateChanged?.Invoke(from, CurrentState);

            return true;
        }

        /// <summary>
        /// Return to previous state (if any).
        /// </summary>
        public bool RevertToPrevious(bool forceReenter = false)
        {
            if (PreviousState == null) return false;
            return ChangeState(PreviousState, forceReenter);
        }

        /// <summary>
        /// Clear current state (calls OnExit).
        /// </summary>
        public bool ClearState()
        {
            if (CurrentState == null) return false;
            if (_isTransitioning) return false;

            _isTransitioning = true;

            var from = CurrentState;
            CurrentState.OnExit(_ctx);

            PreviousState = from;
            CurrentState = null;
            _stateTime = 0f;

            _isTransitioning = false;

            if (EnableDebugLog) LogChange(from, null);
            OnStateChanged?.Invoke(from, null);

            return true;
        }

        // -------------------------
        // Update loops
        // -------------------------

        /// <summary>
        /// Call in MonoBehaviour.Update().
        /// Evaluates transitions first, then ticks current state.
        /// </summary>
        public void Tick()
        {
#if UNITY_5_3_OR_NEWER
            _stateTime += UnityEngine.Time.deltaTime;
#endif
            if (TryEvaluateTransitions())
                return;

            CurrentState?.Tick(_ctx);
        }

        /// <summary>
        /// Call in MonoBehaviour.FixedUpdate().
        /// </summary>
        public void FixedTick()
        {
            CurrentState?.FixedTick(_ctx);
        }

        /// <summary>
        /// Call in MonoBehaviour.LateUpdate().
        /// </summary>
        public void LateTick()
        {
            CurrentState?.LateTick(_ctx);
        }

        // -------------------------
        // Internal
        // -------------------------

        private bool TryEvaluateTransitions()
        {
            if (CurrentState == null) return false;
            if (_isTransitioning) return false;

            _cached.Clear();

            // Any-state transitions first
            _cached.AddRange(_anyTransitions);

            // Then transitions for current state
            if (_transitions.TryGetValue(CurrentState, out var list))
                _cached.AddRange(list);

            // Scan in order (priority is already sorted within lists)
            for (int i = 0; i < _cached.Count; i++)
            {
                var t = _cached[i];
                if (t?.To == null || t.Condition == null) continue;

                if (t.Condition(_ctx))
                {
                    ChangeState(t.To);
                    return true;
                }
            }

            return false;
        }

        private void LogChange(IState<TContext> from, IState<TContext> to)
        {
#if UNITY_5_3_OR_NEWER
            UnityEngine.Debug.Log($"[{DebugTag}] {StateName(from)} -> {StateName(to)}");
#endif
        }

        private static string StateName(IState<TContext> s)
        {
            if (s == null) return "None";
            if (s is IStateInfo info && !string.IsNullOrEmpty(info.StateId)) return info.StateId;
            return s.GetType().Name;
        }
    }
}
