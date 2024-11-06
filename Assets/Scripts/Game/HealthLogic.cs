using System;
using UnityEngine;

namespace Game {

    public class HealthLogic : MonoBehaviour {

        [SerializeField]
        private int _startHealthCount;
        private int _currentHealthCount;
        public int HealthCount => _currentHealthCount;

        public Action onDamaged;
        public Action onDead;

        private bool _isDamaged;
        public bool IsDamaged => _isDamaged;

        public void Awake() {
            _currentHealthCount = _startHealthCount;
        }

        public void AddDamage() {
            if(_isDamaged || _currentHealthCount <= 0) {
                return;
            }
            gameObject.SetActive(false);
            _isDamaged = true;
            _currentHealthCount--;
            onDamaged?.Invoke();
            if(_currentHealthCount<= 0) {
                onDead?.Invoke();
            }
        }

        public void Revive() {
            if (!_isDamaged) {
                return;
            }
            _isDamaged = false;
            gameObject.SetActive(true);
        }
    }
}

