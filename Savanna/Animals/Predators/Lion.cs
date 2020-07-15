using AnimalTypeClassLibrary;

namespace Savanna.Animals.Predators
{
    /// <summary>
    /// Creating Lion that inherits from Predator class
    /// setting Lion health to 20 and vision range to 6
    /// </summary>
    public class Lion : Predator
    {
        /// <summary>
        /// Defining AnimalSymbol that represents this animal in game visualisation
        /// </summary>
        public override char AnimalSymbol { get { return 'L'; } }

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