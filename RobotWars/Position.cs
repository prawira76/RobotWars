using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars
{
    public class Position
    {
        public Coordinates Coordinates { get; set; }
        public Direction Direction { get; set; }

        public Position(Coordinates coordinates, Direction direction)
        {
            Direction = direction;
            Coordinates = coordinates;
        }

        public override string ToString()
        {
            return $"{Coordinates.X} {Coordinates.Y} {Direction}";
        }

        public override bool Equals(object obj)
        {
            var item = obj as Position;
            if (item == null)
                return false;

            return this.Coordinates.Equals(item.Coordinates) && this.Direction.Equals(item.Direction);
        }
    }
}
