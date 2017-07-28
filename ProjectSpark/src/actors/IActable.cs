using TwistedLogik.Ultraviolet;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;

namespace ProjectSpark.actors
{
    /// <summary>
    /// Required by actors
    /// </summary>
    public interface IActable
    {
        /// <summary>
        /// Drawing Stuff
        /// </summary>
        /// <param name="spriteBatch">spriteBatch - use it or not</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Update the actor
        /// </summary>
        /// <param name="time">Time of the game</param>
        void Update(UltravioletTime time);

        /// <summary>
        /// Starting time at which the update of the actor can start
        /// </summary>
        /// <returns>Starting time in seconds</returns>
        float StartTime();

        /// <summary>
        /// Useful for removing from List
        /// </summary>
        /// <returns>bool - true if it can be removed</returns>
        bool IsExpired();
    }
}
