using System.Collections;
using UnityEngine;

namespace Game {

    public class BoomboxAbility : AbstractAbility {

        [SerializeField]
        private AdditionalEnemyTarget _additionalEnemyTargetPrefab;
        private AdditionalEnemyTarget _additionalEnemyTarget;

        public override void Init(PlayerController playerController) {
            base.Init(playerController);
            _additionalEnemyTarget = Instantiate(_additionalEnemyTargetPrefab, _player.transform.position, _additionalEnemyTargetPrefab.transform.rotation, transform.parent.parent);
            _additionalEnemyTarget.gameObject.SetActive(false);
        }

        public override void Activate() {
            base.Activate();
            _additionalEnemyTarget.transform.position = _player.transform.position;
            _additionalEnemyTarget.gameObject.SetActive(true);
            _player.GameEntitiesContainer.additionalEnemyTarget = _additionalEnemyTarget;
        }

        protected override void Stop() {
            base.Stop();
            _player.GameEntitiesContainer.additionalEnemyTarget = null;
            _additionalEnemyTarget.ActivateAdditionalTargetDestroyEffect();
            _additionalEnemyTarget.gameObject.SetActive(false);
        }
    }
}
