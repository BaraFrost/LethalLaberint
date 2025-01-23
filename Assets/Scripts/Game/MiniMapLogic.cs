using UnityEngine;

namespace Game {

    public class MiniMapLogic : MonoBehaviour {

        [SerializeField]
        private MiniMapCameraPlacer _miniMapCameraPlacer;

        [SerializeField]
        private GameObject _miniMapImage;

        public void Init(SpawnedLabyrinthCellsContainer cellsContainer) {
            _miniMapCameraPlacer.Place(cellsContainer);
        }

        public void ActivateMiniMap() {
            _miniMapCameraPlacer.gameObject.SetActive(true);
            _miniMapImage.SetActive(true);
        }

        public void DeactivateMiniMap() {
            _miniMapCameraPlacer.gameObject.SetActive(false);
            _miniMapImage.SetActive(false);
        }
    }
}

