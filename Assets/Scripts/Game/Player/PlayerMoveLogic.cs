using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Game {

    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveLogic : AbstractPlayerLogic {

        [SerializeField]
        private float _correctionDistance;

        [SerializeField]
        private float _correctionForce;

        [SerializeField]
        private float _rayDistance;

        [SerializeField]
        private float _moveSpeed;
        private float CurrentMoveSpeed => _moveSpeed * _player.PlayerModifierLogic.SpeedModifier;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private LayerMask _wallLayer;

        public Action onStartMove;
        public Action onStopMove;

        private float _gravityForce;
        private Vector3 _inputMoveVector;
        private CharacterController _characterController;
        private bool _isMoving;
        public bool IsMoving => _isMoving;
        private bool _freeze;

        public override void Init(PlayerController player) {
            base.Init(player);
            _characterController = GetComponent<CharacterController>();
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            UpdateCharacterMove();
            UpdateGravity();
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
            var correctedMovement = CorrectMovementOnNavMesh(moveDirection);
            if (correctedMovement.sqrMagnitude != 0) {
                return correctedMovement;
            }
            if (Physics.SphereCast(transform.position, 0.2f, Vector3Int.RoundToInt(moveDirection.normalized), out var raycasthit, _rayDistance, _wallLayer)) {
                Vector3 wallNormal = raycasthit.point - gameObject.transform.position;
                correctedMovement = Vector3.ProjectOnPlane(transform.forward, Vector3Int.RoundToInt(wallNormal.normalized)).normalized;
            } else {
                return correctedMovement;
            }
            return CorrectMovementOnNavMesh(correctedMovement * CurrentMoveSpeed * Time.deltaTime);
        }

        private void UpdateCharacterMove() {
            _inputMoveVector = _freeze ? Vector3.zero : _player.PlayerInputLogic.InputMoveVector;
            _inputMoveVector = CorrectMovement(_inputMoveVector.normalized * CurrentMoveSpeed * Time.deltaTime) * CurrentMoveSpeed * Time.deltaTime;
            UpdateRotation();
            if (_inputMoveVector.magnitude != 0 && !_isMoving) {
                onStartMove.Invoke();
                _isMoving = true;

            } else if (_inputMoveVector.magnitude == 0 && _isMoving) {
                _isMoving = false;
                onStopMove.Invoke();
            }
            _inputMoveVector.y = _gravityForce;
            _characterController.Move(_inputMoveVector);
        }

        private void UpdateRotation() {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _inputMoveVector, _rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        private void UpdateGravity() {
            if (!_characterController.isGrounded) {
                _gravityForce -= 20f * Time.deltaTime;
            } else {
                _gravityForce = -1f;
            }
        }

        public void Teleport(Vector3 position) {
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }

        private void OnDisable() {
            _freeze = false;
            StopAllCoroutines();
        }

        public void FreezeMovement(float time) {
            if (_freeze || _player.HealthLogic.IsDamaged) {
                return;
            }
            StartCoroutine(FreezeMovementCoroutine(time));
        }

        private IEnumerator FreezeMovementCoroutine(float time) {
            _freeze = true;
            yield return new WaitForSeconds(time);
            _freeze = false;
        }

        [SerializeField]
        private float _pushForce = 1f;

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            var rb = hit.collider.attachedRigidbody;
            if (rb == null || rb.isKinematic)
                return;
            var pushDirection = new Vector3(hit.moveDirection.x, 0.2f, hit.moveDirection.z);
            rb.AddForce(pushDirection.normalized * _pushForce, ForceMode.Impulse);
        }
    }
}

