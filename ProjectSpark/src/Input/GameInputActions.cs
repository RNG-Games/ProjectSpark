using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.Input
{
    public class GameInputActions : InputActionCollection
    {
        public GameInputActions(UltravioletContext uv) : base(uv) { }

        protected override void OnCreatingActions()
        {
            ExitApplication = CreateAction("EXIT_APPLICATION");
            MoveLeft = CreateAction("MOVE_LEFT");
            MoveRight = CreateAction("MOVE_RIGHT");
            ActionKey = CreateAction("ACTION_KEY");
            Fullscreen = CreateAction("TOGGLE_FULLSCREEN");
            base.OnCreatingActions();
        }

        protected override void OnResetting()
        {
            ExitApplication.Primary = CreateKeyboardBinding(Key.Escape);
            MoveLeft.Primary = CreateKeyboardBinding(Key.Left);
            MoveLeft.Secondary = CreateKeyboardBinding(Key.A);
            MoveRight.Primary = CreateKeyboardBinding(Key.Right);
            MoveRight.Secondary = CreateKeyboardBinding(Key.D);
            ActionKey.Primary = CreateKeyboardBinding(Key.Space);
            Fullscreen.Primary = CreateKeyboardBinding(Key.F11);
            base.OnResetting();
        }

        public InputAction ExitApplication { get; private set; }
        public InputAction MoveLeft { get; private set; }
        public InputAction MoveRight { get; private set; }
        public InputAction ActionKey { get; private set; }
        public InputAction Fullscreen { get; private set; }
    }
}
