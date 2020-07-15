using AnimalTypeClassLibrary;
using Savanna.Animals.Predators;
using Xunit;

namespace SavannaTests
{
    public class BabyAnimalTest
    {
        [Fact]
        public void BabyAnimalMoveWhenRoundCountIs1()
        {
            //Setup
            Lion lion1 = new Lion();
            Lion lion2 = new Lion();
            BabyAnimal babyAnimal = new BabyAnimal(lion1, lion2) { RoundCount = 1 };

            //Act
            var result = babyAnimal.Move();

            //Assert
            Assert.Null(result);
            Assert.Equal(2, babyAnimal.RoundCount);
        }

        [Fact]
        public void BabyAnimalMoveWhenRoundCountIs3()
        {
            //Setup
            Lion lion1 = new Lion();
            Lion lion2 = new Lion();
            BabyAnimal babyAnimal = new BabyAnimal(lion1, lion2) { RoundCount = 3 };

            //Act
            var result = babyAnimal.Move();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(4, babyAnimal.RoundCount);
        }

        [Fact]
        public void BabyAnimalMoveWhenRoundCountIsGreaterThan3()
        {
            //Setup
            Lion lion1 = new Lion();
            Lion lion2 = new Lion();
            BabyAnimal babyAnimal = new BabyAnimal(lion1, lion2) { RoundCount = 4 };

            //Act
            var result = babyAnimal.Move();

            //Assert
            Assert.Null(result);
            Assert.Equal(9, babyAnimal.RoundCount);
        }

        [Fact]
        public void BabyAnimalMoveButParentCoordinatesChanged()
        {
            //Setup
            Lion lion1 = new Lion() { WidthCoordinate = 1, HeightCoordinate = 1 };
            Lion lion2 = new Lion() { WidthCoordinate = 2, HeightCoordinate = 1 };
            BabyAnimal babyAnimal = new BabyAnimal(lion1, lion2) { RoundCount = 2 };
            lion1.HeightCoordinate = 2;

            //Act
            var result = babyAnimal.Move();

            //Assert
            Assert.Null(result);
            Assert.Equal(9, babyAnimal.RoundCount);
        }

        [Fact]
        public void BabyAnimalMoveButParentHasBeenRemoved()
        {
            //Setup
            Lion lion1 = new Lion() { WidthCoordinate = 1, HeightCoordinate = 1 };
            Lion lion2 = new Lion() { WidthCoordinate = 2, HeightCoordinate = 1 };
            BabyAnimal babyAnimal = new BabyAnimal(lion1, lion2) { RoundCount = 3 };
            lion1.Die();

            //Act
            var result = babyAnimal.Move();

            //Assert
            Assert.Null(result);
            Assert.Equal(9, babyAnimal.RoundCount);
        }
    }
}