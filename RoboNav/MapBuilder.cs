using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboNav
{
    public class MapBuilder 
    {

        public MapBuilder() { }

        //Drawing the possible expandable node
        public void DrawNode(Node initial, Node goalstate, Node visitedNode, List<Grid> wall,  int mapWidth, int mapLength)
        {
            Console.Clear();

            bool wallDrawn = false;

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    if ((initial.X == j) && (initial.Y == i))
                    {
                        Console.Write("|i");
                        continue;
                    }

                    if ((goalstate.X == j) && (goalstate.Y == i))
                    {
                        Console.Write("|g");
                        continue;
                    }

                    if ((visitedNode.X == j) && (visitedNode.Y == i))
                    {
                        Console.Write("|*");
                        continue;
                    }

                    foreach (Grid g in wall)
                    {
                        if ((g.WallPresent == true) && (g.Position.X == j) && (g.Position.Y == i))
                        {
                            Console.Write("|w");
                            wallDrawn = true;
                            break;
                        }
                        wallDrawn = false;
                    }

                    if (wallDrawn == false)
                        Console.Write("| ");
                }
                Console.WriteLine("|");
            }

            if ((visitedNode.X == goalstate.X) && (visitedNode.Y == goalstate.Y))
            {
                Console.WriteLine("Solution found at the node: ({0},{1})", visitedNode.X, visitedNode.Y);
            }
            else
            {
                Console.WriteLine("Expanding the node: ({0},{1})", visitedNode.X, visitedNode.Y);
            }
        }

        //Drawing the path of a certain solution
        public void DrawPath(Node initialState, Node goalstate, List<Node> path, List<Grid> wall, int mapWidth, int mapLength)
        {
            
            Console.Clear();

            bool wallDrawn = false;

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    if ((initialState.X == j) && (initialState.Y == i))
                    {
                        Console.Write("|i");

                        continue;
                    }

                    if ((goalstate.X == j) && (goalstate.Y == i))
                    {
                        Console.Write("|g");

                        continue;
                    }

                    if (path.Any(x => x.X == j && x.Y == i))
                    {
                        Console.Write("|*");

                        continue;
                    }

                    foreach (Grid g in wall)
                    {
                        if ((g.WallPresent == true) && (g.Position.X == j) && (g.Position.Y == i))
                        {
                            Console.Write("|w");

                            wallDrawn = true;

                            break;
                        }

                        wallDrawn = false;
                    }

                    if (wallDrawn == false) 
                    {
                        Console.Write("| ");
                    }
                        
                }

                Console.WriteLine("|");
            }

            Console.WriteLine();

        }
    }
}
