namespace Project.Common.Patterns
{
    public interface IState
    {
        bool IsActive { get; }

        void EnableState();

        void DisableState();
    }
}