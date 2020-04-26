using UnityEngine;

namespace FlagSection.Scripts
{
    public class FlagBootstrapper : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _size;

        private void Start() => 
            new CreateMeshCommand(GetComponent<MeshFilter>().mesh, _size).Execute();
    }
}