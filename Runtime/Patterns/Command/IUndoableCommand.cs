namespace HoangTuDongAnh.UP.Common.Patterns.Command
{
    /// <summary>
    /// Undoable command: supports undo.
    /// </summary>
    public interface IUndoableCommand : ICommand
    {
        void Undo();
    }
}