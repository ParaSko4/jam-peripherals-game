namespace Jam.VirtualKeyboard.Keys
{
    public abstract class VirtualKey
    {
        public bool IsKeyBindedToPlayer { get; set; } = true;

        public void Execute()
        {
            if (IsKeyBindedToPlayer)
            {
                ExecuteKeyboardAction();
            }
            else
            {
                ExecuteBoardAction();
            }
        }

        public abstract void ExecuteBoardAction();
        public abstract void ExecuteKeyboardAction();
    }
}