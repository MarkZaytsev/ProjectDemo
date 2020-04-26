using Common.FrostLib;
using MetroSection.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroSection
{
    public class ValidateConnectionsCommand : ICommand
    {
        private readonly Route[] _routes;

        public ValidateConnectionsCommand(Route[] routes) => _routes = routes;

        public void Execute()
        {
            var stations = CollectStations();
            foreach (var station in stations)
            {
                foreach (var connection in station.Connections)
                {
                    if(connection.IsConnected(station))
                        continue;

                    throw new Exception($"{station} has one way connection with {connection}");
                }
            }
        }

        private Station[] CollectStations()
        {
            var stations = new List<Station>();
            foreach (var route in _routes)
                stations.AddRange(route.Stations.Where(s => !stations.Contains(s)).ToArray());

            return stations.ToArray();
        }
    }
}