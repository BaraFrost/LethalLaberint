using UnityEngine;

namespace Game {

    public class DestroyableEffect : MonoBehaviour {

        [SerializeField]
        private float _destroyTime;

        private void Start() {
            Destroy(gameObject, _destroyTime);
        }
    }
}