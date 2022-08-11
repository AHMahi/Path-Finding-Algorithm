using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoboNav
{
    public class Robot
    {
        private Node position;
        private Node goalPosition;
        private Map robotMap;
        private MapBuilder _interface = new MapBuilder();

        public Robot(string initialState, string goalState, Map map)//can use this
        {
            Converter ifs = new Converter(initialState);

            List<int> coordinate = ifs.getIntFromString();

            position = new Node(coordinate[0], coordinate[1]);

            ifs = new Converter(goalState);

            coordinate = ifs.getIntFromString();

            goalPosition = new Node(coordinate[0], coordinate[1]);

            robotMap = map;

        }

        public Node Position { get { return position; } }

        //Expandable paths and position notification
        public void notification()
        {
            Console.WriteLine("Current Position: ({0},{1}", position.X, position.Y);

            foreach (Grid g in robotMap.Grids)
            {
                if ((position.X == g.Position.X) && (position.Y == g.Position.Y))
                {
                    foreach (Path p in g.Paths)
                    {
                        Console.WriteLine("Possible paths: ({0},{1})", goalPosition.X, goalPosition.Y);
                    }
                }
            }

            Console.WriteLine("GoalPosition is to reach ({0},{1})", goalPosition.X, goalPosition.Y);

        }

        //Movement Methods

        public string MoveUp()
        {
            return "up";
        }

        public string MoveLeft()
        {
            return "left";
        }

        public string MoveDown()
        {
            return "down";
        }

        public string MoveRight()
        {
            return "right";
        }

        //BFS(Breadth First Search)

        public string BreadthFirstSearch()
        {
            if ((position.X == goalPosition.X) && (position.Y == goalPosition.Y))
            {
                return "Initial position is the solution, no movement is required";
            }

            else
            {
                //Initializing of free nodes and visited nodes Structures
                // Queue stores nodes following FIFO so first element entry will be showed first

                Queue<Node> openNode = new Queue<Node>();
                List<Node> visitedList = new List<Node>();

                //Initializing of expanding node
                Node visitedNode;

                openNode.Enqueue(position);//Enqueue to add value of the open node

                while (openNode.Count != 0)
                {
                    //Exapnsion of the first node inside the queue
                    visitedNode = openNode.Dequeue();//Dequeue to remove  
                    visitedList.Add(visitedNode);

                    //Initialize interface class
                    _interface.DrawNode(Position, goalPosition, visitedNode, robotMap.WallList, robotMap.Width, robotMap.Length);

                    foreach (Grid g in robotMap.Grids)
                    {
                        //Verifying that the expanding grid is still within map bounds
                        if ((visitedNode.X == g.Position.X) && (visitedNode.Y == g.Position.Y))
                        {
                            //Checking availability of the adjacent nodes
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    //Repeated state checking
                                    if ((!visitedList.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                        && !openNode.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                    {
                                        p.Location.Position.ParentNode = new Node(visitedNode);

                                        //Enqueue available paths to the frontier list
                                        openNode.Enqueue(p.Location.Position);
                                    }
                                }
                            }
                        }

                        //If solution exists
                        if ((visitedNode.X == goalPosition.X) && (visitedNode.Y == goalPosition.Y))
                        {
                            return CreateSolution("BFS", visitedList, goalPosition);
                        }

                        /*else { return "No Solution found"; }*/
                    }

                }

                return "No Solution found";
            }
        }

        //DFS (Depth First Search)
        public string DepthFirstSearch()
        {
            if ((position.Y == goalPosition.Y) && (position.X == goalPosition.X))
            {
                return "Initial position is the solution, no movement is required";
            }

            else
            {
                //Initializing of free nodes and visited nodes Structures

                Stack<Node> openNode = new Stack<Node>();//Stack follows LIFO so last element displayed first
                List<Node> visitedList = new List<Node>();

                //Initializing of expanding node
                 Node visitedNode;

                //Pushing initial position in openNode stack
                openNode.Push(position);

                while (openNode.Count != 0)
                {
                    //we visited a node present in the openNode stack and then we pop it out of the stack
                    visitedNode = openNode.Pop();

                    //The visited node gets added to the visitedList and gets expanded
                    visitedList.Add(visitedNode);

                    //expansion test debug
                    //Console.WriteLine("Expanding: " + visitedNode.CoOrdinate);

                    //Initialize interface class and draw all the possible nodes
                    _interface.DrawNode(position, goalPosition, visitedNode, robotMap.WallList, robotMap.Length, robotMap.Width);
                    
                    foreach (Grid g in robotMap.Grids)
                    {
                        //Verifying that the expanding grid is still within map bounds
                        if ((visitedNode.X == g.Position.X) && (visitedNode.Y == g.Position.Y))
                        {
                            //Checking availability of the adjacent nodes
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    //Repeated state checking
                                    if ((!visitedList.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                        && !openNode.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                    {
                                        p.Location.Position.ParentNode = new Node(visitedNode);

                                        //Push available paths to the frontier list
                                        openNode.Push(p.Location.Position);
                                    }
                                }
                            }

                        }

                        //If goal is reached we draw the solution pathway

                        if ((visitedNode.X == goalPosition.X) && (visitedNode.Y == goalPosition.Y))
                        {
                            return CreateSolution("DFS", visitedList, goalPosition);
                        }

                        /*else { return "No Solution found"; }*/
                    }

                }

                return "No Solution found";
            }
            
        }

        //A star Search

        public string AStarSearch()
        {
            if ((position.Y == goalPosition.Y) && (position.X == goalPosition.X))
            {
                return "Initial position is the solution, no movement is required";
            }

            else
            {
                //Initializing of free nodes and visited nodes Structures
                List<Node> openNode = new List<Node>();
                List<Node> visitedList = new List<Node>();

                //Initailize expanding Node
                Node visitedNode;

                //inster initial position in the openNode list
                openNode.Add(position);

                //Initial stationary cost
                position.GCost = 0;

                while (openNode.Count != 0)
                {
                    //sorting the openNode list by f(n) -- the value difference between each node
                    openNode = openNode.OrderBy(s => s.FCost).ToList();

                    //Expanding the first node of the list
                    visitedNode = openNode.First();
                    openNode.Remove(openNode.First());

                    //Add the expanded node to the visited list
                    visitedList.Add(visitedNode);

                    //initializing the interface class and drawing the possible expandable nodes

                    _interface.DrawNode(position, goalPosition, visitedNode, robotMap.WallList, robotMap.Length, robotMap.Width);

                    foreach (Grid g in robotMap.Grids)
                    {
                        //Verifying that the expanding grid is still within map bounds
                        if ((visitedNode.X == g.Position.X) && (visitedNode.Y == g.Position.Y))
                        {
                            //Checking availability of the adjacent nodes
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    //Repeated state checking
                                    if ((!visitedList.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                        && !openNode.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                    {
                                        p.Location.Position.ParentNode = new Node(visitedNode);

                                        //Calculate g(n) as the cost from the start to the current node
                                        p.Location.Position.GCost = visitedNode.GCost + 1;

                                        //Calculate f(n) value
                                        p.Location.Position.FCost = p.Location.Position.GCost + Math.Sqrt(Math.Pow(goalPosition.X - p.Location.Position.X, 2) + Math.Pow(goalPosition.Y - p.Location.Position.Y, 2));

                                        //Add available paths to the frontier list
                                        openNode.Add(p.Location.Position);
                                    }
                                }
                            }

                        }

                        //If goal is reached we draw the solution pathway

                        if ((visitedNode.X == goalPosition.X) && (visitedNode.Y == goalPosition.Y))
                        {
                            return CreateSolution("AStar", visitedList, goalPosition);
                        }

                    }
                }

                return "No Solution";
            }
        }

        //Greedy Best First Search(GBFS)
        public string GreedyBestFirstSearch()
        {
            if ((position.Y == goalPosition.Y) && (position.X == goalPosition.X))
            {
                return "Initial position is the solution, no movement is required";
            }

            else
            {
                //Initializing of free nodes and visited nodes Structures
                List<Node> openNode = new List<Node>();
                List<Node> visitedList = new List<Node>();

                //Initailize expanding Node
                Node visitedNode;

                //inster initial position in the openNode list
                openNode.Add(position);

                while (openNode.Count != 0)
                {
                    //sorting the openNode list by distance of the grid to goal
                    openNode = openNode.OrderBy(s => s.GoalDistance).ToList();

                    //Expanding the first node of the list
                    visitedNode = openNode.First();
                    openNode.Remove(openNode.First());
                    visitedList.Add(visitedNode);

                    //initializing the interface class and drawing the possible expandable nodes

                    _interface.DrawNode(position, goalPosition, visitedNode, robotMap.WallList, robotMap.Length, robotMap.Width);

                    foreach (Grid g in robotMap.Grids)
                    {
                        //Verifying that the expanding grid is still within map bounds
                        if ((visitedNode.X == g.Position.X) && (visitedNode.Y == g.Position.Y))
                        {
                            //Checking availability of the adjacent nodes
                            if (g.Paths.Count != 0)
                            {
                                foreach (Path p in g.Paths)
                                {
                                    //Repeated state checking
                                    if ((!visitedList.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                        && !openNode.Any(x => x.X == p.Location.Position.X && x.Y == p.Location.Position.Y))
                                    {
                                        p.Location.Position.ParentNode = new Node(visitedNode);

                                        //Calculating the heuristic value h(n)
                                        p.Location.Position.GoalDistance = Math.Sqrt(Math.Pow(goalPosition.X - p.Location.Position.X, 2) + Math.Pow(goalPosition.Y - p.Location.Position.Y, 2));

                                        //Add available paths to the frontier list
                                        openNode.Add(p.Location.Position);
                                    }
                                }
                            }

                            //If goal is reached we draw the solution pathway

                            if ((visitedNode.X == goalPosition.X) && (visitedNode.Y == goalPosition.Y))
                            {
                                return CreateSolution("Greedy Best First", visitedList, goalPosition);
                            }
                        }
                    }
                }


                return "No Solution";
            }
        }


        public string CreateSolution(string method, List<Node> expandedNodeList, Node childNode)
        {
            //Initializing dataStructs
            string solution = "";
            List<Node> path = new List<Node>();
            List<string> action = new List<string>();

            expandedNodeList.Reverse();

            foreach (Node p in expandedNodeList)
            {
                if ((p.X == childNode.X) && (p.Y == childNode.Y))
                    path.Add(p);

                if (path.Count() != 0)
                {
                    if ((path.Last().ParentNode.X == p.X) && (path.Last().ParentNode.Y == p.Y))
                    {
                        path.Add(p);
                    }
                }
            }

            path.Reverse();

            //Produce action from path
            for (int i = 0; i < path.Count(); i++)
            {
                if (i == path.Count() - 1)
                {
                    break;
                }

                if (path[i + 1].X == path[i].X + 1)
                {
                    action.Add(MoveRight());
                }

                if (path[i + 1].X == path[i].X - 1)
                {
                    action.Add(MoveLeft());
                }

                if (path[i + 1].Y == path[i].Y + 1)
                {
                    action.Add(MoveDown());
                }

                if (path[i + 1].Y == path[i].Y - 1)
                {
                    action.Add(MoveUp());
                }
            }

            foreach (string a in action)
            {
                solution = solution + a + "; ";
            }

            _interface.DrawPath(position, goalPosition, path, robotMap.WallList, robotMap.Width, robotMap.Length);

            return "Directions taken by robot using " + method + ": " + solution + "  Total Count: " + expandedNodeList.Count();
        }
    }
}






        





   
