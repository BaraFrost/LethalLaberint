using NaughtyAttributes;
using UnityEngine;

namespace UI {

    public class MenuPlayerAnimatorController : MonoBehaviour {

        [SerializeField]
        private Animator _playerAnimator;

        [SerializeField]
        private int _animationsCount;

        private void Awake() {
            ActivateRandomDance();
        }

        [Button]
        private void ActivateRandomDance() {
            _playerAnimator.Play(Random.Range(1, _animationsCount).ToString());
        }
    }
}
