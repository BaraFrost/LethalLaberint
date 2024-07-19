using UnityEngine;

namespace Game {

    public class CollectibleItem : MonoBehaviour {

        [SerializeField]
        private WalletCollectibleItem _walletCollectibleItemPrefab;
        public WalletCollectibleItem WalletCollectibleItem => _walletCollectibleItemPrefab;
    }
}
