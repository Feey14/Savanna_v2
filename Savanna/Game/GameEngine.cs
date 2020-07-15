using System;
using System.Collections.Generic;
using System.Text;
using AnimalTypeClassLibrary;

namespace Savanna.Game
{
    public class GameEngine
    {
        /// <summary>
        /// List of animals which are in current game
        /// </summary>
        public List<Animal> GameAnimals { get; set; } = new List<Animal>();

        /// <summary>
        /// List of Babyanimals, animals which are not in the game yet but soon should be born
        /// game does not display babyanimals
        /// </summary>
        public List<BabyAnimal> BabyAnimals { get; set; } = new List<BabyAnimal>();

        /// <summary>
        /// Creates Char array, necessary for printing game, fills it with animal symbols at coordinates where animals are located and prints it to colsole
        /// </summary>
        public void PrintField()
        {
            char[,] Field = new char[GameEnvironment.Width, GameEnvironment.Height];
            for (int y = 0; y < GameEnvironment.Height; y++)
                for (int x = 0; x < GameEnvironment.Width; x++)
                    Field[x, y] = ' ';

            foreach (Animal animal in GameAnimals)
            {
                Field[animal.WidthCoordinate, animal.HeightCoordinate] = animal.AnimalSymbol;
            }

            StringBuilder line = new StringBuilder();
            for (int y = 0; y < GameEnvironment.Height; y++)
            {
                line.Clear();
                for (int x = 0; x < GameEnvironment.Width; x++)
                {
                    line.Append(Field[x, y]);
                }
                Console.WriteLine(line.Append("|"));
            }
            line.Clear();
            for (int x = 0; x <= GameEnvironment.Width; x++)
            {
                line.Append("-");
            }
            Console.WriteLine(line.ToString());
        }

        /// <summary>
        /// Adds animal to game at random coordinates where it is possible (in game field bounds and empty field)
        /// </summary>
        public void AddAnimal(params Animal[] animals)
        {
            foreach (Animal animal in animals)
            {
                SavannaRandomNumbers.SavannaRandomNumbers random = new SavannaRandomNumbers.SavannaRandomNumbers();
                int randomheight, randomwidth;
                do
                {
                    randomheight = random.GetRandomNumber(GameEnvironment.Height);
                    randomwidth = random.GetRandomNumber(GameEnvironment.Width);
                } while (GameAnimals.Exists(an => an.WidthCoordinate == randomwidth && an.HeightCoordinate == randomheight));
                animal.WidthCoordinate = randomwidth;
                animal.HeightCoordinate = randomheight;
                GameAnimals.Add(animal);
            }
        }

        /// <summary>
        /// Iterating function that defines how game running, basically moves animals each loop
        /// To make it more conviniet and meke it easier to follow the game. Predators make moves first its acheived by sorting animal list
        /// </summary>
        public void Iterate()
        {
            for (int i = GameAnimals.Count - 1; i >= 0; i--)
            {
                if (GameAnimals[i] is Predator)
                    AnimalIterating(i);
            }
            for (int i = GameAnimals.Count - 1; i >= 0; i--)
            {
                if (GameAnimals[i] is NonPredator)
                    AnimalIterating(i);
            }
            for (int i = BabyAnimals.Count - 1; i >= 0; i--)
            {
                Animal babyanimal = BabyAnimals[i].Move();
                if (babyanimal != null)
                    AddAnimal(babyanimal);
                else if (BabyAnimals[i].RoundCount > 3 && babyanimal == null)
                {
                    BabyAnimals.RemoveAt(i);
                }
            }
            /// <summary>
            /// Function that helps game to iterate collects data for animals to make their move, defines logic when to remove animal from the game
            /// </summary>
            void AnimalIterating(int i)
            {
                //Finds Nearby animals withing Animal visionrange and that is not this animal
                List<Animal> nearbyanimals = GameAnimals.FindAll(animal => animal.WidthCoordinate <= GameAnimals[i].WidthCoordinate + GameAnimals[i].VisionRange
                                                                && animal.WidthCoordinate >= GameAnimals[i].WidthCoordinate - GameAnimals[i].VisionRange
                                                                && animal.HeightCoordinate <= GameAnimals[i].HeightCoordinate + GameAnimals[i].VisionRange
                                                                && animal.HeightCoordinate >= GameAnimals[i].HeightCoordinate - GameAnimals[i].VisionRange
                                                                && animal != GameAnimals[i]);
                GameAnimals[i].Move(nearbyanimals, BabyAnimals, GameEnvironment.Width, GameEnvironment.Height);
                if (GameAnimals[i].Health < 0)
                {
                    GameAnimals.RemoveAt(i);
                }
            }
        }
    }
}