using SFML.Graphics;
using SFML.Window;

namespace _ProjectSpark.gamestates
{
	abstract class GameState
	{
		public abstract void Draw(RenderWindow _window);
		public abstract void Update(float _deltaTime);

		public virtual void KeyPressed(object sender, KeyEventArgs e) { }

		public bool IsFinished { get; protected set; }
		public GameState NewState { get; set; }
	}
}
