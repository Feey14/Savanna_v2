using AnimalTypeClassLibrary;

namespace Savanna.Animals.NonPredators
{
    /// <summary>
    /// Creating Antelope that inherits from NonPredator class
    /// setting Antelope's health to 20 and vision range to 6
    /// </summary>
    public class Antelope : NonPredator
    {
        /// <summary>
        /// Defining AnimalSymbol that represents this animal in game visualisation
        /// </summary>
        public override char AnimalSymbol => 'A';

        /// <summary>
        /// overriding Animal's Health with which he starts game with
        /// </summary>
        public override double Health { get; set; } = 20;

        /// <summary>
        /// overriding Animal's Visionrange, animals sees his victim/attacker within this range
        /// </summary>
        public override int VisionRange => 6;
    }
}