using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _ProjectSpark.actors;
using _ProjectSpark.actors.blocks;
using _ProjectSpark.actors.enemies;
using System.Collections.Generic;
using System.Linq;

namespace _ProjectSpark.gamestates
{
	class MainState : GameState
	{
	    private float _time = 0;
        Text text = new Text(){ Font = new Font(Resources.GetFont("trebuc.ttf"))};

        Player player = Player.getPlayer();    
        List<IActable> actors = new List<IActable>();

        //test area       
        public MainState()
        {
            // blocks
            actors.Add(new Regular(15, 15));
            actors.Add(new LargeBlock("regular", 4, 9, 5, 10));
            actors.Add(new LargeBlock("spike", 4, 9, 4, 4));
            actors.Add(new Spike(15, 14));
            actors.Add(new Trampoline(25, 14));
            actors.Add(new Decoration(30, 14, "testdeco.png"));
            actors.Add(new Decoration(28, 14, "testdeco.png"));
            actors.Add(new Decoration(29, 13, "testdeco.png"));
            actors.Add(new Decoration(29, 15, "testdeco.png"));

            // enemies
            actors.Add(new Horizontal(14, 28, 30));
            actors.Add(new Vertical(29, 13, 15));
            // lines
            actors.Add(new Line(new Vector2f(9, 30), 20));
        }

        public override void Draw(RenderWindow _window) {
		    text.DisplayedString = "FALLING CIRCLE!!!!";
            text.Position = new Vector2f(200, 200);
            _window.Clear(new Color(222, 206, 206));			


            _window.Draw(text);

            foreach (var actor in actors)
                actor.Draw(_window);
            player.Draw(_window);
        }

		public override void Update(float _deltaTime)
		{
		    _time += _deltaTime;
            actors = actors.Where(a => !a.IsExpired()).ToList();
            foreach (var actor in actors)
                actor.Update(_deltaTime);

            player.Update(_deltaTime);
        }

        public override void KeyPressed(object sender, KeyEventArgs e)
	    {
	    }
	}
}
