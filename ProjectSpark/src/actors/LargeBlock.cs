using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ProjectSpark.util;
using _ProjectSpark.actors.blocks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace _ProjectSpark.actors
{
    [Serializable]
    class LargeBlock : IActable
    {
        List<Block> blocklist = new List<Block>();

        public LargeBlock(String type, int leftmost, int rightmost, int topmost, int bottommost)
        {
            switch (type)
            {
                case "regular":
                    for (int i = leftmost; i <= rightmost; i++)
                    {
                        for (int j = topmost; j <= bottommost; j++)
                        {
                            blocklist.Add(new Regular(i, j));
                        }
                    }
                    break;
                case "spike":
                    for (int i = leftmost; i <= rightmost; i++)
                    {
                        for (int j = topmost; j <= bottommost; j++)
                        {
                            blocklist.Add(new Spike(i, j));
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Draw(RenderWindow _window)
        {
            foreach (var block in blocklist)
            {
                block.Draw(_window);
            }
        }

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public void Update(float _deltaTime)
        {
            foreach (var block in blocklist)
            {
                block.Update(_deltaTime);
            }
        }

        public virtual Memento<IActable> Save() => new Memento<IActable>(this);
    }
}
