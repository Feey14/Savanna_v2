using System;
using System.Collections.Generic;

namespace AnimalTypeClassLibrary
{
    public abstract class NonPredator : Animal
    {
        /// <summary>
        /// Defines logic for NonPredator movement priorites are Retreat from predator > breed > stray
        /// also removes 0.5 health each move
        /// </summary>
        public override void Move(in List<Animal> nearbyanimals, in List<BabyAnimal> babyanimals, int gamewidth, int gameheight)
        {
            if (RetreatFromPredator(nearbyanimals, gamewidth, gameheight) == false)
            {
                if (Breed(nearbyanimals, babyanimals, gamewidth, gameheight) == false)
                {
                    Stray(nearbyanimals, gamewidth, gameheight);
                }
            }
            Health -= 0.5;
        }

        /// <summary>
        /// Looks for closest predator and runs from it
        /// Scans area around animal
        /// if there are multiple predator choseses random one and runs from it
        /// </summary>
        private bool RetreatFromPredator(in List<Animal> nearbyanimals, int gamewidth, int gameheight)
        {
            int lookingforpredatorvisionrange = 1;
            List<Animal> predators = nearbyanimals.FindAll(animal => animal is Predator);
            if (predators.Count > 0)
            {
                List<Animal> runfrom = LookingForClosestAnimal(lookingforpredatorvisionrange, predators);
                if (runfrom.Count > 0)
                {
                    Random random = new Random();
                    Animal predator = runfrom[random.Next(runfrom.Count)];
                    RunFromAnimal(predator, nearbyanimals, gamewidth, gameheight);
                    return true;
                }
            }
            return false;
        }
    }
}