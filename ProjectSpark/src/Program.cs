using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _ProjectSpark.gamestates;

namespace _ProjectSpark
{
    internal class Program
    {

        #region public Attributes

        public static RenderWindow Window;
        public static Stack<GameState> States = new Stack<GameState>();

        #if DEBUG
        public static bool Debug;
        #endif

        #endregion

        #region private Attributes

        private static readonly Dictionary<bool, RenderWindow> WindowCache = new Dictionary<bool, RenderWindow>();
        private static bool _isFullscreen;
        private static Clock _clock;
        private static float _deltaTime;
        private static float _gameTime;
        private static GameState _current;
        private static Text _text;

        #endregion

        #region Logic

        private static void Init()
        {
            _isFullscreen = Properties.Settings.Default.fullscreen;
            _clock = new Clock();
            _gameTime = 0f;
            _text = new Text("", Resources.GetFont("rabelo.ttf"));
            States.Push(new MainState());
        }

        public static void Main(string[] args)
        {
            Init();
            AttachWindow();

            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Update();
                Render();

                if (_current.IsFinished)
                    States.Pop();
                if (_current.NewState != null)
                {
                    States.Push(_current.NewState);
                    _current.NewState = null;
                }
                if(States.Count == 0)
                    Window.Close();
            }

            DetachWindow();
            foreach (var key in WindowCache.Keys.ToList())
            {
                WindowCache[key].Close();
                WindowCache[key].Dispose();
                WindowCache.Remove(key);
            }
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Update & Render

        private static void Update()
        {
            _deltaTime = _clock.ElapsedTime.AsSeconds();
            _gameTime += _deltaTime;
            _clock.Restart();
			_text.DisplayedString =$"{_gameTime:f2} s";
            _current = States.Peek();
            _current.Update(_deltaTime);
        }

        private static void Render()
        {
            Window.Clear(new Color(100, 149, 237));
            _current.Draw(Window);
            Window.Draw(_text);
            Window.SetView(new View(new Vector2f(960, 540), new Vector2f(1920, 1080)));
            Window.Display();
        }

        #endregion

        #region Window Handle

        public static void AttachWindow()
        {
            DetachWindow();
            if (!WindowCache.ContainsKey(_isFullscreen))
            {
                var window = new RenderWindow(
                    _isFullscreen ? VideoMode.DesktopMode : new VideoMode(1280, 720),
                    "MyApp",
                    _isFullscreen ? Styles.Fullscreen : Styles.Default/*,
                    new ContextSettings() { AntialiasingLevel = 16 }*/);

                /* Set window icon
                using (var image = Resources.GetImage("icon.png"))
                {
                    window.SetIcon(image.Size.X, image.Size.Y, image.Pixels);
                }
                */
                WindowCache[_isFullscreen] = window;
            }

            Window = WindowCache[_isFullscreen];
            Window.Closed += Window_OnClose;
            Window.KeyPressed += Window_OnKeyPressed;
            Window.SetKeyRepeatEnabled(false);
            Window.SetMouseCursorVisible(false);
            Window.SetVerticalSyncEnabled(true);
            Window.SetVisible(true);
            Window.SetActive(true);
            Window.RequestFocus();
        }

        public static void DetachWindow()
        {
            if (Window == null)
            {
                return;
            }
            Window.Closed -= Window_OnClose;
            Window.KeyPressed -= Window_OnKeyPressed;
            Window.SetVisible(false);
            Window = null;
        }

        #endregion

        #region Events

        private static void Window_OnClose(object sender, EventArgs e) { Window.Close(); }

        private static void Window_OnKeyPressed(object sender, KeyEventArgs e)
        {
			_current.KeyPressed(sender, e);
            #if DEBUG
            if (e.Code == Keyboard.Key.J)
                Debug = !Debug;
            #endif

            if (e.Code == Keyboard.Key.F11)
            {
                _isFullscreen = !_isFullscreen;
                Properties.Settings.Default.fullscreen = _isFullscreen;
                Properties.Settings.Default.Save();
                AttachWindow();
            }
        }

        #endregion

    }
}
