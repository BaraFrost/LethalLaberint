using UnityEngine;

namespace Game {

    public class WalletItemsToMoneyConverter : AbstractPlayerLogic {

        [SerializeField]
        private Wallet _wallet;

        [SerializeField]
        private MoneyWallet _moneyWallet;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent<WarehouseArea>(out var warehouseArea)) {
                var money = GetWalletMoney();
                var items = _wallet.TakeAllItems();
                warehouseArea.AddItems(items);
                _moneyWallet.AddMoney((int)(money));
            }
        }

        public int GetWalletMoney() {
            return (int)(_wallet.GetItemsMoney() * _player.PlayerModifierLogic.MoneyModifier);
        }
    }
}
