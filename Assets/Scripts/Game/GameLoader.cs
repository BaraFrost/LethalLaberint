using Data;
using UI;
using UnityEngine;

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
        private PathDrawer _pathDrawer;

        [SerializeField]
        private EnvironmentSpawnLogic _environmentSpawnLogic;

        [SerializeField]
        private GameEntitiesContainer _gameEntitiesContainer;

        [SerializeField]
        private HintManager _hintManager;

        private void Start() {
            Init();
        }

        private void Init() {
            _labyrinthSpawner.Spawn(_gameEntitiesContainer);

            _miniMapLogic.Init(_gameEntitiesContainer.cellsContainer);

            _gameEntitiesContainer.cellsContainer.StartCells.ShipLogic.Init(_miniMapLogic, HandleExitEvent);

            _playerSpawner.Init(HandlePlayerDeadEvent, _gameEntitiesContainer);

            _enemySpawner.Spawn(_gameEntitiesContainer);

            _collectibleItemsSpawner.Spawn(_gameEntitiesContainer);

            _entityPointersSystem.Init(_gameEntitiesContainer);

            _inventoryVisualizer.Init(_gameEntitiesContainer.playerController, _gameEntitiesContainer.cellsContainer.StartCells);

            _doorsOpeningButton.Init(_gameEntitiesContainer);

            _itemsCountVisualizer.Init(_collectibleItemsSpawner.SpawnedItems);

            _pathDrawer.Init(_gameEntitiesContainer.cellsContainer.StartCells.ShipLogic.WarehouseArea.transform, _gameEntitiesContainer.playerController.transform);

            _environmentSpawnLogic.Spawn(_gameEntitiesContainer.cellsContainer);

            _hintManager.Init(_gameEntitiesContainer.playerController);
        }

        private void HandlePlayerDeadEvent() {
            PopupManager.Instance.ShowDeathPopup(new DeathPopup.Data {
                continueCallback = () => {
                    Account.Instance.HandleMatchDoneEvent(new Account.MatchDoneEvent() {
                        EarnedMoney = 0,
                    });
                    ScenesSwitchManager.Instance.LoadMenuScene();
                }
            }
            );
        }

        private void HandleExitEvent() {
            var earnedMoney = _gameEntitiesContainer.playerController.MoneyWallet.MoneyCount;
            _gameEntitiesContainer.playerController.DisablePlayer();
            PopupManager.Instance.ShowWinPopup(() => {
                if (Account.Instance.DifficultyProgressionConfig.IsBonusStage || Account.Instance.TotalDays != Account.Instance.CurrentDay) {
                    Account.Instance.HandleMatchDoneEvent(new Account.MatchDoneEvent() {
                        EarnedMoney = earnedMoney,
                    });
                    ScenesSwitchManager.Instance.LoadMenuScene();
                } else {
                    Account.Instance.CurrentStageMoney = earnedMoney;
                    ScenesSwitchManager.Instance.LoadMiniGameScene();
                }
            },
            earnedMoney);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.R)) {
                PopupManager.Instance.ShowFadePopup(() => {
                    ScenesSwitchManager.Instance.LoadMenuScene();
                });
            }
        }
    }
}
