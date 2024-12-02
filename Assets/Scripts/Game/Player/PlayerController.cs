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

        private AbstractPlayerLogic[] _playerLogics;

        private float _gravityForce;
        private Vector3 _inputMoveVector;

        private CharacterController _characterController;
        private Animator _characterAnimator;

        public void Init() {
            _playerLogics = GetComponents<AbstractPlayerLogic>();
            foreach(var logic in _playerLogics) {
                logic.Init(this);
            }
            HealthLogic.onDamaged += ItemsCollector.DropAllItems;
            HealthLogic.onDamaged += _ragdollVisualLogic.SpawnRagdoll;
        }

        private void Update() {
            foreach (var logic in _playerLogics) {
                logic.UpdateLogic();
            }
        }
    }
}