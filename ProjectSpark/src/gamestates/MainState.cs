using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSpark.actors;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;

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
