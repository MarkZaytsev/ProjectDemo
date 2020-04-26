using MetroSection.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MetroSection.UI
{
    public class ClickEvent : UnityEvent<NodeView>
    {

    }

    public class NodeView : MonoBehaviour
    {
        public int Id => _data.Id;
        
        public NodeView[] Connections;

        public readonly ClickEvent OnClick = new ClickEvent();

        private Station _data;
        private Button _btn;

        public void SetParent(Transform p) => transform.parent = p;

        public void SetLocalPosition(Vector2 pos) => transform.localPosition = pos;

        public void ResetScale() => transform.localScale = Vector3.one;

        public void Initialize(Station data)
        {
            _data = data;

            GetComponentInChildren<Text>().text = name = _data.Name;

            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(() => OnClick.Invoke(this));
        }

        public int[] GetConnectionsIds() => _data.Connections.Where(s => s).Select(s => s.Id).ToArray();

        public void Select() => _btn.Select();

        public void Deselct() => EventSystem.current.SetSelectedGameObject(null);

        public override string ToString() => _data.Name;
    }
}