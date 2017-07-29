using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProjectSpark.assets;
using ProjectSpark.gamestates;
using ProjectSpark.util;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text;
using TwistedLogik.Ultraviolet.OpenGL;
using TwistedLogik.Ultraviolet.OpenGL.Graphics;
using TwistedLogik.Ultraviolet.Platform;
using ProjectSpark.glyphshaders;
using ProjectSpark.input;

namespace ProjectSpark
{
    public class Game : UltravioletApplication
    {
        public Game() 
            : base("Tide Productions", "Project Spark") 
        { }

        #region Don't touch
        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            var configuration = new OpenGLUltravioletConfiguration();
            PopulateConfiguration(configuration);

#if DEBUG
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) =>
            {
                System.Diagnostics.Debug.WriteLine(message);
            };
#endif
            configuration.WindowIsResizable = false;
            return new OpenGLUltravioletContext(this, configuration);
        }

        #endregion

        #region Loading

        protected override void OnLoadingContent()
        {
            //TODO: Attach & Detach Window & Apply changes
            var gfx = Ultraviolet.GetGraphics();
            Ultraviolet.GetPlatform().Windows.GetPrimary().Caption = "Project Spark Test";
            Ultraviolet.GetPlatform().Windows.GetPrimary().ClientSize = new Size2(1280, 720);
            camera = new Camera();
            _content = ContentManager.Create("Content");
            Resources.ContentManager = _content;
            LoadInputBindings();
            Resources.Input = Ultraviolet.GetInput();
            Resources.gfx = Ultraviolet.GetGraphics();
            tr = new TextRenderer();
            tr.RegisterGlyphShader("shaky", new Shaky());
            tr.RegisterGlyphShader("wavy", new Wavy());
            spriteBatch = SpriteBatch.Create();

            LoadContentManifests();
            base.OnLoadingContent();

            Anonymous = _content.Load<SpriteFont>(GlobalFontID.Anonymous16);
            Rabelo = _content.Load<SpriteFont>(GlobalFontID.Rabelo16);
            Trebuchet = _content.Load<SpriteFont>(GlobalFontID.TrebuchetMS16);

            States.Push(new MainMenuState());
        }

        protected void LoadContentManifests()
        {
            var uvContent = Ultraviolet.GetContent();

            var contentManifestFiles = _content.GetAssetFilePathsInDirectory("Manifests");

            uvContent.Manifests.Load(contentManifestFiles);
            uvContent.Manifests["Global"]["Sprites"].PopulateAssetLibrary(typeof(GlobalSpriteID));
            uvContent.Manifests["Global"]["Fonts"].PopulateAssetLibrary(typeof(GlobalFontID));
        }

        protected void LoadInputBindings()
        {
            var inputBindingsPath = Path.Combine(GetRoamingApplicationSettingsDirectory(), "InputBindings.xml");
            Ultraviolet.GetInput().GetActions().Load(inputBindingsPath, throwIfNotFound: false);
        }

        #endregion

        #region Update & Render

        private bool fullscreen;
        protected override void OnUpdating(UltravioletTime time)
        {
            _current = States.Peek();
            if (_current.IsFinished)
                States.Pop();
            if (_current.NewState != null)
            {
                States.Push(_current.NewState);
                _current.NewState = null;
            }
            if (States.Count == 0)
            {
                Exit();
                return;
            }

            _current = States.Peek();

            UpdateCamera(time);
            if (Resources.Input.GetActions().Fullscreen.IsPressed() && !fullscreen)
            {
                Ultraviolet.GetPlatform().Windows.GetPrimary().SetWindowMode(WindowMode.Fullscreen);
                fullscreen = true;
            }
            else if (Resources.Input.GetActions().Fullscreen.IsPressed() && fullscreen)
            {
                Ultraviolet.GetPlatform().Windows.GetPrimary().SetWindowMode(WindowMode.Windowed);
                fullscreen = false;
            }
            screenScale = camera.GetScreenScale(Ultraviolet.GetPlatform(), 1920, 1080);

            Resources.deltaTime = (float)time.ElapsedTime.TotalSeconds;

            if (Resources.Input.GetActions().ExitApplication.IsPressed())
            {
                Exit();
            }
            _current.Update(time);

            base.OnUpdating(time);
        }

        protected override void OnDrawing(UltravioletTime time)
        {
            var gfx = Ultraviolet.GetGraphics();
            gfx.Clear(new Color(222, 206, 206));
            var viewMatrix = camera.GetTransform();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, viewMatrix * Matrix.CreateScale(screenScale));
            _current.Draw(spriteBatch);
            var settings = new TextLayoutSettings(Trebuchet, null, null, TextFlags.Standard);
            tr.Draw(spriteBatch, "|shader:wavy|Hallo Welt ich teste gerade |shader:shaky|glyph shaders|shader| !!!", new Vector2(100,100), Color.White, settings);
            spriteBatch.End();
            base.OnDrawing(time);
        }
        #endregion

        #region End
        protected override void OnShutdown()
        {
            SaveInputBindings();

            base.OnShutdown();
        }

        protected void SaveInputBindings()
        {
            var inputBindingsPath = Path.Combine(GetRoamingApplicationSettingsDirectory(), "InputBindings.xml");
            Ultraviolet.GetInput().GetActions().Save(inputBindingsPath);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SafeDispose.DisposeRef(ref _content);
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Camera

        private static Action recall = null;
        private static bool moveCamera = false;
        private static float cameraTargetPosition;
        private static float cameraSpeed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endposition"></param>
        /// <param name="time">in seconds!!!</param>
        public static void MoveCameraDown(float endposition, float time)
        {
            if (moveCamera)
                throw new InvalidOperationException();
            cameraTargetPosition = endposition;
            cameraSpeed = -(endposition - camera.Position.Y) /time;
            Console.WriteLine(cameraSpeed);
            moveCamera = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endposition"></param>
        /// <param name="time">in seconds!!!</param>
        /// <param name="release"></param>
        public static void MoveCameraDown(float endposition, float time, Action release)
        {
            MoveCameraDown(endposition, time);
            recall = release;
        }

        public void UpdateCamera(UltravioletTime time)
        {
            if (moveCamera)
            {
                if (-camera.Position.Y >= cameraTargetPosition*screenScale.X)
                {
                    CameraMovementDone();
                }
                else
                {
                    camera.Move(new Vector2f(0, cameraSpeed * (float)time.ElapsedTime.TotalSeconds));
                }
            }
        }

        private static void CameraMovementDone()
        {
            cameraSpeed = 0f;
            moveCamera = false;
            recall?.Invoke();
            recall = null;
            Console.WriteLine("Done");
        }

        #endregion

        #region Attributes
        private ContentManager _content;
        private Stack<GameState> States = new Stack<GameState>();
        private GameState _current;
        private SpriteBatch spriteBatch;
        private SpriteFont Anonymous;
        private SpriteFont Rabelo;
        private SpriteFont Trebuchet;
        private TextRenderer tr;
        private static Camera camera;
        private Vector3 screenScale;

        #endregion

    }
}