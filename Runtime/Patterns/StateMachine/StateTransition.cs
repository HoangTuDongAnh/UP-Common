using System;

namespace HoangTuDongAnh.UP.Common.Patterns.StateMachine
{
    /// <summary>
    /// Conditional transition (From -> To).
    /// From == null means Any-State transition.
    /// </summary>
    public sealed class StateTransition<TContext>
    {
        public readonly IState<TContext> From;
        public readonly IState<TContext> To;
        public readonly Func<TContext, bool> Condition;
        public readonly int Priority; // higher first

        public StateTransition(IState<TContext> from, IState<TContext> to, Func<TContext, bool> condition, int priority = 0)
        {
            From = from;
            To = to;
            Condition = condition;
            Priority = priority;
        }
    }
}