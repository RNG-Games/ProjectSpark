using System;
using System.Collections;
using System.Collections.Generic;
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

namespace ProjectSpark
{
    public class Game : UltravioletApplication
    {
        public Game() 
            : base("Tide Productions", "Project Spark") 
        { }

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

        protected override void OnLoadingContent()
        {
            //TODO: Attach & Detach Window & Apply changes
            var gfx = Ultraviolet.GetGraphics();
            Ultraviolet.GetPlatform().Windows.GetPrimary().Caption = "Project Spark Test";
            Ultraviolet.GetPlatform().Windows.GetPrimary().ClientSize = new Size2(1280,720);
            _content = ContentManager.Create("Content");
            Resources.ContentManager = _content;
            tr = new TextRenderer();
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
        protected override void OnUpdating(UltravioletTime time)
        {
            _current = States.Peek();
            //TODO Update camera
            Resources.deltaTime = (float) time.ElapsedTime.TotalSeconds;
            _current.Update(time);

            base.OnUpdating(time);
        }
        
        protected override void OnDrawing(UltravioletTime time)
        {
            var gfx = Ultraviolet.GetGraphics();
            gfx.Clear(new Color(222, 206, 206));
            var X = new Vector2(1,1);
            Vector2f k = X;
            var scaleX = (float)1280/ 1920;
            var scaleY = (float)720 / 1080;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, matrix);
            _current.Draw(spriteBatch);

            var settings = new TextLayoutSettings(Trebuchet, null, null, TextFlags.Standard);
            tr.Draw(spriteBatch, "Hello, world!", new Vector2(100,100), Color.White, settings);

            spriteBatch.End();
            base.OnDrawing(time);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SafeDispose.DisposeRef(ref _content);
            }
            base.Dispose(disposing);
        }

        private ContentManager _content;
        private Stack<GameState> States = new Stack<GameState>();
        private GameState _current;
        private SpriteBatch spriteBatch;
        private SpriteFont Anonymous;
        private SpriteFont Rabelo;
        private SpriteFont Trebuchet;
        private TextRenderer tr;
    }
}