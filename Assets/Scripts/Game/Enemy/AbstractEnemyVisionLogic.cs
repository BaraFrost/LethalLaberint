using System.Collections;
using UnityEngine;

namespace Game {

    public abstract class AbstractEnemyVisionLogic : AbstractEnemyLogic<Enemy> {

        private bool _temporaryDisabled;

        public bool CanSeeTarget(PlayerController enemyTarget) {
            if (_temporaryDisabled) {
                return false;
            }
            return CanSeeTargetInternal(enemyTarget);
        }

        protected abstract bool CanSeeTargetInternal(PlayerController enemyTarget);

        public void TemporarilyDisable(float disableTime) {
            if (_temporaryDisabled) {
                return;
            }
            StartCoroutine(TemporaryDisableCoroutine(disableTime));
        }

        private IEnumerator TemporaryDisableCoroutine(float disableTime) {
            _temporaryDisabled = true;
            yield return new WaitForSeconds(disableTime);
            _temporaryDisabled = false;
        }
    }
}

