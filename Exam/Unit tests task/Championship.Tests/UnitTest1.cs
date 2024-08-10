using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Championship.Tests
{
    public class Tests
    {
        private League league;
        [SetUp]
        public void Setup()
        {
            league = new League();
        }

        [Test]
        public void Initialize_ShouldWorkCorrectly()
        {
            Assert.IsTrue(league.Teams.Count == 0);
            Assert.IsTrue(league.Capacity == 10);
        }
        [Test]
        public void AddTeam_ExceedCapacity_ThrowException()
        {
            PopulateLeagueWithTeams();
            int n = league.Capacity - league.Teams.Count;
            for(int i = 0; i < n; i++)
            {
                league.AddTeam(new Team($"Real Madrid{i}"));
            }

            InvalidOperationException ex =
                Assert.Throws<InvalidOperationException>(() => league.AddTeam(new Team("Arsenal")));

            Assert.AreEqual("League is full.", ex.Message);
        }
        [Test]
        public void AddTeam_DuplicateName_ThrowException()
        {
            PopulateLeagueWithTeams();

            string duplicateName = league.Teams.Last().Name;

            InvalidOperationException ex =
                Assert.Throws<InvalidOperationException>(() => league.AddTeam(new Team(duplicateName)));

            Assert.AreEqual("Team already exists.", ex.Message);
        }
        [Test]
        public void AddTeam_ValidData_ShouldWorkCorrectly()
        {
            List<Team> expectedTeams = new List<Team>();

            for(int i = 0; i < league.Capacity; i++)
            {
                Team teamToAdd = new Team($"Barselona{i}");

                expectedTeams.Add(teamToAdd);
                league.AddTeam(teamToAdd);

                CollectionAssert.AreEqual(expectedTeams, league.Teams);
            }
        }
        [Test]
        public void RemoveTeam_TeamDoesNotExist_ShouldReturnFalse()
        {
            PopulateLeagueWithTeams();
            string invalidTeamName = league.Teams.Last().Name + "invalid";

            Assert.IsFalse(league.RemoveTeam(invalidTeamName));
        }
        [Test]
        public void RemoveTeam_ValidData_ShouldReturnTrueAndRemoveTeam()
        {
            PopulateLeagueWithTeams();
            List<Team> initialTeams = new List<Team>(league.Teams);

            for(int i = 0; i < initialTeams.Count; i++)
            {
                bool result = league.RemoveTeam(initialTeams[i].Name);
                Assert.IsTrue(result);

                CollectionAssert.AreEqual(initialTeams.Skip(i + 1), league.Teams);
            }
        }
        [Test]
        public void PlayMatch_HomeTeamDoesNotExist_ThrowException()
        {
            PopulateLeagueWithTeams();

            string homeTeamName = league.Teams.Last().Name + "Non existing team";
            string awayTeamName = league.Teams.Last().Name;

            Assert.Throws<InvalidOperationException>(()
                => league.PlayMatch(homeTeamName, awayTeamName, 2, 3));
        }
        [Test]
        public void PlayMatch_AwayTeamDoesNotExist_ThrowException()
        {
            PopulateLeagueWithTeams();

            string awayTeamName = league.Teams.Last().Name + "Non existing team";
            string homeTeamName = league.Teams.Last().Name;

            Assert.Throws<InvalidOperationException>(()
                => league.PlayMatch(homeTeamName, awayTeamName, 2, 3));
        }
        [Test]
        public void PlayMatch_BothTeamsDoNotExist_ThrowException()
        {
            PopulateLeagueWithTeams();

            string homeTeamName = league.Teams.Last().Name + "Non existing team";
            string awayTeamName = league.Teams.Last().Name + "Invalid";

            Assert.Throws<InvalidOperationException>(()
                => league.PlayMatch(homeTeamName, awayTeamName, 2, 3));
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void PlayMatch_Draw_ShouldWorkCorrectly(int goalsScored)
        {
            PopulateLeagueWithTeams();

            Team homeTeam = league.Teams.First();
            Team awayTeam = league.Teams.Last();

            for (int i = 0; i < 15; i++)
            {
                league.PlayMatch(homeTeam.Name, awayTeam.Name, goalsScored, goalsScored);

                Assert.AreEqual(i + 1, homeTeam.Draws);
                Assert.AreEqual(i + 1, awayTeam.Draws);

                Assert.AreEqual(0, homeTeam.Wins);
                Assert.AreEqual(0, awayTeam.Wins);

                Assert.AreEqual(0, homeTeam.Loses);
                Assert.AreEqual(0, awayTeam.Loses);
            }
        }
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 3)]
        [TestCase(6, 5)]
        public void PlayMatch_HomeTeamWins_ShouldWorkCorrectly(int homeTeamGoals, int awayTeamGoals)
        {
            PopulateLeagueWithTeams();

            Team homeTeam = league.Teams.First();
            Team awayTeam = league.Teams.Last();

            for (int i = 0; i < 15; i++)
            {
                league.PlayMatch(homeTeam.Name, awayTeam.Name, homeTeamGoals, awayTeamGoals);

                Assert.AreEqual(0, homeTeam.Draws);
                Assert.AreEqual(0, awayTeam.Draws);

                Assert.AreEqual(i + 1, homeTeam.Wins);
                Assert.AreEqual(0, awayTeam.Wins);

                Assert.AreEqual(0, homeTeam.Loses);
                Assert.AreEqual(i + 1, awayTeam.Loses);
            }
        }
        [TestCase(0, 1)]
        [TestCase(0, 5)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        public void PlayMatch_AwayTeamWins_ShouldWorkCorreclt(int homeTeamGoals, int awayTeamGoals)
        {
            PopulateLeagueWithTeams();

            Team homeTeam = league.Teams.First();
            Team awayTeam = league.Teams.Last();

            for (int i = 0; i < 15; i++)
            {
                league.PlayMatch(homeTeam.Name, awayTeam.Name, homeTeamGoals, awayTeamGoals);

                Assert.AreEqual(0, homeTeam.Draws);
                Assert.AreEqual(0, awayTeam.Draws);

                Assert.AreEqual(0, homeTeam.Wins);
                Assert.AreEqual(i + 1, awayTeam.Wins);

                Assert.AreEqual(i + 1, homeTeam.Loses);
                Assert.AreEqual(0, awayTeam.Loses);
            }
        }
        [Test]
        public void GetTeamInfo_TeamDoesNotExist_ThrowException()
        {
            PopulateLeagueWithTeams();
            string invalidName = league.Teams.Last().Name + "invalid";

            Assert.Throws<InvalidOperationException>(() => league.GetTeamInfo(invalidName));
        }
        [Test]
        public void GetTeamInfo_ValidData_ShouldWorkCorrectly()
        {
            PopulateLeagueWithTeams();

            foreach(Team team in league.Teams)
            {
                string expected =
                    $"{team.Name} - {team.Points} points ({team.Wins}W {team.Draws}D {team.Loses}L)";

                string actual = league.GetTeamInfo(team.Name);

                Assert.AreEqual(expected, actual);
            }
        }
        private void PopulateLeagueWithTeams()
        {
            for (int i = 0; i < league.Capacity - 1; i++)
            {
                league.AddTeam(new Team($"Barselona{i}"));
            }
        }
    }
}