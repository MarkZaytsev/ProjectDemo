using System.Collections.Generic;
using Common.FrostLib;
using UnityEngine;

namespace FlagSection.Scripts
{
    public class CreateMeshCommand : ICommand
    {
        private readonly Mesh _mesh;
        private readonly Vector2Int _size;

        public CreateMeshCommand(Mesh mesh, Vector2Int size)
        {
            _size = size;
            _mesh = mesh;
        }

        public void Execute()
        {
            _mesh.vertices = CreateVertices();
            _mesh.triangles = CreateTriangles();

            var uvs = MapUVs(_mesh.vertices);

            _mesh.uv = uvs;
        }

        private Vector3[] CreateVertices()
        {
            var vertices = new Vector3[(_size.x + 1) * (_size.y + 1)];
            for (int i = 0, y = 0; y <= _size.y; y++)
            {
                for (var x = 0; x <= _size.x; x++, i++)
                    vertices[i] = new Vector3(x, y);
            }

            return vertices;
        }

        private int[] CreateTriangles()
        {
            var triangles = new int[_size.x * _size.y * 6];
            for (int ti = 0, vi = 0, y = 0; y < _size.y; y++, vi++)
            {
                for (var x = 0; x < _size.x; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + _size.x + 1;
                    triangles[ti + 5] = vi + _size.x + 2;
                }
            }

            return triangles;
        }

        private Vector2[] MapUVs(IReadOnlyList<Vector3> vertices)
        {
            var uvs = new Vector2[vertices.Count];
            for (var index = 0; index < uvs.Length; index++)
            {
                var vert = vertices[index];
                uvs[index] = new Vector2(vert.x / _size.x, vert.y / _size.y);
            }

            return uvs;
        }
    }
}