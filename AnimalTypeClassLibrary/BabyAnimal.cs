using System;

namespace AnimalTypeClassLibrary
{
    public class BabyAnimal
    {
        /// <summary>
        /// RoundCount Represents how many rounds babyanimal has existed for
        /// when roundcount is 3 new animal should be born
        /// </summary>
        public int RoundCount { get; set; } = 0;

        /// <summary>
        /// Below are Parent Attributes used for deine if parents have moved or changed
        /// </summary>
        public Animal Parent1 { get; set; }

        public Animal Parent2 { get; set; }
        public int Parent1WidthCoordinates { get; set; }
        public int Parent2WidthCoordinates { get; set; }
        public int Parent1Heightcoordinates { get; set; }
        public int Parent2Heightcoordinates { get; set; }

        /// <summary>
        /// constructor that captures parent data
        /// </summary>
        public BabyAnimal(in Animal parent1, in Animal parent2)
            => (Parent1, Parent2, Parent1WidthCoordinates, Parent2WidthCoordinates, Parent1Heightcoordinates, Parent2Heightcoordinates)
            = (parent1, parent2, parent1.WidthCoordinate, parent2.WidthCoordinate, parent1.HeightCoordinate, parent2.HeightCoordinate);

        /// <summary>
        /// Move function for BabyAnimal makes new animal if 3 rounds has passed
        /// also checks if all criterias for baby to be born are completed such as parents didnt move
        /// </summary>
        public Animal Move()
        {
            if (RoundCount > 3 || Parent1.WidthCoordinate != Parent1WidthCoordinates || Parent2.WidthCoordinate != Parent2WidthCoordinates || Parent1.HeightCoordinate != Parent1Heightcoordinates || Parent2.HeightCoordinate != Parent2Heightcoordinates || Parent1.Health <= 0 || Parent2.Health <= 0) { RoundCount = 9; return null; }
            if (RoundCount == 3)
            {
                Type classname = Parent1.GetType();
                Animal newanimal = (Animal)Activator.CreateInstance(classname);
                RoundCount++;
                return newanimal;
            }
            else
            {
                RoundCount++;
                return null;
            }
        }
    }
}