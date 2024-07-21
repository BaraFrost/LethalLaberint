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
        private Collider _collider;
        public Collider Collider => _collider;

        [SerializeField]
        private PlayerVisualLogic _playerVisualLogic;

        [SerializeField]
        private VariableJoystick _joystick;

        public float speedMove;
        public float jumpPower;
        public float rotationSpeed;

        private float gravityForce;
        private Vector3 moveVector;

        private CharacterController ch_controller;
        private Animator ch_animator;

        private void Start() {
            HealthLogic.onDamaged += ItemsCollector.DropAllItems;
            HealthLogic.onDamaged += _playerVisualLogic.SpawnRagdoll;
            ch_animator = GetComponent<Animator>();
            ch_controller = GetComponent<CharacterController>();
        }

        private void Update() {
            CharacterMove();
            GamingGravity();
        }


        private void CharacterMove() {
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * speedMove;////_joystick.Horizontal * speedMove
            moveVector.z = Input.GetAxis("Vertical") * speedMove; // //_joystick.Vertical * speedMove;
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, rotationSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
            if (ch_controller.isGrounded) {

                if (gravityForce < 0) {
                    ch_animator.SetBool("Jump", false);
                }

                if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("Move", true);
                else ch_animator.SetBool("Move", false);
            }
            moveVector.y = gravityForce;
            ch_controller.Move(moveVector * Time.deltaTime);
        }

        private void GamingGravity() {
            if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
            else gravityForce = -1f;
            if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded) {
                gravityForce = jumpPower;
                ch_animator.SetBool("Jump", true);
            }
        }
    }

}