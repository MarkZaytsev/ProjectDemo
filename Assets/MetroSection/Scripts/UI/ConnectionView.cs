using UnityEngine;

namespace MetroSection.UI
{
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectionView : MonoBehaviour
    {
        private NodeView _nodeA;
        private NodeView _nodeB;

        public void Initialize(NodeView a, NodeView b, Color color)
        {
            _nodeA = a;
            _nodeB = b;

            name = $"{_nodeA} -> {_nodeB}";

            SetupLine(color);
        }

        private void SetupLine(Color color)
        {
            var ren = GetComponent<LineRenderer>();

            ren.positionCount = 2;
            ren.SetPosition(0, _nodeA.transform.position);
            ren.SetPosition(1, _nodeB.transform.position);

            ren.startColor = ren.endColor = color;
        }
    }
}