using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;

namespace ProjectSpark.gamestates
{
    public abstract class GameState
    {
        public abstract void Update(UltravioletTime time);
        public abstract void Draw(SpriteBatch spriteBatch);
        public bool IsFinished { get; protected set; }
        public GameState NewState { get; set; }
    }
}
