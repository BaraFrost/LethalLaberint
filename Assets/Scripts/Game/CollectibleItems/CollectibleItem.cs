using UnityEngine;

namespace Game {

    public class CollectibleItem : MonoBehaviour, ILabyrinthEntity {

        [SerializeField]
        private WalletCollectibleItem _walletCollectibleItemPrefab;
        public WalletCollectibleItem WalletCollectibleItem => _walletCollectibleItemPrefab;

        [SerializeField]
        private Collider _collider;

        [SerializeField]
        private SpriteRenderer _miniMapImage;

        public bool Collected { get; private set; }

        public bool collectedByEnemy;

        public void DisableCollectibleItem() {
            _collider.enabled = false;
            this.enabled = false;
            _miniMapImage.enabled = false;
            Collected = true;
        }
    }
}
