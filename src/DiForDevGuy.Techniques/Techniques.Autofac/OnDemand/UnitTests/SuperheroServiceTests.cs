using Lib;
using Lib.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class SuperheroServiceTests
    {
        [Test]
        public void test_getting_all_avengers()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            mockLogger.Setup(obj =>
                obj.Log(It.IsAny<string>(), It.IsAny<string[]>())).Callback<string, string[]>((msg, args) =>
                {
                    Console.WriteLine("Unit test Logger output: " + msg);
                });

            Mock<IAvengerRepository> mockAvengerRepository = new Mock<IAvengerRepository>();

            mockAvengerRepository.Setup(obj => obj.FetchAll()).Returns(GetHeroList());

            Mock<IComponentLocator> mockComponentLocator = new Mock<IComponentLocator>();

            mockComponentLocator.Setup(obj => obj.ResolveComponent<ILogger>()).Returns(mockLogger.Object);
            mockComponentLocator.Setup(obj => obj.ResolveComponent<IAvengerRepository>()).Returns(mockAvengerRepository.Object);

            SuperheroService superheroService = new SuperheroService(mockComponentLocator.Object);

            IEnumerable<Hero> avengers = superheroService.GetAvengers();

            Assert.IsTrue(avengers.Count() == 3);
        }

        [Test]
        public void test_getting_a_single_avenger()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            mockLogger.Setup(obj =>
                obj.Log(It.IsAny<string>(), It.IsAny<string[]>())).Callback<string, string[]>((msg, args) =>
                {
                    Console.WriteLine("Unit test Logger output: " + msg);
                });

            Mock<IAvengerRepository> mockAvengerRepository = new Mock<IAvengerRepository>();

            string heroName = "Ironman";

            mockAvengerRepository.Setup(obj => obj.Fetch(heroName)).Returns(GetHeroList().First(item => item.SuperheroName == heroName));

            Mock<IComponentLocator> mockComponentLocator = new Mock<IComponentLocator>();

            mockComponentLocator.Setup(obj => obj.ResolveComponent<ILogger>()).Returns(mockLogger.Object);
            mockComponentLocator.Setup(obj => obj.ResolveComponent<IAvengerRepository>()).Returns(mockAvengerRepository.Object);

            SuperheroService superheroService = new SuperheroService(mockComponentLocator.Object);

            Hero avenger = superheroService.GetAvenger(heroName);

            Assert.IsTrue(avenger.SuperheroName == heroName);
        }

        IEnumerable<Hero> GetHeroList()
        {
            List<Hero> heroes = new List<Hero>()
            {
                new Hero("Ironman", "Tony Stark", "Badass Geek Suit"),
                new Hero("Thor", "Thor Johnson", "Craftsman Professional Hammer"),
                new Hero("Captain America", "Steve Rogers", "Steroid Tolerance & Big Frisbee")
            };

            return heroes;
        }
    }
}