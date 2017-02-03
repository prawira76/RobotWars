using System;
using System.Runtime.Remoting;
using NUnit.Framework;

namespace RobotWars.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [TestCase("A 2 N")]
        [TestCase("1 B N")]
        [TestCase("1 2 3")]
        [TestCase("1 2 N S")]
        [TestCase("1 2")]
        [TestCase("1.5 2.3 N")]
        public void ParsePosition_InValidInput_ExceptionIsThrown(string input)
        {
            Assert.That(() => Parser.ParsePosition(input), Throws.TypeOf<ArgumentException>());
        }

        [Test, TestCaseSource(typeof(TestDataClass), "ParserTestCases_ParsePositionValidInput")]
        public Position ParsePosition_ValidInput_OutputIsCorrect(string input)
        {
           return Parser.ParsePosition(input);
        }

        [TestCase("A 0")]
        [TestCase("A B")]
        [TestCase("1.1 1")]
        [TestCase("1 1.1")]
        [TestCase("-1 5")]
        [TestCase("5 -1")]
        [TestCase("1 2 B")]
        [TestCase("5")]
        public void ParseCoordinates_InValidInput_ExceptionIsThrown(string input)
        {
            Assert.That(() => Parser.ParseCoordinates(input), Throws.TypeOf<ArgumentException>());
        }

        [Test, TestCaseSource(typeof(TestDataClass), "ParserTestCases_ParseCoordinatesValidInput")]
        public Coordinates ParseCoordinates_ValidInput_OutputIsCorrect(string input)
        {
            return Parser.ParseCoordinates(input);
        }

        [TestCase("NN")]
        [TestCase("A")]
        [TestCase("2")]
        [TestCase("@")]
        [TestCase("N S")]
        public void ParseDirection_InValidInput_ExceptionIsThrown(string input)
        {
            Assert.That(() => Parser.ParseDirection(input), Throws.TypeOf<ArgumentException>());
        }

        [Test, TestCaseSource(typeof(TestDataClass), "ParserTestCases_ParseDirectionValidInput")]
        public Direction ParseDirection_ValidInput_OutputIsCorrect(string input)
        {
           return Parser.ParseDirection(input);
        }

        [TestCase('?')]
        [TestCase('N')]
        [TestCase('2')]
        public void ParseCommand_InvalidInput_ExceptionIsThrown(char input)
        {
            Assert.That(() => Parser.ParseCommand(input), Throws.TypeOf<ArgumentException>());    
        }

        [Test, TestCaseSource(typeof(TestDataClass), "ParserTestCases_ParseCommandValidInput")]
        public ControlCommand ParseCommand_ValidInput_OutputIsCorrect(char input)
        {
            return Parser.ParseCommand(input);
        }
    }
}
