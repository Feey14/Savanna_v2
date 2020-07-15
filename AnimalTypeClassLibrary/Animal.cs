using System.Collections.Generic;
using System.Linq;
using Savanna.SavannaRandomNumbers;

namespace AnimalTypeClassLibrary
{
    public abstract class Animal
    {
        /// <summary>
        /// Animal's current WidthCoordinate within the game
        /// </summary>
        public int WidthCoordinate { get; set; }

        /// <summary>
        /// Animal's current HeightCoordinate within the game
        /// </summary>
        public int HeightCoordinate { get; set; }

        /// <summary>
        /// Animal's current Health value
        /// Animal is considered dead when his health is 0 and should be removed from the game
        /// </summary>
        public abstract double Health { get; set; }

        /// <summary>
        /// overriding Animal's Visionrange, animals sees his victim/attacker within this range
        /// </summary>
        public abstract int VisionRange { get; }

        /// <summary>
        /// AnimalSymbol represents this animal in game visualisation
        /// </summary>
        public abstract char AnimalSymbol { get; }

        /// <summary>
        /// Setting Random number generator
        /// </summary>
        public ISavannaRandomNumbers RandomNumberGenerator { get; set; }

        public Animal(ISavannaRandomNumbers random = null)
        {
            if (random == null)
                RandomNumberGenerator = new SavannaRandomNumbers();
        }

        /// <summary>
        /// Defines how animals move, each animal has different move logic based on its type
        /// </summary>
        public abstract void Move(in List<Animal> nearbyanimals, in List<BabyAnimal> babyanimals, int gamewidth, int gameheight);

        /// <summary>
        /// Sets Health to 0 when animal dies
        /// </summary>
        public void Die()
        {
            Health = 0;
        }

        /// <summary>
        /// Function that makes animal stray in random empty direction
        /// </summary>
        public void Stray(in List<Animal> nearbyanimals, int gamewidth, int gameheight)
        {
            int x, y;
            do
            {
                x = WidthCoordinate + RandomNumberGenerator.GetRandomNumber(-1, 1);
                y = HeightCoordinate + RandomNumberGenerator.GetRandomNumber(-1, 1);
            } while (x < 0 || y < 0 || x >= gamewidth || y >= gameheight || !IsEmpty(x, y, nearbyanimals));
            WidthCoordinate = x;
            HeightCoordinate = y;
        }

        /// <summary>
        /// Breed function makes BabyAnimal and if parent are in the same position for 3 rounds animal is born
        /// if parents have child they keep breeding untill child is born
        /// and when child should be born parents make move so they dont just keep breeding
        /// </summary>
        public bool Breed(in List<Animal> nearbyanimals, List<BabyAnimal> babyanimals, int gamewidth, int gameheight)
        {
            BabyAnimal existingchild = babyanimals.Find(babyanimal => babyanimal.Parent1 == this || babyanimal.Parent2 == this);
            if (existingchild != null)
            {
                if (existingchild.RoundCount > 3)
                {
                    Stray(nearbyanimals, gamewidth, gameheight);
                }
                return true;
            }
            else
            if (nearbyanimals.Any(an => an.WidthCoordinate == WidthCoordinate - 1 && an.HeightCoordinate == HeightCoordinate && an.GetType().Equals(this.GetType())))
            {
                Animal result = nearbyanimals.Find(an => an.WidthCoordinate == WidthCoordinate - 1 && an.HeightCoordinate == HeightCoordinate);
                BabyAnimal child = new BabyAnimal(this, result);
                babyanimals.Add(child);
                return true;
            }
            else if (nearbyanimals.Any(an => an.WidthCoordinate == WidthCoordinate + 1 && an.HeightCoordinate == HeightCoordinate && an.GetType().Equals(this.GetType())))
            {
                Animal result = nearbyanimals.Find(an => an.WidthCoordinate == WidthCoordinate + 1 && an.HeightCoordinate == HeightCoordinate);
                BabyAnimal child = new BabyAnimal(this, result);
                babyanimals.Add(child);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Defines how animal is moving towards other animal also dosent allow it to step on other animal
        /// </summary>
        public void MoveTowardsAnimal(in Animal target, in List<Animal> nearbyanimals)
        {
            if (target.WidthCoordinate > WidthCoordinate && IsEmpty(WidthCoordinate + 1, HeightCoordinate, nearbyanimals)) WidthCoordinate += 1;
            else if (target.WidthCoordinate < WidthCoordinate && IsEmpty(WidthCoordinate - 1, HeightCoordinate, nearbyanimals)) WidthCoordinate -= 1;
            if (target.HeightCoordinate > HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate + 1, nearbyanimals)) HeightCoordinate += 1;
            else if (target.HeightCoordinate < HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate - 1, nearbyanimals)) HeightCoordinate -= 1;
        }

