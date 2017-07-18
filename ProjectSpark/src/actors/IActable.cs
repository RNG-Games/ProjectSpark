using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _ProjectSpark.actors
{
    /// <summary>
    /// Required by actors
    /// </summary>
    public interface IActable
    {
        /// <summary>
        /// Drawing Stuff
        /// </summary>
        /// <param name="_window">RenderWindow in which is drawn</param>
        void Draw(RenderWindow _window);

        /// <summary>
        /// Update the actor
        /// </summary>
        /// <param name="_deltaTime"></param>
        void Update(float _deltaTime);

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
