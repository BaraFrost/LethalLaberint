using System;
using UnityEngine;

namespace Game {

    public class HealthLogic : MonoBehaviour {

        [SerializeField]
        private int _startHealthCount;
        private int _currentHealthCount;
        public int HealthCount => _currentHealthCount;

        public Action onDamaged;

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

