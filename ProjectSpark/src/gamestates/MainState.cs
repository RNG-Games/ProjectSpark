using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSpark.actors;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using ProjectSpark.actors.blocks;

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
