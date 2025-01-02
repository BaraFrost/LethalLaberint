using System.Collections;
using UnityEngine;

namespace Game {

    public class BoomboxAbility : AbstractAbility {

        [SerializeField]
        private AdditionalEnemyTarget _additionalEnemyTargetPrefab;
        private AdditionalEnemyTarget _additionalEnemyTarget;

        public override void Activate() {
            base.Activate();
            _additionalEnemyTarget = Instantiate(_additionalEnemyTargetPrefab, _player.transform.position, _additionalEnemyTargetPrefab.transform.rotation, transform.parent.parent);
            _player.GameEntitiesContainer.additionalEnemyTarget = _additionalEnemyTarget;
        }

        protected override void Stop() {
            base.Stop();
            _player.GameEntitiesContainer.additionalEnemyTarget = null;
            _additionalEnemyTarget.DestroyAdditionalTarget();
        }
    }
}
