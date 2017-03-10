using System;

namespace ShootingGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ShootingGame game = new ShootingGame())
            {
                game.Run();
            }
        }
    }
#endif
}

