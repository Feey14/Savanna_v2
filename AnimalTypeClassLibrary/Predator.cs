using System;
using System.Collections.Generic;

namespace AnimalTypeClassLibrary
{
    public abstract class Predator : Animal
    {
        /// <summary>
        /// Defines logic for Predator movement priorites are eat animal > chase prey > breed > stray
        /// also removes 0.5 health each move
        /// </summary>
        public override void Move(in List<Animal> nearbyanimals, in List<BabyAnimal> babyanimals, int gamewidth, int gameheight)
        {
            if (EatClosestNonPredator(nearbyanimals) == false)
                if (ChaseClosestNonPredator(nearbyanimals) == false)
                {
                    if (Breed(nearbyanimals, babyanimals, gamewidth, gameheight) == false)
                    {
                        Stray(nearbyanimals, gamewidth, gameheight);
                    }
                }
            Health -= 0.5;
        }

        /// <summary>
        /// If its possible eats nearby animal that is located in 1 coordinate interval
        /// If multiple animals can be eatern random animal is eaten
        /// Marks prey as dead, prey coordinates are assigned to predator, health restored to 20
        /// returns true if prey is eaten returns false if theres nothing to eat
        /// </summary>
        private bool EatClosestNonPredator(List<Animal> nearbyanimas)
        {
            // Finds animal within 1 cell that is not predator
            List<Animal> CanEat = nearbyanimas.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + 1
                                                    && animal.WidthCoordinate >= WidthCoordinate - 1
                                                    && animal.HeightCoordinate <= HeightCoordinate + 1
                                                    && animal.HeightCoordinate >= HeightCoordinate - 1
                                                    && animal != this && animal is NonPredator);
            if (CanEat.Count > 0)
            {
                Random randon = new Random();
                Animal target = CanEat[randon.Next(CanEat.Count)];
                if (target.Health > 0)
                {
                    target.Die();
                    WidthCoordinate = target.WidthCoordinate;
                    HeightCoordinate = target.HeightCoordinate;
                    Health = 20;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Looks for closest non predator prey and chases it by moving towards it
        /// Scans area around predator and chases nearest prey
        /// if there are multiple victims chosese random one
        /// </summary>
        private bool ChaseClosestNonPredator(List<Animal> nearbyanimals)
        {
            int lookingforpreyvisionrange = 2;
            List<Animal> targets = nearbyanimals.FindAll(animal => animal is NonPredator);
            if (targets.Count > 0)
            {
                List<Animal> canChase = LookingForClosestAnimal(lookingforpreyvisionrange, targets);
                if (canChase.Count > 0)
                {
                    Random random = new Random();
                    Animal prey = canChase[random.Next(canChase.Count)];
                    MoveTowardsAnimal(prey, nearbyanimals);
                    return true;
                }
            }
            return false;
        }
    }
}