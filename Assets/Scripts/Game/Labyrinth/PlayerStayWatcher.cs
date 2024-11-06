using System;
using UnityEngine;

namespace Game {

    public class PlayerStayWatcher : MonoBehaviour {

        public Action onPlayerEnter;
        public Action onPlayerExit;

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<PlayerController>(out var player)) {
                onPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.TryGetComponent<PlayerController>(out var player)) {
                onPlayerExit?.Invoke();
            }
        }
    }
}
