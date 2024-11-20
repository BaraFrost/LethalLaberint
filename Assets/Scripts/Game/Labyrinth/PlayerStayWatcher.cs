using System;
using UnityEngine;

namespace Game {

    public class PlayerStayWatcher : MonoBehaviour {

        public Action onPlayerEnter;
        public Action onPlayerExit;
        public bool IsActive { get; set; } = true;

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<PlayerController>(out var player) && IsActive) {
                onPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.TryGetComponent<PlayerController>(out var player) && IsActive) {
                onPlayerExit?.Invoke();
            }
        }
    }
}
