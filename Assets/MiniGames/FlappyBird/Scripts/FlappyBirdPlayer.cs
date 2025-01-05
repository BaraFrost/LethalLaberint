using Game;
using System;
using UnityEngine;

namespace MiniGames.FlappyBird {

    public class FlappyBirdPlayer : MonoBehaviour {

        [SerializeField]
        private float _jumpForce = 5f; // Сила прыжка
        [SerializeField]
        private float _fallSpeed = 2.5f; // Скорость падения
        [SerializeField]
        private float _tiltSpeed = 5f; // Скорость наклона
        [SerializeField]
        private float _maxTiltAngle = 45f; // Максимальный угол наклона
        private Rigidbody _rb;

        [SerializeField]
        private DestroyableEffect _destroyEffect;
        [SerializeField]
        private FlappyBirdWallet _wallet;
        public FlappyBirdWallet Wallet => _wallet;

        public Action onPlayerDead;

        void Start() {
            _rb = GetComponent<Rigidbody>();
        }

        public void UpdateLogic() {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
            }

            ApplyCustomFallSpeed();
            UpdateTilt();
        }

        private void ApplyCustomFallSpeed() {
            _rb.velocity += Vector3.down * _fallSpeed * Time.deltaTime;
        }

        private void UpdateTilt() {
            float targetAngle = Mathf.Clamp(_rb.velocity.y * _maxTiltAngle / _jumpForce, -_maxTiltAngle, _maxTiltAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-targetAngle, 90, 0), _tiltSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<FlappyBirdMoney>(out var money)) {
                money.gameObject.SetActive(false);
                _wallet.AddMoney();
            } else if (other.TryGetComponent<FlappyBirdObstacle>(out var obstacle)) {
                Instantiate(_destroyEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                onPlayerDead?.Invoke();
            }
        }
    }
}

