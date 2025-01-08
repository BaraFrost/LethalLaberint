using UnityEngine;

namespace Game {

    public class DisableEffect : MonoBehaviour {

        [SerializeField]
        private float _timeToDisable;
        private float _currentTimeToDisable;

        private void OnEnable() {
            _currentTimeToDisable = _timeToDisable;
        }

        private void Update() {
            _currentTimeToDisable -= Time.deltaTime;
            if (_currentTimeToDisable < 0) {
                gameObject.SetActive(false);
            }
        }
    }
}

