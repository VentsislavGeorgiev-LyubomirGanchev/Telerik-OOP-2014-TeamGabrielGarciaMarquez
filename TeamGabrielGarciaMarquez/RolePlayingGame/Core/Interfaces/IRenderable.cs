namespace RolePlayingGame.Core.Interfaces
{
    using System;
    using System.Drawing;

    interface IRenderable
    {
        /// <summary>
        /// Updates render object
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="elapsedTime"></param>
        void Update(double gameTime, double elapsedTime);

        /// <summary>
        /// Draws render object
        /// </summary>
        /// <param name="graphics"></param>
        void Draw(Graphics graphics);
    }
}