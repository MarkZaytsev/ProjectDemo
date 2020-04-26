using System.Collections.Generic;
using UnityEngine;

namespace MetroSection.Pathfinding
{
    public class GraphNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector2 Point { get; set; }
        public List<Edge> Connections { get; set; } = new List<Edge>();

        public float? MinCostToStart { get; set; }
        public GraphNode NearestToStart { get; set; }
        public float StraightLineDistanceToEnd { get; private set; }

        public float CalcStraightLineDistanceTo(GraphNode end) =>
            StraightLineDistanceToEnd = (end.Point - Point).sqrMagnitude;

        public override string ToString() => Name;
    }
}