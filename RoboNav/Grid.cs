using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboNav
{
    public class Grid
    {
        private Node position;
        private bool _wallPresent;
        private List<Path> paths = new List<Path>();

        public Grid(Node currentPos, bool wallPresent)
        {
            position = currentPos;
            _wallPresent = wallPresent;
        }

        public Node Position
        {
            get { return position; }
        }

        public bool WallPresent
        {
            get { return _wallPresent; }

            set { _wallPresent = value; }
        }

        public List<Path> Paths
        {
            get { return paths; }

            set { paths = value; }
        }
    }
}
