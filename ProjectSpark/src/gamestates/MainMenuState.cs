using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using ProjectSpark.glyphshaders;
using ProjectSpark.input;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Audio;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text;

namespace ProjectSpark.gamestates
{
    public enum MMenuHighligh
    {
        Play = 0,
        Options = 1,
        Exit = 2
    }

    public class MainMenuState : GameState
    {
        private MMenuHighligh selected;
        private TextRenderer textRenderer;
        private SpriteFont spriteFont;
        private Sprite selector;
        private Vector2f position;

        private Song rainy;
        private SongPlayer songPlayer;

        public MainMenuState()
        {
            selected = 0;
            textRenderer = new TextRenderer();
            textRenderer.RegisterGlyphShader("shaky", new Shaky());
            spriteFont = Resources.ContentManager.Load<SpriteFont>(GlobalFontID.TrebuchetMS32);
            selector = Resources.ContentManager.Load<Sprite>(GlobalSpriteID.selectorplaceholder);
            position = new Vector2f(50,50);
            rainy = Resources.ContentManager.Load<Song>(GlobalSongID.RainyNostalgia);
            songPlayer = SongPlayer.Create();
            songPlayer.Play(rainy);
            songPlayer.IsLooping = true;
        }

        public override void Update(UltravioletTime time)
        {
            songPlayer.Update(time);
            var move = 0;
            if(Resources.Input.GetActions().SelectKey.IsPressed())
                Select();
            if (Resources.Input.GetActions().DownKey.IsPressed())
                move++;
            if (Resources.Input.GetActions().UpKey.IsPressed())
                move--;

            if (move < 0 && selected == MMenuHighligh.Play)
                move = 0;
            if (move > 0 && selected == MMenuHighligh.Exit)
                move = 0;
            selected += move;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Resources.gfx.Clear(new Color(0,0,0));
            var settings = new TextLayoutSettings(spriteFont, null, null, TextFlags.Standard, SpriteFontStyle.Bold);
            textRenderer.Draw(spriteBatch, GetString(MMenuHighligh.Play), new Vector2(1550, 700),
                selected == MMenuHighligh.Play ? Color.White : Color.Gray, settings);
            textRenderer.Draw(spriteBatch, GetString(MMenuHighligh.Options), new Vector2(1550, 800),
                selected == MMenuHighligh.Options ? Color.White : Color.Gray, settings);
            textRenderer.Draw(spriteBatch, GetString(MMenuHighligh.Exit), new Vector2(1550, 900),
                selected == MMenuHighligh.Exit ? Color.White : Color.Gray, settings);
            spriteBatch.DrawScaledSprite(selector["selectorplaceholder"].Controller, GetPosition(), new Vector2(1,1));
        }

        private string GetString(MMenuHighligh target)
        {
            string temp;

            switch (target)
            {
                case MMenuHighligh.Play:
                    temp = "PLAY";
                    break;
                case MMenuHighligh.Options:
                    temp = "OPTIONS";
                    break;
                case MMenuHighligh.Exit:
                    temp = "EXIT";
                    break;
                default:
                    temp = "WTF";
                    break;
            }
            if (target == selected)
                temp = "|shader:shaky|" + temp + "|shader|";
            return temp;
        }

        private Vector2f GetPosition()
        {
            switch (selected)
            {
                case MMenuHighligh.Play:
                    return new Vector2f(1500, 711);
                case MMenuHighligh.Options:
                    return new Vector2f(1500, 811);
                case MMenuHighligh.Exit:
                    return new Vector2f(1500, 911);
                default:
                    return new Vector2f(1500,911);
            }
        }

        private void Select()
        {
            switch (selected)
            {
                case MMenuHighligh.Play:
                    IsFinished = true;
                    NewState = new MainState();
                    break;
                case MMenuHighligh.Options:
                    break;
                case MMenuHighligh.Exit:
                    IsFinished = true;
                    break;
            }
            if(selected != MMenuHighligh.Options)
                songPlayer.Stop();
        }
    }
}
