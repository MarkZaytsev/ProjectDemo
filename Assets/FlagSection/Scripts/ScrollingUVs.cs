using UnityEngine;

namespace FlagSection.Scripts
{
    public class ScrollingUVs : MonoBehaviour
    {
        [SerializeField]
        private int _materialIndex = 0;
        [SerializeField]
        private string _textureName = "_MainTex";
        [SerializeField]
        private Vector2 _speed = new Vector2(1f, 1f);

        private Vector2 _uvOffset = Vector2.zero;
        private Material _mat;

        protected void Awake() => _mat = GetComponent<Renderer>().materials[_materialIndex];

        protected void LateUpdate()
        {
            _uvOffset += _speed * Time.deltaTime;
            _mat.SetTextureOffset(_textureName, _uvOffset);
        }
    }
}