using UnityEngine;
using UnityEngine.AI;

namespace Game {

    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private HealthLogic _healthLogic;
        public HealthLogic HealthLogic => _healthLogic;

        [SerializeField]
        private ItemsCollector _itemsCollector;
        public ItemsCollector ItemsCollector => _itemsCollector;

        [SerializeField]
        private Collider _collider;
        public Collider Collider => _collider;

        [SerializeField]
        private PlayerVisualLogic _playerVisualLogic;

        [SerializeField]
        private PlayerAbilityLogic _playerAbilityLogic;
        public PlayerAbilityLogic PlayerAbilityLogic => _playerAbilityLogic;

        [SerializeField]
        private PlayerInputLogic _playerInputLogic;
        public PlayerInputLogic PlayerInputLogic => _playerInputLogic;

        [SerializeField]
        private VariableJoystick _joystick;

        [SerializeField]
        private float _correctionDistance;

        [SerializeField]
        private float _correctionForce;

        [SerializeField]
        private float _rayDistance;

        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private LayerMask _wallLayer;  // Создайте переменную для слоя стен

        private float _gravityForce;
        private Vector3 _inputMoveVector;

        private CharacterController _characterController;
        private Animator _characterAnimator;

        private void Start() {
            HealthLogic.onDamaged += ItemsCollector.DropAllItems;
            HealthLogic.onDamaged += _playerVisualLogic.SpawnRagdoll;
            _characterAnimator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update() {
            CharacterMove();
            GamingGravity();
        }

        private Vector3 CorrectMovementOnNavMesh(Vector3 moveDirection) {
            var playerPosition = transform.position;
            playerPosition.y = 0;
            var newPosition = transform.position + moveDirection;
            if (NavMesh.SamplePosition(newPosition, out var hit, _correctionDistance, NavMesh.AllAreas)) {
                var navMeshPoint = hit.position;
                navMeshPoint.y = 0f;
                var correctedDirection = (navMeshPoint - playerPosition).normalized;
                if ((navMeshPoint - playerPosition).magnitude < _correctionForce * Time.deltaTime) {
                    return Vector3.zero;
                }
                return correctedDirection;
            }
            return Vector3.zero;
        }

        private Vector3 CorrectMovement(Vector3 moveDirection) {
            if (moveDirection.sqrMagnitude == 0) {
                return moveDirection;
            }
            Debug.Log($"start:{_inputMoveVector}");
            var correctedMovement = CorrectMovementOnNavMesh(moveDirection);
            if (correctedMovement.sqrMagnitude != 0) {
                return correctedMovement;
            }
            if (Physics.SphereCast(transform.position, 0.2f, Vector3Int.RoundToInt(moveDirection.normalized), out var raycasthit, _rayDistance, _wallLayer)) {
                Vector3 wallNormal = raycasthit.point - gameObject.transform.position;
                Debug.Log($"wallNormal:{wallNormal}");
                correctedMovement = Vector3.ProjectOnPlane(transform.forward, Vector3Int.RoundToInt(wallNormal.normalized)).normalized;
            } else {
                return correctedMovement;
            }
            return CorrectMovementOnNavMesh(correctedMovement * _moveSpeed * Time.deltaTime);
        }

        private void CharacterMove() {
            _inputMoveVector = Vector3.zero;
            if (_joystick.Direction.magnitude > 0) {
                var joystickVector = _joystick.Direction;
                _inputMoveVector.x = joystickVector.x;
                _inputMoveVector.z = joystickVector.y;
            } else {
                _inputMoveVector.x = Input.GetAxisRaw("Horizontal");
                _inputMoveVector.z = Input.GetAxisRaw("Vertical");
            }
            _inputMoveVector = CorrectMovement(_inputMoveVector.normalized * _moveSpeed * Time.deltaTime) * _moveSpeed * Time.deltaTime;
            Debug.Log(_inputMoveVector);
            UpdateRotation();
            if (_joystick.Direction.magnitude > 0) {

                _inputMoveVector *= _joystick.Direction.magnitude;
            }
            if (_characterController.isGrounded) {
                if (_inputMoveVector.x != 0 || _inputMoveVector.z != 0) _characterAnimator.SetBool("Move", true);
                else _characterAnimator.SetBool("Move", false);
            }
            _inputMoveVector.y = _gravityForce;
            _characterController.Move(_inputMoveVector);
        }

        private void UpdateRotation() {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _inputMoveVector, _rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        private void GamingGravity() {
            if (!_characterController.isGrounded) {
                _gravityForce -= 20f * Time.deltaTime;
            } else {
                _gravityForce = -1f;
            }
        }
    }

}