        /// <summary>
        /// Defines how animal is running from other animal, dosent allow to step on other animal
        /// </summary>
        public void RunFromAnimal(in Animal target, in List<Animal> nearbyanimals, int gamewidth, int gameheight)
        {
            if (target.WidthCoordinate > WidthCoordinate && WidthCoordinate > 0 && IsEmpty(WidthCoordinate - 1, HeightCoordinate, nearbyanimals)) WidthCoordinate -= 1;
            else if (target.WidthCoordinate == WidthCoordinate && WidthCoordinate > 0 && IsEmpty(WidthCoordinate - 1, HeightCoordinate, nearbyanimals)) WidthCoordinate -= 1;
            else if (WidthCoordinate < gamewidth - 1)
            {
                if (target.WidthCoordinate < WidthCoordinate && IsEmpty(WidthCoordinate + 1, HeightCoordinate, nearbyanimals)) WidthCoordinate += 1;
                else if (target.WidthCoordinate == WidthCoordinate && IsEmpty(WidthCoordinate + 1, HeightCoordinate, nearbyanimals)) WidthCoordinate += 1;
            }

            if (target.HeightCoordinate > HeightCoordinate && HeightCoordinate > 0 && IsEmpty(WidthCoordinate, HeightCoordinate - 1, nearbyanimals)) HeightCoordinate -= 1;
            else if (target.HeightCoordinate == HeightCoordinate && HeightCoordinate > 0 && IsEmpty(WidthCoordinate, HeightCoordinate - 1, nearbyanimals)) HeightCoordinate -= 1;
            else if (HeightCoordinate < gameheight - 1)
            {
                if (target.HeightCoordinate < HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate + 1, nearbyanimals)) HeightCoordinate += 1;
                else if (target.HeightCoordinate == HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate + 1, nearbyanimals)) HeightCoordinate += 1;
            }
        }

        /// <summary>
        /// Checks if there is animal on provided coordinates
        /// </summary>
        private bool IsEmpty(int widthcoordinate, int heightcoordinate, in List<Animal> nearbyanimals)
        {
            if (nearbyanimals.Any(an => an.WidthCoordinate == widthcoordinate && an.HeightCoordinate == heightcoordinate)) return false;
            else return true;
        }

        /// <summary>
        /// Returns closest animals to this animal
        /// </summary>
        public List<Animal> LookingForClosestAnimal(int visionrange, in List<Animal> nearbyanimals)
        {
            List<Animal> ClosestAnimals;
            do
            {
                //Looking for closeset animal
                ClosestAnimals = nearbyanimals.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + visionrange && animal.WidthCoordinate >= WidthCoordinate - visionrange && animal.HeightCoordinate <= HeightCoordinate + visionrange && animal.HeightCoordinate >= HeightCoordinate - visionrange && animal != this);
                visionrange++;
            }
            while (visionrange <= VisionRange && ClosestAnimals.Count <= 0);
            return ClosestAnimals;
        }
    }
}