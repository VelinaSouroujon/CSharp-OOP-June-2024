using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Tests
    {
        private InfluencerRepository repository;
        [SetUp]
        public void Setup()
        {
            repository = new InfluencerRepository();
        }

        [Test]
        public void Initialize_ShouldCreateEmptyCollection()
        {
            Assert.IsTrue(repository.Influencers.Count == 0);
        }
        [Test]
        public void RegisterInfluencer_InfluencerIsNull_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => repository.RegisterInfluencer(null));
        }
        [Test]
        public void RegisterInfluencer_DuplicateUsername_Throw()
        {
            string name = "Viki";
            repository.RegisterInfluencer(new Influencer(name, 50));

            Assert.Throws<InvalidOperationException>(()
                => repository.RegisterInfluencer(new Influencer(name, 100)));
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(13)]
        [TestCase(152)]
        public void RegisterInfluencer_ValidData_WorksCorrectly(int n)
        {
            List<Influencer> expectedInfluencers = new List<Influencer>();

            for (int i = 0; i < n; i++)
            {
                Influencer influencer = new Influencer($"Pesho{i}", i + 20);

                expectedInfluencers.Add(influencer);
                string returnStr = repository.RegisterInfluencer(influencer);
                string expectedStr = $"Successfully added influencer {influencer.Username} with {influencer.Followers}";

                Assert.AreEqual(expectedStr, returnStr);
            }
            CollectionAssert.AreEqual(expectedInfluencers, repository.Influencers);

            //Assert.AreEqual(expectedInfluencers.Count, repository.Influencers.Count);

            //int counter = 0;
            //foreach(Influencer influencer in repository.Influencers)
            //{
            //    Assert.AreEqual(expectedInfluencers[counter++], influencer);
            //}
        }
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("                ")]
        public void RemoveInfluencer_NullOrWhiteSpace_Throw(string username)
        {
            Assert.Throws<ArgumentNullException>(() => repository.RemoveInfluencer(username));
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(21)]
        public void RemoveInfluencer_ValidData_WorksCorrectly(int n)
        {
            List<Influencer> expectedInfluencers = new List<Influencer>();
            for(int i = 0; i < n; i++)
            {
                Influencer influencer = new Influencer($"Pesho{i}", i + 20);

                expectedInfluencers.Add(influencer);
                repository.RegisterInfluencer(influencer);
            }

            for(int i = 0; i < n; i++)
            {
                Influencer influencerToRemove = expectedInfluencers[i];

                Assert.IsTrue(repository.RemoveInfluencer(influencerToRemove.Username));
                Assert.IsFalse(repository.RemoveInfluencer(influencerToRemove.Username));

                CollectionAssert.AreEqual(expectedInfluencers.Skip(i + 1), repository.Influencers);
            }
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(8)]
        [TestCase(86)]
        public void GetInfluencerWithMostFollowers_WorksCorrectly(int n)
        {
            List<Influencer> expectedInfluencers = new List<Influencer>();

            for (int i = 0; i < n; i++)
            {
                Influencer influencer = new Influencer($"Pesho{i}", Random.Shared.Next());

                expectedInfluencers.Add(influencer);
                repository.RegisterInfluencer(influencer);
            }

            Assert.AreEqual
                (expectedInfluencers.MaxBy(x => x.Followers), repository.GetInfluencerWithMostFollowers());
        }
        [Test]
        public void GetInfluencer_WorksCorrectly()
        {
            for (int i = 0; i < 15; i++)
            {
                Influencer influencer = new Influencer($"Pesho{i}", i + 20);

                Assert.IsTrue(repository.GetInfluencer(influencer.Username) == null);

                repository.RegisterInfluencer(influencer);

                Assert.AreEqual(influencer, repository.GetInfluencer(influencer.Username));
            }
        }
    }
}