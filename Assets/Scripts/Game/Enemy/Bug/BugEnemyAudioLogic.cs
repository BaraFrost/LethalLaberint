using UnityEngine;

namespace Game {

    public class BugEnemyAudioLogic : AbstractEnemyAudioLogic<BugEnemy> {

        [SerializeField]
        private AudioSource _dropItemSound;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if(_dropItemSound != null) {
                Enemy.EnemyItemsCollectionLogic.OnItemDropped += PlayDropSound;
            }
        }

        private void PlayDropSound() {
            if (_dropItemSound.isPlaying) {
                return;
            }
            _dropItemSound.Play();
            return;
        }
    }
}

