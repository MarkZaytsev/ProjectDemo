namespace MetroSection.Pathfinding
{
    public class Edge
    {
        public float Cost { get; set; }
        public GraphNode ConnectedGraphNode { get; set; }

        public override string ToString() => $"-> {ConnectedGraphNode}";
    }
}