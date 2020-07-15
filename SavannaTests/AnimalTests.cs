using System.Collections.Generic;
using AnimalTypeClassLibrary;
using Castle.Core.Internal;
using Moq;
using Savanna.Animals.NonPredators;
using Savanna.Animals.Predators;
using Savanna.SavannaRandomNumbers;
using Xunit;

namespace SavannaTests
{
    public class AnimalTests
    {
        [Fact]
        public void AnimalDies()
        {
            //Setup
            Lion animal = new Lion
            {
                Health = 20
            };
            //Act

            animal.Die();

            //Assert
            Assert.Equal(0, animal.Health);
        }

        [Fact]
        public void AnimalStraysTopLeft()
        {
            //Setup
            var randomNumberMock = new Mock<ISavannaRandomNumbers>();
            randomNumberMock.Setup(p => p.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(-1);
            List<Animal> animals = new List<Animal>();
            Lion animal = new Lion
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1,
                RandomNumberGenerator = randomNumberMock.Object
            };

            //Act
            animal.Stray(animals, 5, 5);

            //Asseert
            randomNumberMock.Verify(s => s.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeast(2));
            Assert.Equal(0, animal.WidthCoordinate);
            Assert.Equal(0, animal.HeightCoordinate);
        }

        [Fact]
        public void AnimalStraysBottomRight()
        {
            //Setup
            var randomNumberMock = new Mock<ISavannaRandomNumbers>();
            randomNumberMock.Setup(p => p.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(1);
            List<Animal> animals = new List<Animal>();
            Lion animal = new Lion
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1,
                RandomNumberGenerator = randomNumberMock.Object
            };

            //Action
            animal.Stray(animals, 5, 5);

            //Assert
            randomNumberMock.Verify(s => s.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeast(2));
            Assert.Equal(2, animal.WidthCoordinate);
            Assert.Equal(2, animal.HeightCoordinate);
        }

        [Theory]
        [InlineData(1, 1, 2, 1)]
        [InlineData(2, 1, 1, 1)]
        public void BreedingIsSuccesfullParentsStray(int Parent1WidthCoordinate, int Parent1HeightCoordinate, int Parent2WidthCoordinate, int Parent2HeightCoordinate)
        {
            //Setup
            Lion animal1 = new Lion()
            {
                WidthCoordinate = Parent1WidthCoordinate,
                HeightCoordinate = Parent1HeightCoordinate
            };
            Lion animal2 = new Lion()
            {
                WidthCoordinate = Parent2WidthCoordinate,
                HeightCoordinate = Parent2HeightCoordinate
            };
            List<Animal> nearbyanimals = new List<Animal>();
            List<BabyAnimal> babyAnimals = new List<BabyAnimal>();
            BabyAnimal babyAnimal = new BabyAnimal(animal1, animal2)
            {
                RoundCount = 4
            };

            //Action
            babyAnimals.Add(babyAnimal);

            //Assert
            Assert.True(animal1.Breed(nearbyanimals, babyAnimals, 5, 5));
        }

        [Fact]
        public void CantBreedNoAnimalsNearby()
        {
            //Setup
            Lion animal = new Lion()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            List<Animal> nearbyanimals = new List<Animal>();
            List<BabyAnimal> babyAnimals = new List<BabyAnimal>();

            //Result
            var result = animal.Breed(nearbyanimals, babyAnimals, 5, 5);

            //Assert
            Assert.True(babyAnimals.IsNullOrEmpty());
            Assert.False(result);
        }

        [Fact]
        public void BabyAnimalIsCreated()
        {
            //Setup
            Lion animal1 = new Lion()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            Lion animal2 = new Lion()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 1
            };
            List<Animal> nearbyanimals = new List<Animal>() { animal2 };
            List<BabyAnimal> babyAnimals = new List<BabyAnimal>();
            List<Animal> nearbyanimals2 = new List<Animal>() { animal1 };

            //Action
            var result = animal1.Breed(nearbyanimals, babyAnimals, 5, 5);
            animal2.Breed(nearbyanimals2, babyAnimals, 5, 5);

