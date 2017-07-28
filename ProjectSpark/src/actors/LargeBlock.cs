using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSpark.assets;
using ProjectSpark.actors.blocks;
using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Input;

namespace ProjectSpark.actors
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in blocklist)
            {
                block.Draw(spriteBatch);
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

        public void Update(UltravioletTime time)
        {
            foreach (var block in blocklist)
            {
                block.Update(time);
            }
        }
    }
}
