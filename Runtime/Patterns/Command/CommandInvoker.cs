using System.Collections.Generic;
using HoangTuDongAnh.UP.Common.Patterns.Singleton;

namespace HoangTuDongAnh.UP.Common.Patterns.Command
{
    /// <summary>
    /// Executes commands and keeps history for Undo/Redo.
    /// </summary>
    public sealed class CommandInvoker : Singleton<CommandInvoker>
    {
        private readonly Stack<IUndoableCommand> _undoStack = new();
        private readonly Stack<IUndoableCommand> _redoStack = new();

        /// <summary>
        /// Max undo history. Set <= 0 for unlimited.
        /// </summary>
        public int MaxHistory { get; set; } = 100;

        public int UndoCount => _undoStack.Count;
        public int RedoCount => _redoStack.Count;

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        /// <summary>
        /// Execute a command.
        /// If it's undoable, it will be stored in history.
        /// </summary>
        public void Execute(ICommand command)
        {
            if (command == null) return;

            command.Execute();

            // New action invalidates redo branch.
            _redoStack.Clear();

            // Store only undoable commands.
            if (command is IUndoableCommand undoable)
            {
                _undoStack.Push(undoable);
                TrimUndoHistoryIfNeeded();
            }
        }

        /// <summary>
        /// Undo the last command (if any).
        /// </summary>
        public void Undo()
        {
            if (!CanUndo) return;

            var cmd = _undoStack.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);
        }

        /// <summary>
        /// Redo the last undone command (if any).
        /// </summary>
        public void Redo()
        {
            if (!CanRedo) return;

            var cmd = _redoStack.Pop();
            cmd.Execute();
            _undoStack.Push(cmd);

            TrimUndoHistoryIfNeeded();
        }

        /// <summary>
        /// Clear Undo/Redo history (useful on new scene/game reset).
        /// </summary>
        public void ClearHistory()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        // Keep history bounded to avoid unbounded memory growth.
        private void TrimUndoHistoryIfNeeded()
        {
            if (MaxHistory <= 0) return;
            if (_undoStack.Count <= MaxHistory) return;

            // Stack cannot remove bottom directly -> rebuild.
            var temp = new List<IUndoableCommand>(_undoStack);
            // temp is top->bottom order (enumeration of Stack is LIFO)
            // Keep newest MaxHistory items => first MaxHistory entries of temp.
            temp.RemoveRange(MaxHistory, temp.Count - MaxHistory);

            _undoStack.Clear();
            // Re-push in reverse to restore original order.
            for (int i = temp.Count - 1; i >= 0; i--)
                _undoStack.Push(temp[i]);
        }
    }
}
