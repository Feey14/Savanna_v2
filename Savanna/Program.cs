namespace Savanna
{
    public class Program
    {
        /// <summary>
        /// initializing game
        /// </summary>
        private static void Main()
        {
            Game.GameEngine game = new Game.GameEngine();
            GameLoop timer = new GameLoop();
            timer.StartTimer(game);
        }
    }
}