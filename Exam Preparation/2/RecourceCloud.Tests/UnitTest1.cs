using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecourceCloud.Tests
{
    public class Tests
    {
        private DepartmentCloud departmentCloud;
        [SetUp]
        public void Setup()
        {
            departmentCloud = new DepartmentCloud();
        }

        [Test]
        public void Initialize_CreateEmptyCollections()
        {
            Assert.IsTrue(departmentCloud.Tasks.Count == 0);
            Assert.IsTrue(departmentCloud.Resources.Count == 0);
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(50)]
        public void LogTask_InvalidArgsCount_Throw(int n)
        {
            string[] args = new string[n];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = i.ToString();
            }
            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(args));
        }
        [Test]
        public void LogTask_NullArgs_Throw()
        {
            string[] args = new string[3];
            for (int i = 0;i < args.Length;i++)
            {
                Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(args));
                args[i] = i.ToString();
            }
        }
        [Test]
        public void LogTask_DuplicateName_ReturnCorrectMessage()
        {
            string duplicateName = "Gosho";
            string[] args1 = { "2", "Pesho", duplicateName };
            string[] args2 = {"1", "Viktor", duplicateName};

            departmentCloud.LogTask(args1);

            string expectedResult = $"{duplicateName} is already logged.";
            string actualResult = departmentCloud.LogTask(args2);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsTrue(departmentCloud.Tasks.Count == 1);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(7)]
        [TestCase(20)]
        public void LogTask_ValidData_ShouldWorkCorrectly(int n)
        {
            List<Task> expectedTasks = new List<Task>();

            for (int i = 0; i < n; i++)
            {
                string[] args = { $"{i + 1}", $"Kitty{i}", $"Pesho{i}" };

                Task task = new Task(int.Parse(args[0]), args[1], args[2]);
                expectedTasks.Add(task);

                string actualReturn = departmentCloud.LogTask(args);
                string expectedReturn = "Task logged successfully.";

                Assert.AreEqual(expectedReturn, actualReturn);
                Assert.AreEqual(expectedTasks.Count, departmentCloud.Tasks.Count);

                int counter = 0;
                foreach(Task actualTask in departmentCloud.Tasks)
                {
                    Task expectedTask = expectedTasks[counter];

                    Assert.AreEqual(expectedTask.Priority, actualTask.Priority);
                    Assert.AreEqual(expectedTask.Label, actualTask.Label);
                    Assert.AreEqual(expectedTask.ResourceName, actualTask.ResourceName);

                    counter++;
                }
            }
        }
        [Test]
        public void CreateResource_NoTasks_ReturnsFalse()
        {
            bool result = departmentCloud.CreateResource();
            Assert.IsFalse(result);
        }
        [Test]
        public void CreateResource_ValidData_ShouldWorkCorrectly()
        {
            PopulateTasks();

            int n = departmentCloud.Tasks.Count;
            for (int i = 0; i < n; i++)
            {
                int initialTasksCount = departmentCloud.Tasks.Count;

                Task expectedTask = departmentCloud.Tasks.MinBy(x => x.Priority);

                Assert.IsTrue(departmentCloud.Tasks.Contains(expectedTask));
                bool actualResult = departmentCloud.CreateResource();
                Assert.IsFalse(departmentCloud.Tasks.Contains(expectedTask));
                Assert.IsTrue(actualResult);

                Resource actualResource = departmentCloud.Resources.Last();

                Assert.IsTrue(departmentCloud.Resources.Count == i + 1);
                Assert.IsTrue(departmentCloud.Tasks.Count == initialTasksCount - 1);

                Assert.AreEqual(expectedTask.ResourceName, actualResource.Name);
                Assert.AreEqual(expectedTask.Label, actualResource.ResourceType);
            }
        }
        [Test]
        public void TestResource_NoResourceFound_ReturnsNull()
        {
            PopulateResources();

            string invalidName = "Invalid name";

            Resource? result = departmentCloud.TestResource(invalidName);
            Assert.IsNull(result);
        }
        [Test]
        public void TestResource_ValidData_ShouldWorkCorrectly()
        {
            PopulateResources();

            Resource expectedResource = departmentCloud.Resources.Last();
            Resource actualResource = departmentCloud.TestResource(expectedResource.Name);

            Assert.AreEqual(expectedResource, actualResource);

            Assert.IsTrue(actualResource.IsTested);
        }
        private void PopulateTasks()
        {
            for (int i = 0; i < 15; i++)
            {
                string[] args = { $"{Random.Shared.Next()}", $"Kitty{i}", $"Pesho{i}" };
                departmentCloud.LogTask(args);
            }

        }
        private void PopulateResources()
        {
            PopulateTasks();

            for (int i = 0; i < departmentCloud.Tasks.Count; i++)
            {
                departmentCloud.CreateResource();
            }
        }
    }
}