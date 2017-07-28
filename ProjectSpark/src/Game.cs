using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProjectSpark.assets;
using ProjectSpark.gamestates;
using ProjectSpark.Input;
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

            States.Push(new MainState());
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
        protected override void OnUpdating(UltravioletTime time)
        {
            _current = States.Peek();
            UpdateCamera(time);
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
            var X = new Vector2(1, 1);
            Vector2f k = X;
            var scaleX = (float)1280 / 1920;
            var scaleY = (float)720 / 1080;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, matrix);
            _current.Draw(spriteBatch);

            var settings = new TextLayoutSettings(Trebuchet, null, null, TextFlags.Standard);
            tr.Draw(spriteBatch, "|shader:wavy|Hallo Welt ich teste gerade |shader:shaky|glyph shaders|shader| !!!|shader|", new Vector2(100,100), Color.White, settings);
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
            cameraSpeed = (endposition - Resources.gfx.GetViewport().Y)/time;
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
            var vp = Resources.gfx.GetViewport();
            if (moveCamera)
            {
                if (Resources.gfx.GetViewport().Y - cameraTargetPosition >= 0)
                {
                    CameraMovementDone();
                    return;
                }
                Resources.gfx.SetViewport(new Viewport(vp.X, vp.Y + (int)(cameraSpeed * time.ElapsedTime.TotalSeconds), vp.Width, vp.Height));
            }
        }

        private static void CameraMovementDone()
        {
            cameraSpeed = 0f;
            moveCamera = false;
            recall?.Invoke();
            recall = null;
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
        #endregion

    }
}