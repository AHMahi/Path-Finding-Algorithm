using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboNav
{
    public class Map
    {
        List<Grid> grids = new List<Grid>();
        private int width;
        private int length;
        private List<string> wall;
        private List<Grid> wallList = new List<Grid>();


        //Map constructor
        public Map(string mapSize, List<string> mapWall)
        {
            Converter ifs = new Converter(mapSize);

            List<int> coordinate = ifs.getIntFromString();

            width = coordinate[0];
            length = coordinate[1];
            wall = mapWall;
            drawMap();
            //test();
            
        }
        public List<Grid> Grids
        {
            get
            {
                return grids;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }

        public List<Grid> WallList
        {
            get
            {
                return wallList;
            }
        }

    /*    public void test()
        {
            Console.WriteLine("hel");
        }*/

        public void drawMap()
        {
            for (int i = 0; i < width; i++)
            {
                //Console.WriteLine("drawmap");
                for (int j = 0; j < length; j++)
                {
                    grids.Add(new Grid(new Node(j, i), false));
                }
            }

            for (int i = 0; i < wall.Count; i++)
            {
                drawWall(wall[i]);
            }

            drawPath();
        }
        
        public void drawWall(string aWall)
        {
            Converter ifs = new Converter(aWall);//an object of converter class

            List<int> coordinate = ifs.getIntFromString();

            for (int j = coordinate[1]; j < coordinate[1] + coordinate[3]; j++)
            {
                for (int i = coordinate[0]; i < coordinate[0] + coordinate[2]; i++)
                {
                    int index = grids.FindIndex(x => (x.Position.X == i) && (x.Position.Y == j));
                    grids[index].WallPresent = true;
                }
            }

            foreach (Grid g in grids)
            {
                if (g.WallPresent == true)
                {
                    wallList.Add(g);
                }
            }

        }

        public void drawPath()
        {
            //Console.WriteLine("The length is {0}, Width is {1}", length, width);
            for ( int i = 0; i < grids.Count; i++ )
            {
                if (!grids[i].WallPresent)
                {
                    for( int j = 0; j < width; j++)
                    {
                        if ((i >= j * length) && (i < (j + 1) * length -1))
                        {
                            //Console.WriteLine("hell");
                            grids[i].Paths.Add(new Path(grids[i + 1]));
                        }
                    }

                    for (int j = 0; j < width; j++)
                    {
                        if ((i > j * length) && (i < (j + 1) * length ))
                        {
                            //Console.WriteLine("hell");
                            grids[i].Paths.Add(new Path(grids[i - 1]));
                        }
                    }

                    if ( i < length * width - length)
                    {
                        if (!grids[i + length].WallPresent)
                        {
                            //Console.WriteLine("hell");
                            grids[i].Paths.Add(new Path(grids[i + length]));
                        }
                    }

                    if ( i > length - 1)
                    {
                        if (!grids[i - length].WallPresent)
                        {
                            grids[i].Paths.Add(new Path(grids[i - length]));
                        }
                    }

                }
            }

            //Delete the paths with obstacles

            foreach (Grid g in grids)
            {
                for (int i = 0; i < g.Paths.Count; i++)
                {
                    if (g.Paths[i].Location.WallPresent == true)
                    {
                        g.Paths.Remove(g.Paths[i]);
                    }
                }
            }

        }

        // This method can be used for debugging and checking the position of cells and walls 
        public void printMap()
        {

            foreach (Grid g in grids) 
            {
                //Console.WriteLine("hello");
                Console.WriteLine("Cell: ({0},{1}), wall: {2}", g.Position.X, g.Position.Y, g.WallPresent);
                foreach (Path p in g.Paths)
                {
                    Console.WriteLine(p.Location.Position.CoOrdinate);
                }
            }
        }

    }

}
