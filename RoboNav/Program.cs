using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboNav
{
    public class Program
    {
        static void Main(string[] args)
        {
            Initializer loadFile = new Initializer("D:\\C#\\RoboNav\\RoboNav\\RoboNav\\RobotNav-test.txt");

            loadFile.readData();
            
            Map map = new Map(loadFile.SizeMap, loadFile.WallInfo);

            Robot robot = new Robot(loadFile.InitialState, loadFile.GoalState, map);

            string ChooseMethod(int methodNum)
            {
                string methodExcecutable;

                switch (methodNum)
                {
                    case 1:

                        methodExcecutable = robot.BreadthFirstSearch();
                        break;

                    case 2:

                        methodExcecutable = robot.DepthFirstSearch();
                        break;

                    case 3:

                        methodExcecutable = robot.AStarSearch();
                        break;

                    case 4:

                        methodExcecutable = robot.GreedyBestFirstSearch();
                        break;

                    default:

                        methodExcecutable = "invalid search Method";
                        break;
                }

                return methodExcecutable;
            }

            Console.WriteLine("Map information");
            loadFile.MapInfo();
            Console.WriteLine("..................................");
            Console.WriteLine("Select search Method");
            Console.WriteLine("press 1 = Breadth First Search");
            Console.WriteLine("press 2 = Depth First Search");
            Console.WriteLine("press 3 = A start Search");
            Console.WriteLine("press 4 = Greedy Best First Search");


            string val;

            val = Console.ReadLine();

            // use of System Diagnostics library

            Stopwatch mytimer = new Stopwatch();

            mytimer.Start();
            
            Console.WriteLine(ChooseMethod(Convert.ToInt32(val)));

            mytimer.Stop();

            Console.WriteLine("ALgorithm Execution time: {0}", mytimer.Elapsed);

            loadFile.ExitFile();

            Console.ReadLine();

        }
    }
}
