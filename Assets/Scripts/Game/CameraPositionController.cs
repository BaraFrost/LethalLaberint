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

        private Vector3 _initialPosition;  // Переменная для хранения начальной позиции камеры
        private bool _isFreeFlightMode = false;  // Флаг для режима свободного полета

        private float _rotationX = 0f; // Угол наклона камеры по оси X
        private float _rotationY = 0f; // Угол поворота камеры по оси Y

        void Start() {
            _startYPosition = gameObject.transform.position.y;
            _cameraPlayerPositionDifference = _player.gameObject.transform.position - gameObject.transform.position;
            _initialPosition = transform.position;  // Сохраняем начальную позицию камеры
        }

        void Update() {
            // Переключение режима свободного полета при нажатии клавиши 'K'
#if DEV_BUILD
            if (Input.GetKeyDown(KeyCode.K)) {
                _isFreeFlightMode = !_isFreeFlightMode;

                // Если режим свободного полета выключается, сохраняем текущую позицию как начальную
                if (!_isFreeFlightMode) {
                    _startYPosition = gameObject.transform.position.y;
                    _cameraPlayerPositionDifference = _player.gameObject.transform.position - gameObject.transform.position;
                    _initialPosition = transform.position;  // Сохраняем начальную позицию камеры
                }
            }
#endif

            // Обновление позиции камеры в зависимости от режима
            if (_isFreeFlightMode) {
                FreeFlightCameraControl();  // Управление камерой в режиме свободного полета
            } else {
                UpdateCameraPosition();  // Стандартное поведение с следованием за игроком
            }
        }

        // Обновление позиции камеры, следуя за игроком
        private void UpdateCameraPosition() {
            if (_player == null) {
                return;
            }

            var newPosition = Vector3.SmoothDamp(transform.position, _player.gameObject.transform.position - _cameraPlayerPositionDifference, ref _velocity, _smooth);
            newPosition.y = _startYPosition;  // Сохраняем начальную высоту
            transform.position = newPosition;
        }

        // Управление свободным полетом камеры
        private void FreeFlightCameraControl() {
            float moveSpeed = 10f;  // Скорость перемещения камеры

            // Управление перемещением камеры с помощью клавиш WASD
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float moveZ = 0f;

            // Движение вперед по направлению взгляда (клавиша W)
            if (Input.GetKey(KeyCode.W)) {
                moveZ = moveSpeed * Time.deltaTime;
            }

            // Движение назад по направлению взгляда (клавиша S)
            if (Input.GetKey(KeyCode.S)) {
                moveZ = -moveSpeed * Time.deltaTime;  // Отрицательное значение для движения назад
            }

            // Перемещение камеры в 3D-пространстве (исключаем изменения по оси Y при движении вперед/назад)
            Vector3 move = transform.forward * moveZ + transform.right * moveX;
            transform.position += move;

            // Управление высотой камеры с помощью клавиш Ctrl и Shift
            if (Input.GetKey(KeyCode.LeftControl)) {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime;  // Снижаем камеру при нажатии Ctrl
            }

            if (Input.GetKey(KeyCode.LeftShift)) {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;  // Поднимаем камеру при нажатии Shift
            }

            // Получаем движение мыши для поворота камеры
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Поворот по оси Y (горизонтальный поворот камеры)
            _rotationY += mouseX * 2f;  // Умножаем на чувствительность

            // Поворот по оси X (вертикальный поворот камеры)
            _rotationX -= mouseY * 2f;  // Инвертируем, чтобы движение мыши вверх/вниз было интуитивным

            // Применяем повороты
            transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);  // Поворот только по осям X и Y
        }
    }
}