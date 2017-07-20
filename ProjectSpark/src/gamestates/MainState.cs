using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _ProjectSpark.actors;
using _ProjectSpark.actors.blocks;
using _ProjectSpark.actors.enemies;

namespace _ProjectSpark.gamestates
{
	class MainState : GameState
	{
		//Sprite testSprite = new Sprite(Resources.GetTexture("pixel.png"));
	    //private Vertex[] line = {new Vertex(new Vector2f(50, 50)), new Vertex(new Vector2f(100,100)) };
	    private float _time = 0;
        Text text = new Text(){ Font = new Font(Resources.GetFont("trebuc.ttf"))};
        Player player = Player.getPlayer();
        Block bl1 = new Regular(15, 15);
        LargeBlock bl2 = new LargeBlock("regular", 4, 9, 5, 10);
        LargeBlock bl3 = new LargeBlock("spike", 4, 9, 4, 4);
        Block bl4 = new Spike(15, 14);
        Line test = new Line(new Vector2f(9, 30), 20);
        Block bl5 = new Trampoline(25, 14);
        Enemy e1 = new Stationary(26, 14);


	    public override void Draw(RenderWindow _window)
		{
		    text.DisplayedString = "FALLING CIRCLE!!!!";
            text.Position = new Vector2f(200, 200);
            _window.Clear(new Color(0, 0, 0));
            //testSprite.Scale = new Vector2f(64, 64);
			//_window.Draw(testSprite);
            //_window.Draw(line,0,2, PrimitiveType.Lines);
            _window.Draw(text);
            bl1.Draw(_window);
            bl2.Draw(_window);
            bl3.Draw(_window);
            bl4.Draw(_window);
            bl5.Draw(_window);
            e1.Draw(_window);
            player.Draw(_window);
            test.Draw(_window);
        }

		public override void Update(float _deltaTime)
		{
		    _time += _deltaTime;
            //if(Keyboard.IsKeyPressed(Keyboard.Key.V)) testSprite.Position += new Vector2f(2f, 2f);
            bl1.Update(_deltaTime);
            bl2.Update(_deltaTime);
            bl3.Update(_deltaTime);
            bl4.Update(_deltaTime);
            bl5.Update(_deltaTime);
            e1.Update(_deltaTime);
            test.Update(_deltaTime);
            player.Update(_deltaTime);
        }

        public override void KeyPressed(object sender, KeyEventArgs e)
	    {
	    }
	}
}