            //Assert
            Assert.True(result);
            Assert.Single(babyAnimals);
        }

        [Fact]
        public void MoveTowardsAnimalTopLeft()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            List<Animal> nearbyanimals = new List<Animal>() { antelope };

            //Action
            lion.MoveTowardsAnimal(antelope, nearbyanimals);

            //Assert
            Assert.Equal(2, lion.WidthCoordinate);
            Assert.Equal(2, lion.HeightCoordinate);
        }

        [Fact]
        public void MoveTowardsAnimalBottomRight()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            List<Animal> nearbyanimals = new List<Animal>() { antelope };

            //Action
            lion.MoveTowardsAnimal(antelope, nearbyanimals);

            //Assert
            Assert.Equal(1, lion.WidthCoordinate);
            Assert.Equal(1, lion.HeightCoordinate);
        }

        [Fact]
        public void RunFromAnimalToBottomRight()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 4,
                HeightCoordinate = 4
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 2
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 5, 5);

            //Assert
            Assert.Equal(1, antelope.WidthCoordinate);
            Assert.Equal(1, antelope.HeightCoordinate);
        }

        [Fact]
        public void RunFromAnimalToBottomLeft()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 4,
                HeightCoordinate = 4
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 6, 6);

            //Assert
            Assert.Equal(5, antelope.WidthCoordinate);
            Assert.Equal(5, antelope.HeightCoordinate);
        }

        [Fact]
        public void RunFromAnimalWhenCoordinatesAreZero()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 2
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 5, 5);

            //Assert
            Assert.Equal(0, antelope.WidthCoordinate);
            Assert.Equal(0, antelope.HeightCoordinate);
        }

        [Fact]
        public void RunFromAnimalWhenCoordinatesAreMaximal()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 2
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 5,
                HeightCoordinate = 5
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 5, 5);

            //Assert
            Assert.Equal(5, antelope.WidthCoordinate);
            Assert.Equal(5, antelope.HeightCoordinate);
        }

        [Fact]
        public void RunFromAnimalWhenCoordinatesAreEqualWithPredatorAtBottomRightCorner()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 5,
                HeightCoordinate = 5
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 5,
                HeightCoordinate = 5
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 5, 5);

            //Assert
            Assert.Equal(4, antelope.WidthCoordinate);
            Assert.Equal(4, antelope.HeightCoordinate);
        }

        [Fact]
        public void RunFromAnimalWhenCoordinatesAreEqualWithPredatorAtTopLeftCorner()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 5, 5);

            //Assert
            Assert.Equal(1, antelope.WidthCoordinate);
            Assert.Equal(1, antelope.HeightCoordinate);
        }

        [Fact]
        public void CantRunBecauseAnimalsAreInTheWay()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 4,
                HeightCoordinate = 4
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 2
            };
            Antelope antelope2 = new Antelope()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 2
            };
            Antelope antelope3 = new Antelope()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 1
            };
            List<Animal> nearbyAnimals = new List<Animal>() { lion, antelope2, antelope3 };

            //Action
            antelope.RunFromAnimal(lion, nearbyAnimals, 5, 5);

            //Assert
            Assert.Equal(2, antelope.WidthCoordinate);
            Assert.Equal(2, antelope.HeightCoordinate);
        }

        [Fact]
        public void OneClosestAnimal()
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 2
            };
            List<Animal> nearbyAnimals = new List<Animal>() { antelope };

            //Action
            var result = lion.LookingForClosestAnimal(1, nearbyAnimals);

            //Assert
            Assert.Contains(antelope, result);
            Assert.Single(result);
        }

        [Fact]
        public void TwoClosestAnimals()//Wierd results
        {
            //Setup
            Lion lion = new Lion()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            Antelope antelope1 = new Antelope()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            Antelope antelope2 = new Antelope()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 2
            };
            List<Animal> nearbyAnimals = new List<Animal>() { antelope1, antelope2 };

            //Action
            var result = lion.LookingForClosestAnimal(5, nearbyAnimals);

            //Assert
            Assert.Contains(antelope1, result);
            Assert.Contains(antelope2, result);
            Assert.Equal(2, result.Count);
        }
    }
}