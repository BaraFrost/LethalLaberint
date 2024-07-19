using UnityEngine;

namespace Game {

    public class MiniMapCameraPlacer : MonoBehaviour {

        [SerializeField]
        private Camera _miniMapCamera;

        [SerializeField]
        private float _additionalSize;

        public void Place(SpawnedLabyrinthCellsContainer cellsContainer) {
            var cellsToRender = cellsContainer.AvailableCells;
            var positionSum = Vector3.zero;
            foreach (var cell in cellsToRender) {
                positionSum += cell.transform.position;
            }
            positionSum /= cellsToRender.Count;
            _miniMapCamera.transform.position = new Vector3(positionSum.x, _miniMapCamera.transform.position.y, positionSum.z);
            _miniMapCamera.orthographicSize = cellsContainer.LabyrinthSize + _additionalSize;
        }
    }
}

