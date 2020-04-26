using System.Collections.Generic;
using System.Linq;

namespace MetroSection.Pathfinding
{
    public class AStar
    {
        private readonly GraphNode[] _graphNodes;
        private GraphNode _start;
        private GraphNode _end;

        public AStar(GraphNode[] graphNodes) => _graphNodes = graphNodes;

        public GraphNode[] FindPath(GraphNode start, GraphNode end)
        {
            _start = start;
            _end = end;

            InitNodes();
            Search();

            return BuildShortestPath();
        }

        private void InitNodes()
        {
            foreach (var node in _graphNodes)
            {
                node.CalcStraightLineDistanceTo(_end);
                node.MinCostToStart = null;
                node.NearestToStart = null;
            }
        }

        private void Search()
        {
            _start.MinCostToStart = 0;
            var visited = new List<GraphNode>();
            var prioQueue = new List<GraphNode> { _start };
            do
            {
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);

                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedGraphNode;
                    if (visited.Contains(childNode))
                        continue;

                    if (childNode.MinCostToStart != null &&
                        !(node.MinCostToStart + cnn.Cost < childNode.MinCostToStart))
                        continue;

                    childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                    childNode.NearestToStart = node;
                    if (!prioQueue.Contains(childNode))
                        prioQueue.Add(childNode);
                }

                visited.Add(node);

                if (node == _end)
                    return;

            } while (prioQueue.Any());
        }

        private GraphNode[] BuildShortestPath()
        {
            var shortestPath = new List<GraphNode> { _end };
            var node = _end;

            while (true)
            {
                if (node.NearestToStart == null)
                {
                    shortestPath.Reverse();
                    return shortestPath.ToArray();
                }

                shortestPath.Add(node.NearestToStart);
                node = node.NearestToStart;
            }
        }
    }
}