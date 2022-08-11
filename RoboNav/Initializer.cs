using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RoboNav
{
    public class Initializer
    {
        private string map;
        private string initialState;
        private string goalState;
        private List<string> wall = new List<string>();
        private System.IO.StreamReader inputFile;
        private string line;

        public Initializer(string file)
        {
            inputFile = new System.IO.StreamReader(file);
        }


        //Properties
        public string SizeMap
        {
            get
            {
                return map;
            }
        }

        public string InitialState
        {
            get
            {
                return initialState;
            }
        }

        public string GoalState
        {
            get
            {
                return goalState;
            }
        }

        public List<string> WallInfo => wall;

        public void readData()
        {
            int counter = 0;

            while((line = inputFile.ReadLine()) != null)
            {
                if(counter == 0)
                {
                    map = line;
                }

                if (counter == 1)
                {
                    initialState = line;
                }

                if (counter == 2)
                {
                    goalState = line;
                }

                if (counter >= 3)
                {
                    wall.Add(line);
                }

                counter++;
            }
        }

        public void MapInfo()
        {
            Console.WriteLine("Map Dimension: {0}", map);
            Console.WriteLine("Initial state: {0}", initialState);
            Console.WriteLine("Goal state: {0}", goalState);

            foreach (string w in wall)
            {
                Console.WriteLine("Wall: {0}", w);
            }
        }

        public void ExitFile()
        {
            inputFile.Close();
        }
    }
}
