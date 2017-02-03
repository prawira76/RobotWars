using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars
{
    public enum Direction { N = 1, S = 2, W = 4, E = 8};
    public enum ControlCommand { L = 1, R = 2, M = 4 };

    public class Robot
    {
        private Position _startPosition;
        private Position _endPosition;
        private Position _currentPosition;
        private Coordinates _bounds;

        public Robot(string name)
        {
            Name = name;
        }

        public bool ParseAndExecuteInstructions(string instructions)
        {
            // insideBounds is false when the robot moves outside the arena. 
            //  When this happens, the instructions are terminated early
            var insideBounds = false;
            foreach (var c in instructions)
            {
                var command = Parser.ParseCommand(c);
                insideBounds = ExecuteCommand(command);

                if (!insideBounds)
                    break;
            }

            _endPosition = _currentPosition;
            return insideBounds;
        }

        public bool Init(Coordinates bounds, string startPosition)
        {
            _startPosition = Parser.ParsePosition(startPosition);
            _currentPosition = StartPosition;

            // bounds are the boundaries of the arena
            _bounds = bounds;

            // The StartPosition should be within the boundaries of the arena
            if (!CheckBounds())
                return false;

            return true;
        }
       
        private bool ExecuteCommand(ControlCommand command)
        {
            var result = true;
            switch (command)
            {
                case ControlCommand.L:
                    TurnLeft();
                    break;
                case ControlCommand.R:
                    TurnRight();
                    break;
                case ControlCommand.M:
                    result = Move();
                    break;
                default:
                    // TODO throw exception
                    break;
            }

            return result;
        }
        
        private void TurnLeft()
        {
            switch (_currentPosition.Direction)
            {
                case Direction.N:
                    _currentPosition.Direction = Direction.W;
                    break;
                case Direction.S:
                    _currentPosition.Direction = Direction.E;
                    break;
                case Direction.W:
                    _currentPosition.Direction = Direction.S;
                    break;
                case Direction.E:
                    _currentPosition.Direction = Direction.N;
                    break;
                default:
                    // TODO throw exception
                    break;
            }
        }

        private void TurnRight()
        {
            switch (_currentPosition.Direction)
            {
                case Direction.N:
                    _currentPosition.Direction = Direction.E;
                    break;
                case Direction.S:
                    _currentPosition.Direction = Direction.W;
                    break;
                case Direction.W:
                    _currentPosition.Direction = Direction.N;
                    break;
                case Direction.E:
                    _currentPosition.Direction = Direction.S;
                    break;
                default:
                    // TODO throw exception
                    break;
            }
        }

        private bool Move()
        {
            switch (_currentPosition.Direction)
            {
                case Direction.N:
                    _currentPosition.Coordinates.Y++;
                    break;
                case Direction.S:
                    _currentPosition.Coordinates.Y--;
                    break;
                case Direction.W:
                    _currentPosition.Coordinates.X--;
                    break;
                case Direction.E:
                    _currentPosition.Coordinates.X++;
                    break;
                default:
                    // TODO throw exception
                    break;
            }

            if (!CheckBounds())
            {
                return false;
            }

            return true;
        }

        private bool CheckBounds()
        {
            // Set the X or Y coordinate to the boundary of the arena if necessary
            if (_currentPosition.Coordinates.Y < 0)
            {
                _currentPosition.Coordinates.Y = 0;
                return false;
            }
            if (_currentPosition.Coordinates.Y > _bounds.Y)
            {
                _currentPosition.Coordinates.Y = _bounds.Y;
                return false;
            }
            if (_currentPosition.Coordinates.X < 0)
            {
                _currentPosition.Coordinates.X = 0;
                return false;
            }
            if (_currentPosition.Coordinates.X > _bounds.X)
            {
                _currentPosition.Coordinates.X = _bounds.X;
                return false;
            }
            
            return true;
        }

        public Position StartPosition
        {
            get { return _startPosition; }
        }

        public Position EndPosition
        {
            get { return _endPosition; }
        }

        public Position CurrentPosition
        {
            get { return _currentPosition; }
        }

        public Coordinates Bounds
        {
            get { return _bounds; }
        }

        public string Name { get; set; }
    }
}
