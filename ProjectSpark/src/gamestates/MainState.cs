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
            actors.Add(new Trampoline(25, 14));
            actors.Add(new Regular(26, 7));

            // enemies

            // lines

            actors.Add(new Checkpoint(true, new Vector2f(9, 30), 1));
            actors.Add(new Transition(new Vector2f(9, 30), 20));

            //Finalize Setup
            //foreach (var checkpoint in actors.Where(a => a is /*CheckPoint Class*/))
            {
                //TODO wichtig für checkpoint
                //
                //Die checkpoint Klasse sollte ne 'public Action setCheckpoint' besitzen, die gecallt wird, wenn der Player mit dem Checkpoint interagiert
                //Und da wir faul sind/ nicht irgendeinen schrott durchreichen wollen, machen wir das so:
                //wenn gemacht, nächste Zeile auskommentoeren
                //checkpoint.createSave = SetCheckpoint
            }
            //TODO gleichermaßen sollte der Player bei Tod die RestoreCheckpoint ausführen
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
