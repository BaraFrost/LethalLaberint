using UnityEngine;

namespace Game {

    public class AdditionalEnemyTarget : MonoBehaviour {

        [SerializeField]
        private DisableEffect _destroyEffectPrefab;
        private DisableEffect _destroyEffectInstance;

        private void Awake() {
            _destroyEffectInstance = Instantiate(_destroyEffectPrefab, transform.position, Quaternion.identity, transform.parent);
            _destroyEffectInstance.gameObject.SetActive(false);
        }

        public void ActivateAdditionalTargetDestroyEffect() {
            _destroyEffectInstance.transform.position = transform.position;
            _destroyEffectInstance.gameObject.SetActive(true);
        }
    }
}
