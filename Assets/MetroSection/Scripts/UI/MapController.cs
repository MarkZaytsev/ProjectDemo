using MetroSection.Pathfinding;
using System;
using MetroSection.Data;

namespace MetroSection.UI
{
    public class MapController : IDisposable
    {
        private readonly NodeView[] _views;
        private readonly Pathfinder _pathfinder;
        private readonly PathPresenter _presenter;

        private NodeView _from;

        public MapController(Map map, MapBuilder builder)
        {
            _views = builder.Build(map);
            _pathfinder = new Pathfinder(_views);
            _presenter = new PathPresenter(map);

            Subsctibe();
        }

        private void Subsctibe()
        {
            foreach (var view in _views)
                view.OnClick.AddListener(OnViewClicked);
        }

        private void OnViewClicked(NodeView node)
        {
            if (_from == null)
            {
                Select(node);
                return;
            }

            _presenter.Present(_pathfinder.FindPath(_from, node));
            Deselect();
        }

        private void Select(NodeView node)
        {
            _from = node;
            _from.Select();
        }

        private void Deselect()
        {
            _from.Deselct();
            _from = null;
        }

        public void Dispose()
        {
            foreach (var view in _views)
                view.OnClick.RemoveListener(OnViewClicked);
        }
    }
}