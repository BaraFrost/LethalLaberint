using Data;
using NaughtyAttributes;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class GameLoader : MonoBehaviour {

        [SerializeField]
        private LabyrinthSpawner _labyrinthSpawner;

        [SerializeField]
        private EnemySpawner _enemySpawner;

        [SerializeField]
        private PlayerSpawner _playerSpawner;

        [SerializeField]
        private MiniMapLogic _miniMapLogic;

        [SerializeField]
        private CollectibleItemsSpawner _collectibleItemsSpawner;

        [SerializeField]
        private EntityPointersSystem _entityPointersSystem;

        [SerializeField]
        private InventoryVisualizer _inventoryVisualizer;

        [SerializeField]
        private DoorsOpeningButton _doorsOpeningButton;

        [SerializeField]
        private ItemsCountVisualizer _itemsCountVisualizer;

        [SerializeField]
        private DifficultyProgressionConfig _difficultyProgressionConfig;

        [SerializeField]
        private PathDrawer _pathDrawer;

        [SerializeField]
        private EnvironmentSpawnLogic _environmentSpawnLogic;

        private SpawnedLabyrinthCellsContainer _cellsContainer;
        private List<Enemy> _enemies;

        private void Start() {
            Init();
            // Application.targetFrameRate = 30;
        }

        private void Init() {
            _cellsContainer = _labyrinthSpawner.Spawn(_difficultyProgressionConfig);

            _miniMapLogic.Init(_cellsContainer);

            _cellsContainer.StartCells.ShipLogic.Init(_miniMapLogic, HandleExitEvent);

            _playerSpawner.Init(HandlePlayerDeadEvent);

            _enemies = _enemySpawner.Spawn(_playerSpawner.PlayerController, _cellsContainer, _collectibleItemsSpawner.SpawnedItems, _difficultyProgressionConfig);

            _collectibleItemsSpawner.Spawn(_cellsContainer, _difficultyProgressionConfig);

            _entityPointersSystem.Init(_enemies, _playerSpawner.PlayerController);

            _inventoryVisualizer.Init(_playerSpawner.PlayerController);

            _doorsOpeningButton.Init(_cellsContainer.CellsWithDoors, _playerSpawner.PlayerController.transform);

            _itemsCountVisualizer.Init(_collectibleItemsSpawner.SpawnedItems);

            _pathDrawer.Init(_cellsContainer.StartCells.ShipLogic.WarehouseArea.transform, _playerSpawner.PlayerController.transform);

            _environmentSpawnLogic.Spawn(_cellsContainer);
        }

        private void HandlePlayerDeadEvent() {
            Account.Instance.HandleMatchDoneEvent(new Account.MatchDoneEvent() {
                EarnedMoney = 0,
            });
            LoadExitScene();
        }

        private void HandleExitEvent() {
            Account.Instance.HandleMatchDoneEvent(new Account.MatchDoneEvent() {
                EarnedMoney = _playerSpawner.PlayerController.MoneyWallet.MoneyCount,
            });
            LoadExitScene();
        }

        private void LoadExitScene() {
            SceneManager.LoadScene("Menu");
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(0);
            }
        }
    }
}
