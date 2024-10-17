using NaughtyAttributes;
using System.Collections.Generic;
using UI;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class GameLoader : MonoBehaviour {

        [SerializeField]
        private LabyrinthSpawner _labyrinthSpawner;

        [SerializeField]
        private NavMeshSurface _navMeshSurface;

        [SerializeField]
        private EnemySpawner _enemySpawner;

        [SerializeField]
        private PlayerSpawner _playerSpawner;

        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private MiniMapCameraPlacer _miniMapCameraPlacer;

        [SerializeField]
        private CollectibleItemsSpawner _collectibleItemsSpawner;

        [SerializeField]
        private EntityPointersSystem _entityPointersSystem;

        [SerializeField]
        private InventoryVisualizer _inventoryVisualizer;

        [SerializeField]
        private DoorsOpeningButton _doorsOpeningButton;

        private SpawnedLabyrinthCellsContainer _cellsContainer;
        private List<Enemy> _enemies;

        private void Awake() {
            Init();
           // Application.targetFrameRate = 30;
        }

        private void Init() {
            _cellsContainer = _labyrinthSpawner.Spawn();
            _miniMapCameraPlacer.Place(_cellsContainer);
            _navMeshSurface.BuildNavMesh();
            _playerSpawner.Init();
            _enemies = _enemySpawner.Spawn(_playerController, _cellsContainer);
            _collectibleItemsSpawner.Spawn(_cellsContainer);
            _entityPointersSystem.Init(_enemies, _collectibleItemsSpawner.SpawnedItems, _playerController);
            _inventoryVisualizer.Init(_playerController);
            _doorsOpeningButton.Init(_cellsContainer.CellsWithDoors, _playerController.transform);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(0);
            }
        }

#if UNITY_EDITOR
        [Button]
        private void Restart() {
            if (!EditorApplication.isPlaying) {
                return;
            }
            _labyrinthSpawner.Respawn();
            _navMeshSurface.BuildNavMesh();
        }
#endif
    }
}
