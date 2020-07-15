using System;
using System.Timers;

namespace Savanna
{
    public class GameLoop
    {
        /// <summary>
        /// Creates game timer and loops each second meanwhile capturing user input
        /// Each loop iterates game and prints game in the console
        /// </summary>
        public void StartTimer(Game.GameEngine game)
        {
            Timer timer = new Timer(1000);

            timer.Elapsed += (sender, e) => LoopGame(game);
            timer.Start();
            Console.Clear();
            game.PrintField();
            UserInput input = new UserInput();
            input.AddAnimals(game);
            Console.ReadLine();
            timer.Stop();
            timer.Dispose();
        }

        /// <summary>
        /// Loops the game 1 time and prints it
        /// </summary>
        private void LoopGame(Game.GameEngine game)
        {
            game.Iterate();
            Console.Clear();
            game.PrintField();
        }
    }
}