using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _ProjectSpark.actors;
using _ProjectSpark.actors.blocks;


namespace _ProjectSpark.gamestates
{
	class MainState : GameState
	{
		Sprite testSprite = new Sprite(Resources.GetTexture("pixel.png"));
	    private Vertex[] line = {new Vertex(new Vector2f(50, 50)), new Vertex(new Vector2f(100,100)) };
	    private float _time = 0;
        Text text = new Text(){ Font = new Font(Resources.GetFont("trebuc.ttf"))};
        Player player = new Player();
        Block bl1 = new Regular(3, 3);
        LargeBlock bl2 = new LargeBlock("regular", 4, 9, 5, 10);

	    public override void Draw(RenderWindow _window)
		{
		    text.DisplayedString = "FALLING CIRCLE!!!!";
            text.Position = new Vector2f(200, 200);
            _window.Clear(new Color(0, 0, 0));
            testSprite.Scale = new Vector2f(64, 64);
			_window.Draw(testSprite);
            //_window.SetView(_view);
            _window.Draw(line,0,2, PrimitiveType.Lines);
            _window.Draw(text);
            player.Draw(_window);
            bl1.Draw(_window);
            bl2.Draw(_window);
		}

		public override void Update(float _deltaTime)
		{
		    _time += _deltaTime;
            _view.Zoom(1.00005f);
            if(Keyboard.IsKeyPressed(Keyboard.Key.V)) testSprite.Position += new Vector2f(2f, 2f);
            player.Update(_deltaTime);
            bl1.Update(_deltaTime);
            bl2.Update(_deltaTime);
		}
	    private View _view = new View(new Vector2f(0,0), new Vector2f(200, 200));
	}
}
