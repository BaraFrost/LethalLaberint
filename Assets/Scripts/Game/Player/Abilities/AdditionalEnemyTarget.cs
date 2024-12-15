using UnityEngine;

namespace Game {

    public class AdditionalEnemyTarget : MonoBehaviour {

        [SerializeField]
        private DestroyableEffect _destroyEffect;

        public void DestroyAdditionalTarget() {
            Instantiate(_destroyEffect, gameObject.transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}
