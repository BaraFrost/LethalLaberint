using UnityEngine;

namespace Game {

    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private HealthLogic _healthLogic;
        public HealthLogic HealthLogic => _healthLogic;

        [SerializeField]
        private ItemsCollector _itemsCollector;
        public ItemsCollector ItemsCollector => _itemsCollector;

        [SerializeField]
        private MoneyWallet _moneyWallet;
        public MoneyWallet MoneyWallet => _moneyWallet;

        [SerializeField]
        private Collider _collider;
        public Collider Collider => _collider;

        [SerializeField]
        private RagdollVisualLogic _ragdollVisualLogic;
        public RagdollVisualLogic RagdollVisualLogic => _ragdollVisualLogic;

        [SerializeField]
        private PlayerAbilityLogic _playerAbilityLogic;
        public PlayerAbilityLogic PlayerAbilityLogic => _playerAbilityLogic;

        [SerializeField]
        private PlayerInputLogic _playerInputLogic;
        public PlayerInputLogic PlayerInputLogic => _playerInputLogic;

        [SerializeField]
        private PlayerMoveLogic _playerMoveLogic;
        public PlayerMoveLogic PlayerMoveLogic => _playerMoveLogic;

        [SerializeField]
        private PlayerModifierLogic _playerModifierLogic;
        public PlayerModifierLogic PlayerModifierLogic => _playerModifierLogic;

        [SerializeField]
        private PlayerVisualLogic _playerVisualLogic;
        public PlayerVisualLogic PlayerVisualLogic => _playerVisualLogic;

        private GameEntitiesContainer _gameEntitiesContainer;
        public GameEntitiesContainer GameEntitiesContainer => _gameEntitiesContainer;

        private AbstractPlayerLogic[] _playerLogics;

        private float _gravityForce;
        private Vector3 _inputMoveVector;

        private CharacterController _characterController;
        private Animator _characterAnimator;
        private bool _disable;

        public void Init(GameEntitiesContainer gameEntitiesContainer) {
            _gameEntitiesContainer = gameEntitiesContainer;
            _playerLogics = GetComponents<AbstractPlayerLogic>();
            foreach(var logic in _playerLogics) {
                logic.Init(this);
            }
            HealthLogic.onDamaged += (Enemy.EnemyType type) => { ItemsCollector.DropAllItems(); };
            HealthLogic.onDamaged += (Enemy.EnemyType type) => { _ragdollVisualLogic.SpawnRagdoll(); };
        }

        public void UpdateLogics() {
            if(!gameObject.activeSelf || _disable) {
                return;
            }
            foreach (var logic in _playerLogics) {
                logic.UpdateLogic();
            }
        }

        public void DisablePlayer() {
            _disable = true;
        }
    }
}