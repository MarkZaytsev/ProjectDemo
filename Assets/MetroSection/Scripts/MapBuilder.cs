using System;
using System.Collections.Generic;
using System.Linq;
using MetroSection.Data;
using MetroSection.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MetroSection
{
    public class MapBuilder
    {
        [Serializable]
        public struct Context
        {
            public NodeView NodePrefab;
            public Transform NodeContainer;

            public ConnectionView ConnectionPrefab;
            public Transform ConnectionsContainer;

            public float Scale;
        }

        private readonly Context _context;
        private readonly List<NodeView> _views = new List<NodeView>();

        public MapBuilder(Context context) => _context = context;

        public NodeView[] Build(Map map)
        {
            _views.Clear();

            CreateViews(map.Routes);
            SetupConnections();
            BuildConnections(map.Routes);

            return _views.ToArray();
        }

        private void CreateViews(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
                _views.AddRange(CreateRoute(route));
        }

        private IEnumerable<NodeView> CreateRoute(Route route)
        {
            var idx = 0;
            var views = new NodeView[route.Stations.Length];
            foreach (var station in route.Stations)
            {
                var view = Object.Instantiate(_context.NodePrefab);
                view.SetParent(_context.NodeContainer);
                view.SetLocalPosition(station.Position * _context.Scale);
                view.ResetScale();
                view.Initialize(station);

                views[idx++] = view;
            }

            return views;
        }

        private void SetupConnections()
        {
            foreach (var view in _views)
            {
                view.Connections = view.GetConnectionsIds().Select(id => _views.FirstOrDefault(v => v.Id == id))
                    .ToArray();
            }
        }

        private void BuildConnections(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                for (var i = 0; i < route.Stations.Length - 1; i++)
                {
                    var v1 = ViewByStation(route.Stations[i]);
                    var v2 = ViewByStation(route.Stations[i+1]);
                    var con = Object.Instantiate(_context.ConnectionPrefab, Vector3.zero, Quaternion.identity, _context.ConnectionsContainer);
                    con.Initialize(v1, v2, route.Color);
                }
            }
        }

        private NodeView ViewByStation(Station station) => _views.FirstOrDefault(v => v.Id == station.Id);
    }
}