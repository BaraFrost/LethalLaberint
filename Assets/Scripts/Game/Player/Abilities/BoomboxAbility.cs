using System.Collections;
using UnityEngine;

namespace Game {

    public class BoomboxAbility : AbstractAbility {

        [SerializeField]
        private AdditionalEnemyTarget _additionalEnemyTargetPrefab;

        [SerializeField]
        private float _time;

        private AdditionalEnemyTarget _additionalEnemyTarget;
        protected override bool IsAbilityActive => _additionalEnemyTarget != null;

        public override void Activate() {
            StartCoroutine(BoomboxCoroutine());
        }

        private IEnumerator BoomboxCoroutine() {
            _additionalEnemyTarget = Instantiate(_additionalEnemyTargetPrefab, _player.transform.position, _additionalEnemyTargetPrefab.transform.rotation, transform.parent.parent);
            _player.GameEntitiesContainer.additionalEnemyTarget = _additionalEnemyTarget;
            yield return new WaitForSeconds(_time);
            _player.GameEntitiesContainer.additionalEnemyTarget = null;
            _additionalEnemyTarget.DestroyAdditionalTarget();
        }
    }
}
