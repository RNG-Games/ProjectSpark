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

namespace ProjectSpark.gamestates
{
    class MainState : GameState
    {

        private List<IActable> actors = new List<IActable>();
        private Player player;
        public MainState()
        {
            //Add Stuff
            player = Player.getPlayer();

            actors.Add(new Trampoline(20, 10));
            actors.Add(new Decoration(20, 5, "deco"));
            actors.Add(new LargeBlock("spike", 0, 39, 20, 21));
            actors.Add(new Stationary(20, 11));
            actors.Add(new Vertical(22, 10, 12));
            actors.Add(new Horizontal(11, 21, 23));
            actors.Add(new Decoration(22, 10, "deco"));
            actors.Add(new Decoration(22, 12, "deco"));
            actors.Add(new Decoration(21, 11, "deco"));
            actors.Add(new Decoration(23, 11, "deco"));
            actors.Add(new Checkpoint(true, new Vector2f(10, 23), 3));
        }

        public override void Update(UltravioletTime time)
        {
            actors = actors.Where(a => !a.IsExpired()).ToList();
            foreach (var actor in actors)
            {
                actor.Update(time);
            }
            player.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var actor in actors)
            {
                actor.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
        }
    }
}
