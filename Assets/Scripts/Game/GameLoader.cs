using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

namespace Game {

    public class GameLoader : MonoBehaviour {

        [SerializeField]
        private LabyrinthSpawner _labyrinthSpawner;

        [SerializeField]
        private NavMeshSurface _navMeshSurface;

        [SerializeField]
        private EnemySpawner _enemySpawner;

        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private MiniMapCameraPlacer _miniMapCameraPlacer;

        [SerializeField]
        private CollectibleItemsSpawner _collectibleItemsSpawner;

        private SpawnedLabyrinthCellsContainer _cellsContainer;
        private List<Enemy> _enemies;

        private void Awake() {
            Init();
        }

        private void Init() {
            _cellsContainer = _labyrinthSpawner.Spawn();
            _miniMapCameraPlacer.Place(_cellsContainer);
            _navMeshSurface.BuildNavMesh();
            _enemies = _enemySpawner.Spawn(_playerController, _cellsContainer);
            _collectibleItemsSpawner.Spawn(_cellsContainer);
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
