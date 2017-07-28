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

            base.OnCreatingActions();
        }

        protected override void OnResetting()
        {
            ExitApplication.Primary = CreateKeyboardBinding(Key.Escape);
            MoveLeft.Primary = CreateKeyboardBinding(Key.Left);
            MoveLeft.Secondary = CreateKeyboardBinding(Key.A);
            MoveRight.Primary = CreateKeyboardBinding(Key.Right);
            MoveRight.Secondary = CreateKeyboardBinding(Key.D);

            base.OnResetting();
        }

        public InputAction ExitApplication { get; private set; }
        public InputAction MoveLeft { get; private set; }
        public InputAction MoveRight { get; private set; }
    }
}
