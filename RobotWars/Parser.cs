using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars
{
    public class Parser
    {
        public static Position ParsePosition(string position)
        {
            var parameters = position.Split(' ');
            if (parameters.Length != 3)
            {
                throw new ArgumentException(
                    $"Cannot parse Position string {position}. Parameter count = {parameters.Length} (expected: 3).");
            }

            // First parse the direction at the end of the string
            var direction = ParseDirection(parameters[2]);

            // Then remove the direction string to get the string for the coordinates
            var coordinatesString = position.Remove(position.Length-2);

            var coordinates = ParseCoordinates(coordinatesString);

            return new Position(coordinates, direction);
        }

        public static Coordinates ParseCoordinates(string coordinates)
        {
            var parameters = coordinates.Split(' ');
            if (parameters.Length != 2)
            {
                throw new ArgumentException(
                    $"Cannot parse Coordinates string {coordinates}. Parameter count = {parameters.Length} (expected: 2).");
            }

            // X and Y coordinates must be positive integers
            int x, y = 0;
            var xParseResult = int.TryParse(parameters[0], out x);
            var yParseResult = int.TryParse(parameters[1], out y);
            if (!xParseResult || x < 0)
            {
                throw new ArgumentException(
                   $"Cannot parse Coordinates string {coordinates}. Parameter \"{parameters[0]}\" is not a positive integer.");
            }
            if (!yParseResult || y < 0)
            {
                throw new ArgumentException(
                   $"Cannot parse Coordinates string {coordinates}. Parameter \"{parameters[1]}\" is not a positive integer.");
            }

            return new Coordinates(x, y);
        }

        public static Direction ParseDirection(string direction)
        {
            var result = Direction.N;
            switch (direction)
            {
                case "N":
                    result = Direction.N;
                    break;
                case "S":
                    result = Direction.S;
                    break;
                case "W":
                    result = Direction.W;
                    break;
                case "E":
                    result = Direction.E;
                    break;
                default:
                    throw new ArgumentException($"Unknown direction \"{direction}\" (expected: \"N, S, W, E\")." );
            }

            return result;
        }

        public static ControlCommand ParseCommand(char command)
        {
            var result = ControlCommand.L;
            switch (command)
            {
                case 'L':
                    result = ControlCommand.L;
                    break;
                case 'R':
                    result = ControlCommand.R;
                    break;
                case 'M':
                    result = ControlCommand.M;
                    break;
                default:
                    throw new ArgumentException($"Unknown instruction \"{command}\" (expected: \"L, R, M\").");
            }

            return result;
        }
    }
}
