using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Game {

    public class PathDrawer : MonoBehaviour {

        [SerializeField]
        private LineRenderer lineRenderer;
        [SerializeField]
        private float _minDistance;

        private NavMeshPath path;
        private Transform _target;
        private Transform _start;

        public void Init(Transform target, Transform start) {
            _target = target;
            _start = start;
            path = new NavMeshPath();
        }

        private void Update() {
            if (NavMesh.CalculatePath(_start.position, _target.position, NavMesh.AllAreas, path)) {
                DrawPath(path, _target);
            }
        }

        private void DrawPath(NavMeshPath path, Transform target) {
            var corners = path.corners;
            var pathLenght = 0f;
            for (int i = 0; i < corners.Length - 1; i++) {
                pathLenght += Vector3.Distance(corners[i], corners[i + 1]);
            }
            if (pathLenght < _minDistance) {
                lineRenderer.enabled = false;
                return;
            }

            lineRenderer.enabled = true;
            var gridPath = corners;
            gridPath[^1] = target.position;
            lineRenderer.positionCount = gridPath.Length;
            lineRenderer.SetPositions(gridPath);
        }

        /*private Vector3[] ConvertToGrid(Vector3[] pathCorners, Transform target) {
            var gridPositions = new Vector3[pathCorners.Length];

            for (int i = 0; i < pathCorners.Length; i++) {
                gridPositions[i] = WorldToGrid(pathCorners[i]);
            }
            gridPositions[^1] = target.position;
            return gridPositions;
        }

        private Vector3 WorldToGrid(Vector3 worldPosition) {
            float x = Mathf.Round(worldPosition.x / cellSize) * cellSize;
            float z = Mathf.Round(worldPosition.z / cellSize) * cellSize;

            return new Vector3(x, worldPosition.y, z);
        }*/
    }
}
