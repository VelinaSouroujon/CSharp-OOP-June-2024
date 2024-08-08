namespace Television.Tests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;
    public class Tests
    {
        private TelevisionDevice television;

        [SetUp]
        public void Setup()
        {
            television = new TelevisionDevice("Sony", 590, 40, 15);
        }

        [Test]
        public void Initialize_ValidData_WorksCorrectly()
        {
            string brand = "Samsung";
            int price = 450;
            int screenWidth = 30;
            int screenHeight = 20;

            television = new TelevisionDevice(brand, price, screenWidth, screenHeight);

            Assert.AreEqual(brand, television.Brand);
            Assert.AreEqual(price, television.Price);
            Assert.AreEqual(screenWidth, television.ScreenWidth);
            Assert.AreEqual(screenHeight, television.ScreenHeigth);

            Assert.AreEqual(0, television.CurrentChannel);
            Assert.AreEqual(13, television.Volume);
            Assert.IsFalse(television.IsMuted);
        }
        [Test]
        public void SwitchOn_WorksCorrectly()
        {
            string expectedNotMuted =
                $"Cahnnel {television.CurrentChannel} - Volume {television.Volume} - Sound On";

            string actualNotMuted = television.SwitchOn();

            Assert.AreEqual(expectedNotMuted, actualNotMuted);

            television.MuteDevice();

            string expectedMuted =
                $"Cahnnel {television.CurrentChannel} - Volume {television.Volume} - Sound Off";

            string actualMuted = television.SwitchOn();

            Assert.AreEqual (expectedMuted, actualMuted);
        }
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-19)]
        public void ChangeChannel_NegativeValue_ThrowException(int value)
        {
            Assert.Throws<ArgumentException>(() => television.ChangeChannel(value));
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        [TestCase(34)]
        public void ChangeChannel_ValidData_WorksCorrectly(int value)
        {
            int actualResult = television.ChangeChannel(value);

            Assert.AreEqual(value, television.CurrentChannel);
            Assert.AreEqual(value, actualResult);
        }
        [TestCase(0)]
        [TestCase(87)]
        [TestCase(1)]
        [TestCase(73)]
        [TestCase(-25)]
        [TestCase(-1)]
        [TestCase(-87)]
        public void VolumeChange_Up_LowerThan100_WorksCorrectly(int units)
        {         
            int lastVolume = 13;
            int expectedVolume = lastVolume + Math.Abs(units);

            string expectedResult = $"Volume: {expectedVolume}";
            string actualResult = television.VolumeChange("UP", units);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedVolume, television.Volume);
        }
        [TestCase(88)]
        [TestCase(90)]
        [TestCase(500)]
        [TestCase(-88)]
        [TestCase(-108)]
        [TestCase(-90)]
        public void VolumeChange_Up_MoreThan100_WorksCorrectly(int units)
        {
            int expectedVolume = 100;

            string expectedResult = $"Volume: {expectedVolume}";
            string actualResult = television.VolumeChange("UP", units);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedVolume, television.Volume);
        }
        [TestCase(13)]
        [TestCase(-13)]
        [TestCase(0)]
        [TestCase(-7)]
        [TestCase(3)]
        public void VolumeChange_Down_WorksCorrectly(int units)
        {
            int lastVolume = 13;
            int expectedVolume = lastVolume - Math.Abs(units);

            string expectedResult = $"Volume: {expectedVolume}";
            string actualResult = television.VolumeChange("DOWN", units);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedVolume, television.Volume);
        }
        [TestCase(14)]
        [TestCase(-14)]
        [TestCase(-16)]
        [TestCase(64)]
        public void VolumeChange_DownToZero_WorksCorrectly(int units)
        {
            int expectedVolume = 0;

            string expectedResult = $"Volume: {expectedVolume}";
            string actualResult = television.VolumeChange("DOWN", units);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedVolume, television.Volume);
        }
        [Test]
        public void MuteDevice_WorksCorrectly()
        {
            for (int i = 0; i < 10; i++)
            {
                if(i % 2 == 0)
                {
                    Assert.IsTrue(television.MuteDevice());
                    Assert.IsTrue(television.IsMuted);
                }
                else
                {
                    Assert.IsFalse(television.MuteDevice());
                    Assert.IsFalse(television.IsMuted);
                }
            }
        }
        [Test]
        public void ToString_WorksCorrectly()
        {
            string expected =
                $"TV Device: {television.Brand}, Screen Resolution: {television.ScreenWidth}x{television.ScreenHeigth}, Price {television.Price}$";
            string actual = television.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}