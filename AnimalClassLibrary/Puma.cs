using AnimalTypeClassLibrary;

namespace AnimalsClassLibrary
{
    internal class Puma : Predator
    {
        /// <summary>
        /// Defining AnimalSymbol that represents this animal in game visualisation
        /// </summary>
        public override char AnimalSymbol => 'P';

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