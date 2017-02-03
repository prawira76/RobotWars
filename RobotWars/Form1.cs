using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotWars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grid.Rows.Clear();

            var robot1 = new Robot("Robot1");
            var robot2 = new Robot("Robot2");

            var inputLines = txtBox.Lines;
            if (inputLines.Length != 5)
            {
                MessageBox.Show(this, "Invalid number of input lines (expected: 5 lines)", "Invalid input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // First line is the upper right coordinates of the arena 
            var arenaEndCoordinates = Parser.ParseCoordinates(inputLines[0]);
            InitGrid(arenaEndCoordinates.Y + 1, arenaEndCoordinates.X + 1);
            
            // Second line is the Start Position of Robot1
            if (SetUpRobot(robot1, arenaEndCoordinates, inputLines[1]))
            {
                // Third line are the instructions for Robot1
                MoveRobot(robot1, inputLines[2]);
            }

            // Fourth line is the Start Position of Robot2
            if (SetUpRobot(robot2, arenaEndCoordinates, inputLines[3]))
            {
                // Fifth line are the instructions for Robot2
                MoveRobot(robot2, inputLines[4]);
                
                if (robot1.EndPosition.Equals(robot2.EndPosition))
                {
                    var cellEndPosition = TransformPositionToGridCell(robot1.EndPosition, robot1.Bounds);
                    grid[cellEndPosition.ColumnNumber, cellEndPosition.RowNumber].Value =
                        $"CLASH ({robot1.EndPosition})";
                }
            }
        }

        private void InitGrid(int rows, int cols)
        {
            grid.RowCount = rows;
            grid.ColumnCount = cols;
            grid.Invalidate();
        }

        private bool SetUpRobot(Robot robot, Coordinates bounds, string startPosition)
        {
            if (!robot.Init(bounds, startPosition))
            {
                MessageBox.Show(this, $"StartPosition of {robot.Name} ({robot.StartPosition}) is outside the boundaries of the arena.",
                    "Out of bounds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var cellStartPosition = TransformPositionToGridCell(robot.StartPosition, bounds);
            grid[cellStartPosition.ColumnNumber, cellStartPosition.RowNumber].Value = $"{robot.Name} Start ({robot.StartPosition})";

            return true;
        }

        private void MoveRobot(Robot robot, string instructions)
        {
            if (!robot.ParseAndExecuteInstructions(instructions))
            {
                MessageBox.Show(this,
                    $"{robot.Name} got out of bounds. Instructions are terminated early.", "Out of bounds", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            var cellEndPosition = TransformPositionToGridCell(robot.EndPosition, robot.Bounds);
            grid[cellEndPosition.ColumnNumber, cellEndPosition.RowNumber].Value = $"{robot.Name} End ({robot.EndPosition})";
        }

        private DataGridCell TransformPositionToGridCell(Position position, Coordinates bounds)
        {
            // Since the lower-left coordinates of the arena are assumed to be (0, 0) and the grid starts at the top-left with (0, 0),
            //  we need to transform the arena position to the proper grid cell.
            return new DataGridCell(Math.Abs(position.Coordinates.Y - bounds.Y), position.Coordinates.X);
        }
    }
}
