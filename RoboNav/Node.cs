using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboNav
{
    public class Node
    {
        private int x;
        private int y;
        private double fCost;
        private double gCost;
        private double goalDistance;
        private Node parentNode;

        //Constructor
        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        //Constructor Overloading
        public Node(Node parent)
        {
            x = parent.X;
            y = parent.Y;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public double GoalDistance
        {
            get
            {
                return goalDistance;
            }

            set 
            {
                goalDistance = value;
            }
        }

        public double FCost
        {
            get
            {
                return fCost;
            }

            set { fCost = value; }
        }

        public double GCost
        {
            get
            {
                return gCost;
            }

            set { gCost = value; }
        }

        public Node ParentNode
        {
            get
            {
                return parentNode;
            }

            set
            {
                parentNode = value;
            }
        }

        public string CoOrdinate
        {
            get
            {
                return "(" + X + "," + Y + ")";
            }
        }
    }
}
