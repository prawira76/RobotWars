using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RobotWars.Tests
{
    public class TestDataClass
    {
        public static IEnumerable RobotTestCases_ExecuteWithinBounds
        {
            get
            {
                yield return new TestCaseData("1 2 N", "LMLMLMLMM").Returns("1 3 N");
                yield return new TestCaseData("3 3 E", "MMRMMRMRRM").Returns("5 1 E");
            }
        }

        public static IEnumerable RobotTestCases_ExecuteOutofBounds
        {
            get
            {
                yield return new TestCaseData("1 2 N", "LMLMLMLMM").Returns(false);
                yield return new TestCaseData("0 0 E", "MMRMMRMRRM").Returns(false);
            }
        }

        public static IEnumerable ParserTestCases_ParseDirectionValidInput
        {
            get
            {
                yield return new TestCaseData("N").Returns(Direction.N);
                yield return new TestCaseData("S").Returns(Direction.S);
                yield return new TestCaseData("W").Returns(Direction.W);
                yield return new TestCaseData("E").Returns(Direction.E);
            }
        }

        public static IEnumerable ParserTestCases_ParsePositionValidInput
        {
            get
            {
                yield return new TestCaseData("1 2 N").Returns(new Position(new Coordinates(1, 2), Direction.N ));
                yield return new TestCaseData("0 0 S").Returns(new Position(new Coordinates(0, 0), Direction.S));
                yield return new TestCaseData("22 33 W").Returns(new Position(new Coordinates(22, 33), Direction.W));
                yield return new TestCaseData("0 10 E").Returns(new Position(new Coordinates(0, 10), Direction.E));
                yield return new TestCaseData("20 0 S").Returns(new Position(new Coordinates(20, 0), Direction.S));
            }
        }

        public static IEnumerable ParserTestCases_ParseCoordinatesValidInput
        {
            get
            {
                yield return new TestCaseData("1 2").Returns(new Coordinates(1, 2));
                yield return new TestCaseData("0 0").Returns(new Coordinates(0, 0));
                yield return new TestCaseData("1000 2000").Returns(new Coordinates(1000, 2000));
                yield return new TestCaseData("0 10").Returns(new Coordinates(0, 10));
                yield return new TestCaseData("20 0").Returns(new Coordinates(20, 0));
            }
        }
        
        public static IEnumerable ParserTestCases_ParseCommandValidInput
        {
            get
            {
                yield return new TestCaseData('L').Returns(ControlCommand.L);
                yield return new TestCaseData('R').Returns(ControlCommand.R);
                yield return new TestCaseData('M').Returns(ControlCommand.M);
            }
        }
    }
}
