using System.Collections;
using UnityEngine;

namespace Game {
    public class TemporarySpikeVisualLogic : AbstractVisualLogic<ZoneEnemy> {

        [SerializeField]
        private MaterialReplacer _materialReplacer;
        [SerializeField]
        private float _activationTime;

        protected override void PlayAttackVisual() {
            base.PlayAttackVisual();
            StartCoroutine(ActivateCoroutine());
        }

        private IEnumerator ActivateCoroutine() {
            _materialReplacer.Switch();
            yield return new WaitForSeconds(_activationTime);
            _materialReplacer.Switch();
        }
    }
}
