using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.input
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
            UpKey = CreateAction("UP_KEY");
            DownKey = CreateAction("DOWN_KEY");
            SelectKey = CreateAction("SELECT_KEY");
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
            UpKey.Primary = CreateKeyboardBinding(Key.Up);
            UpKey.Secondary = CreateKeyboardBinding(Key.W);
            DownKey.Primary = CreateKeyboardBinding(Key.Down);
            DownKey.Secondary = CreateKeyboardBinding(Key.S);
            Fullscreen.Primary = CreateKeyboardBinding(Key.F11);
            SelectKey.Primary = CreateKeyboardBinding(Key.Return);
            base.OnResetting();
        }

        public InputAction ExitApplication { get; private set; }
        public InputAction MoveLeft { get; private set; }
        public InputAction MoveRight { get; private set; }
        public InputAction ActionKey { get; private set; }
        public InputAction Fullscreen { get; private set; }
        public InputAction UpKey { get; private set; }
        public InputAction DownKey { get; private set; }
        public InputAction SelectKey { get; private set; }
    }
}
