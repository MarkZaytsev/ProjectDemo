using MetroSection.UI;
using System.Linq;

namespace MetroSection.Pathfinding
{
    public class Pathfinder
    {
        private readonly NodeView[] _views;
        private readonly GraphNode[] _graphNodes;
        private readonly AStar _star;

        public Pathfinder(NodeView[] views)
        {
            _views = views;
            _graphNodes = BuildGraphNodes();
            _star = new AStar(_graphNodes);
        }

        private GraphNode[] BuildGraphNodes()
        {
            var graphNodes = _views.Select(v => new GraphNode()
            {
                Id = v.Id,
                Name = v.name,
                Point = v.transform.position
            }).ToArray();

            foreach (var gNode in graphNodes)
            {
                var node = _views.FirstOrDefault(v => v.Id == gNode.Id);
                gNode.Connections = node.Connections
                    .Select(v => new Edge()
                    {
                        ConnectedGraphNode = graphNodes.FirstOrDefault(n => n.Id == v.Id),
                        Cost = 1
                    }).ToList();
            }

            return graphNodes;
        }

        public NodeView[] FindPath(NodeView start, NodeView end)
        {
            var path = _star.FindPath(ViewToGraph(start), ViewToGraph(end));
            return path.Select(GraphToView).ToArray();
        }

        private GraphNode ViewToGraph(NodeView view) => _graphNodes.FirstOrDefault(n => n.Id == view.Id);

        private NodeView GraphToView(GraphNode graphNode) => _views.FirstOrDefault(v => v.Id == graphNode.Id);
    }
}