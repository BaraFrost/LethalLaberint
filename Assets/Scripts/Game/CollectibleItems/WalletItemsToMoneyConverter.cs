using UnityEngine;

namespace Game {

    public class WalletItemsToMoneyConverter : MonoBehaviour {

        [SerializeField]
        private Wallet _wallet;

        [SerializeField]
        private MoneyWallet _moneyWallet;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent<WarehouseArea>(out var warehouseArea)) {
                var money = _wallet.GetItemsMoney();
                var items = _wallet.TakeAllItems();
                warehouseArea.AddItems(items);
                _moneyWallet.AddMoney(money);
            }
        }
    }
}
