using System;
using AnimalTypeClassLibrary;
using Savanna.Game;

namespace Savanna
{
    public class UserInput
    {
        /// <summary>
        /// Captures user input
        /// Checks if animal exists in AnimalDictionary
        /// adds animal based on key input
        /// </summary>
        public void AddAnimals(GameEngine game)
        {
            AnimalDictionary animaldictionary = new AnimalDictionary();
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                if (animaldictionary.AnimalTypes.TryGetValue(input.KeyChar, out Type type))
                {
                    Animal animal = (Animal)Activator.CreateInstance(type);
                    game.AddAnimal(animal);
                }
            } while (input.Key != ConsoleKey.Escape);
        }
    }
}