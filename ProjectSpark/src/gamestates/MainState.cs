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
        private Player player;
        public MainState()
        {
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

            player.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var actor in Resources.actors)
            {
                actor.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
        }
    }
}
