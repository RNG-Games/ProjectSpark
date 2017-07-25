using System;
using System.CodeDom;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _ProjectSpark.actors;
using _ProjectSpark.actors.blocks;
using _ProjectSpark.actors.enemies;
using _ProjectSpark.actors.lines;
using _ProjectSpark.util;
using System.Collections.Generic;
using System.Linq;
using _ProjectSpark.actors.textboxes;

namespace _ProjectSpark.gamestates
{
	class MainState : GameState
	{
	    private float _time = 0;
        Text text = new Text(){ Font = new Font(Resources.GetFont("trebuc.ttf"))};

        Player player = Player.getPlayer();    
        List<IActable> actors = new List<IActable>();

	    private List<Memento<IActable>> savePoint;
	    private bool existingCheckpoint = false;
	    private Vector2f checkpointCamera;

        //test area       
        public MainState()
        {
            // blocks
            string[] a = { "Hello $2World", "Goodbye World", "$1meow" };
            actors.Add(new Speech(new Vector2f(500, 500), a));
            actors.Add(new LargeBlock("regular", 0, 9, 0, 22));
            actors.Add(new LargeBlock("regular", 29, 39, 0, 22));
            actors.Add(new LargeBlock("regular", 10, 19, 20, 20));
            actors.Add(new LargeBlock("spike", 10, 19, 19, 19));

            actors.Add(new Trampoline(19, 34));
            actors.Add(new Trampoline(20, 34));
            actors.Add(new LargeBlock("regular", 0, 0, 23, 45));
            actors.Add(new LargeBlock("regular", 19, 20, 35, 45));
            actors.Add(new LargeBlock("regular", 17, 18, 31, 45));
            actors.Add(new LargeBlock("spike", 17, 18, 30, 30));
            // enemies

            // lines

            actors.Add(new Checkpoint(true, new Vector2f(10, 29), 1));
            actors.Add(new Transition(new Vector2f(9, 30), 20));



            
            //Finalize Setup
            foreach (var checkpoint in actors.Where(act => act is Checkpoint))
            {
                var x = (Checkpoint) checkpoint;
                x.setCheckpoint = SetCheckpoint;
            }
            player.Resawn = RestoreCheckpoint;
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

	    public void SetCheckpoint()
	    {
	        savePoint = new List<Memento<IActable>>();
	        foreach (var actor in actors)
	        {
	            savePoint.Add(actor.Save());
	        }
	        checkpointCamera = Program.Window.GetView().Center;
	        existingCheckpoint = true;
	    }

	    public void RestoreCheckpoint()
	    {
	        if (!existingCheckpoint)
	        {
	            //TODO Reload entire Stage
	            return;
	        }
	        else
	        {
	            actors.Clear();
	            foreach (var memento in savePoint)
	            {
	                actors.Add(memento.GetSavedState());
	            }
	            Program.Window.GetView().Center = checkpointCamera;
	        }
	    }
	}
}
