using MetroSection.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MetroSection.UI
{
    public class PathPresenter
    {
        private readonly Map _map;
        private IReadOnlyList<NodeView> _path;
        private StringBuilder _stringer;
        private Route _activeRoute;

        public PathPresenter(Map map) => _map = map;

        //TODO: Should be path with less transfets more preferable?
        public void Present(IReadOnlyList<NodeView> path)
        {
            _path = path;

            if (!PathIsValid())
                return;
            
            _stringer = new StringBuilder($"Path: {_path[0]}");

            DetectActiveRouteForStation(0);
            for (var i = 1; i < path.Count; i++)
            {
                CheckTransfer(i);
                HandleStationVisited(i);
            }

            Debug.Log(_stringer.ToString());
        }

        private bool PathIsValid()
        {
            switch (_path.Count)
            {
                case 0:
                    Debug.Log("Invalid input");
                    return false;
                case 1:
                    Debug.Log("Stayed on station");
                    return false;
            }

            return true;
        }

        private void DetectActiveRouteForStation(int stationIx) => 
            _activeRoute = FindActiveRoute(stationIx);

        private Route FindActiveRoute(int stationIx)
        {
            if (stationIx >= _path.Count - 1)
                return _activeRoute;

            var possibleRoutes = _map.RoutesByStationId(_path[stationIx].Id);
            var nextPossibleRoutes = _map.RoutesByStationId(_path[stationIx + 1].Id);
            return possibleRoutes.FirstOrDefault(r => nextPossibleRoutes.Contains(r));
        }

        private void CheckTransfer(int stationIx)
        {
            var possibleRoutes = _map.RoutesByStationId(_path[stationIx].Id);
            if (possibleRoutes.Contains(_activeRoute))
                HandleStayedOnRoute();
            else
                HandleRouteTransfer(stationIx);
        }
        
        private void HandleStayedOnRoute() => _stringer.Append(", ");

        private void HandleStationVisited(int stationIx) => _stringer.Append(_path[stationIx].ToString());

        private void HandleRouteTransfer(int stationIx)
        {
            _stringer.Append(" [T], ");
            DetectActiveRouteForStation(stationIx);
        }
    }
}