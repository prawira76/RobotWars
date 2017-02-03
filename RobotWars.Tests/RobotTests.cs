using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace RobotWars.Tests
{
    [TestFixture]
    public class RobotTests
    {
        private Robot _robot;

        [SetUp]
        public void CreateRobot()
        {
            _robot = new Robot("Robot");
        }

        [Test]
        public void Init_ValidInput_StartPositionIsSet()
        {
            var bounds = new Coordinates(5, 5);
            var startPosition = "3 3 E";

            _robot.Init(bounds, startPosition);

            Assert.NotNull(_robot.StartPosition);
        }

        [Test]
        public void Init_ValidInput_CurrentPositionIsSet()
        {
            var bounds = new Coordinates(5, 5);
            var startPosition = "3 3 E";

            _robot.Init(bounds, startPosition);

            Assert.NotNull(_robot.CurrentPosition);
        }

        [Test]
        public void Init_ValidInput_BoundsIsSet()
        {
            var bounds = new Coordinates(5, 5);
            var startPosition = "3 3 E";

            _robot.Init(bounds, startPosition);

            Assert.NotNull(_robot.Bounds);
        }

        [TestCase("5 5 E")]
        [TestCase("6 4 E")]
        [TestCase("5 7 E")]
        public void Init_StartPositionIsOutsideBounds_InitReturnsFalse(string startPosition)
        {
            var bounds = new Coordinates(5, 4);
            
            var result = _robot.Init(bounds, startPosition);

            Assert.False(result);
        }

        [TestCase("5 4 E")]
        [TestCase("0 0 N")]
        [TestCase("3 3 S")]
        public void Init_ValidInput_InitReturnsTrue(string startPosition)
        {
            var bounds = new Coordinates(5, 4);

            var result = _robot.Init(bounds, startPosition);

            Assert.True(result);
        }

        [Test, TestCaseSource(typeof(TestDataClass), "RobotTestCases_ExecuteWithinBounds")]
        public string Execute__EndPositionIsWithinBounds_EndPositionIsCorrect(string strStartPos, string strInstructions)
        {
            var bounds = new Coordinates(5, 5);
            _robot.Init(bounds, strStartPos);

            _robot.ParseAndExecuteInstructions(strInstructions);

            return _robot.EndPosition.ToString();
        }

        [TestCase("1 2 N", "LMLMLMLMM")]
        [TestCase("0 0 E", "MMRMMRMRRM")]
        public void Execute__EndPositionIsOutofBounds_ExecuteMethodReturnsFalse(string strStartPos, string strInstructions)
        {
            var bounds = new Coordinates(1, 2);
            _robot.Init(bounds, strStartPos);
            var result = _robot.ParseAndExecuteInstructions(strInstructions);

            Assert.False(result);
        }
    }

   
}
