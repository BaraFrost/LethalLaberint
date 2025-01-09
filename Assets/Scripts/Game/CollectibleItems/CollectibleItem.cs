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

        [SerializeField]
        private AudioSource _dropSoundPrefab;
        private AudioSource _dropSoundInstance;

        public bool Collected { get; private set; }

        public bool collectedByEnemy;

        private void Awake() {
            if (_dropSoundPrefab != null) {
                _dropSoundInstance = Instantiate(_dropSoundPrefab);
            }
        }

        public void DisableCollectibleItem() {
            _collider.enabled = false;
            this.enabled = false;
            _miniMapImage.enabled = false;
            Collected = true;
        }

        public void Drop() {
            if (_dropSoundInstance != null) {
                _dropSoundInstance.transform.position = gameObject.transform.position;
                _dropSoundInstance.Play();
            }
        }
    }
}
