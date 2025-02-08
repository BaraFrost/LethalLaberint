using UnityEngine;

namespace Game {

    public class CameraPositionController : MonoBehaviour {
        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private float _smooth;

        private Vector3 _velocity = Vector3.zero;
        private Vector3 _cameraPlayerPositionDifference;
        private float _startYPosition;

        private Vector3 _initialPosition;  // ���������� ��� �������� ��������� ������� ������
        private bool _isFreeFlightMode = false;  // ���� ��� ������ ���������� ������

        private float _rotationX = 0f; // ���� ������� ������ �� ��� X
        private float _rotationY = 0f; // ���� �������� ������ �� ��� Y

        void Start() {
            _startYPosition = gameObject.transform.position.y;
            _cameraPlayerPositionDifference = _player.gameObject.transform.position - gameObject.transform.position;
            _initialPosition = transform.position;  // ��������� ��������� ������� ������
        }

        void Update() {
            // ������������ ������ ���������� ������ ��� ������� ������� 'K'
#if DEV_BUILD
            if (Input.GetKeyDown(KeyCode.K)) {
                _isFreeFlightMode = !_isFreeFlightMode;

                // ���� ����� ���������� ������ �����������, ��������� ������� ������� ��� ���������
                if (!_isFreeFlightMode) {
                    _startYPosition = gameObject.transform.position.y;
                    _cameraPlayerPositionDifference = _player.gameObject.transform.position - gameObject.transform.position;
                    _initialPosition = transform.position;  // ��������� ��������� ������� ������
                }
            }
#endif

            // ���������� ������� ������ � ����������� �� ������
            if (_isFreeFlightMode) {
                FreeFlightCameraControl();  // ���������� ������� � ������ ���������� ������
            } else {
                UpdateCameraPosition();  // ����������� ��������� � ����������� �� �������
            }
        }

        // ���������� ������� ������, ������ �� �������
        private void UpdateCameraPosition() {
            if (_player == null) {
                return;
            }

            var newPosition = Vector3.SmoothDamp(transform.position, _player.gameObject.transform.position - _cameraPlayerPositionDifference, ref _velocity, _smooth);
            newPosition.y = _startYPosition;  // ��������� ��������� ������
            transform.position = newPosition;
        }

        // ���������� ��������� ������� ������
        private void FreeFlightCameraControl() {
            float moveSpeed = 10f;  // �������� ����������� ������

            // ���������� ������������ ������ � ������� ������ WASD
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float moveZ = 0f;

            // �������� ������ �� ����������� ������� (������� W)
            if (Input.GetKey(KeyCode.W)) {
                moveZ = moveSpeed * Time.deltaTime;
            }

            // �������� ����� �� ����������� ������� (������� S)
            if (Input.GetKey(KeyCode.S)) {
                moveZ = -moveSpeed * Time.deltaTime;  // ������������� �������� ��� �������� �����
            }

            // ����������� ������ � 3D-������������ (��������� ��������� �� ��� Y ��� �������� ������/�����)
            Vector3 move = transform.forward * moveZ + transform.right * moveX;
            transform.position += move;

            // ���������� ������� ������ � ������� ������ Ctrl � Shift
            if (Input.GetKey(KeyCode.LeftControl)) {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime;  // ������� ������ ��� ������� Ctrl
            }

            if (Input.GetKey(KeyCode.LeftShift)) {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;  // ��������� ������ ��� ������� Shift
            }

            // �������� �������� ���� ��� �������� ������
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // ������� �� ��� Y (�������������� ������� ������)
            _rotationY += mouseX * 2f;  // �������� �� ����������������

            // ������� �� ��� X (������������ ������� ������)
            _rotationX -= mouseY * 2f;  // �����������, ����� �������� ���� �����/���� ���� �����������

            // ��������� ��������
            transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);  // ������� ������ �� ���� X � Y
        }
    }
}