using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSpark.actors;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using ProjectSpark.actors.enemies;
using ProjectSpark.actors.blocks;
using ProjectSpark.actors.lines;
using ProjectSpark.util;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text;
using ProjectSpark.glyphshaders;
using ProjectSpark.assets;

namespace ProjectSpark.gamestates
{
    class MainState : GameState
    {
        private TextRenderer tr;
        private SpriteFont Trebuchet;
        private string collected, death;
        private Player player;
        public MainState()
        {
            tr = new TextRenderer();
            tr.RegisterGlyphShader("shaky", new Shaky());
            tr.RegisterGlyphShader("rainbow", new Rainbow());
            tr.RegisterGlyphShader("wavy", new Wavy());
            Trebuchet = Resources.ContentManager.Load<SpriteFont>(GlobalFontID.TrebuchetMS16);

            //Add Stuff
            player = Player.getPlayer();
            Resources.actors = new List<IActable>();
            Resources.actorBuffer = new List<IActable>();
            Resources.actors.Add(new Trampoline(20, 10));
            Resources.actors.Add(new Decoration(20, 5, "deco"));
            Resources.actors.Add(new LargeBlock("spike", 0, 39, 20, 21));
            Resources.actors.Add(new Stationary(20, 11));
            Resources.actors.Add(new Vertical(22, 10, 12));
            Resources.actors.Add(new Horizontal(11, 21, 23));
            Resources.actors.Add(new Decoration(22, 10, "deco"));
            Resources.actors.Add(new Decoration(22, 12, "deco"));
            Resources.actors.Add(new Decoration(21, 11, "deco"));
            Resources.actors.Add(new Decoration(23, 11, "deco"));
            Resources.actors.Add(new Checkpoint(true, new Vector2f(10, 23), 3));
            Resources.actors.Add(new Transition(new Vector2f(10, 23), 19));
            string[] test = { "Hello, |shader:wavy||c:ffff0000|World!|c||shader| |shader:shaky||shader:wavy|meow|shader| I am|shader| a motherfucking cat yeah I am the coolest no doubt meeeeeeeeeow |shader:rainbow|Lesbian Gay Bisexual Trans Queer/Questioning Intersex Asexuality + pride!!!|shader|", "this is |c:ffff0000||shader:wavy|part 2|shader||c| of this message!!!! yaaaaaay" };
            //actors.Add(new Textbox(test , new Vector2f(500, 500)));
            Resources.actors.Add(new Npc(22, 3, test, "npc"));
            Resources.actors.Add(new Circular(20, 15, 2, 0, 3));
            Resources.actors.Add(new Circular(20, 15, 2, 90, 3));
            Resources.actors.Add(new Circular(20, 15, 2, 180, 3));
            Resources.actors.Add(new Circular(20, 15, 2, 270, 3));
            Resources.actors.Add(new Circular(20, 15, 3, 0, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 90, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 180, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 270, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 45, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 135, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 225, -2));
            Resources.actors.Add(new Circular(20, 15, 3, 315, -2));

            Vector2f[] verteces = { new Vector2f(10, 10), new Vector2f(14, 11), new Vector2f(9, 19)};
            Resources.actors.Add(new Decoration(10, 10, "deco"));
            Resources.actors.Add(new Decoration(14, 11, "deco"));
            Resources.actors.Add(new Decoration(9, 19, "deco"));
            Resources.actors.Add(new Blockwise(verteces, 0));

            Resources.actors.Add(new Jumping(30, 15, -700));
            Resources.actors.Add(new Regular(30, 16));
            Resources.actors.Add(new Collectible(30, 10, 0));
        }

        public override void Update(UltravioletTime time)
        {
            Resources.actors = Resources.actors.Where(a => !a.IsExpired()).ToList();
            foreach (var actor in Resources.actors)
            {
                actor.Update(time);
            }

            Resources.actors.AddRange(Resources.actorBuffer);
            Resources.actorBuffer.Clear();
            death = $"Deaths: {Resources.deaths}";
            collected = $"Collected: {Resources.collectables}";

            player.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var actor in Resources.actors)
            {
                actor.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);

            var settings = new TextLayoutSettings(Trebuchet, null, null, TextFlags.Standard);
            tr.Draw(spriteBatch, collected, new Vector2f(10,10), Color.Black, settings);
            tr.Draw(spriteBatch, death, new Vector2f(10, 30), Color.Black, settings);
        }
    }
}
