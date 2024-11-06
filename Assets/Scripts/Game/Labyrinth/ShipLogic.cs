using Data;
using System;
using UnityEngine;

namespace Game {

    public class ShipLogic : MonoBehaviour {

        [SerializeField]
        private PlayerStayWatcher _miniMapStayWatcher;

        [SerializeField]
        private PlayerStayWatcher _exitPlayerWatcher;

        [SerializeField]
        private WarehouseArea _warehouseArea;
        public WarehouseArea WarehouseArea => _warehouseArea;

        private MiniMapLogic _miniMapLogic;

        public void Init(MiniMapLogic miniMapLogic, Action exitEvent) {
            SubscribeMiniMapSwitch(miniMapLogic);
            _exitPlayerWatcher.onPlayerEnter += exitEvent;
        }

        private void SubscribeMiniMapSwitch(MiniMapLogic miniMapLogic) {
            _miniMapStayWatcher.onPlayerEnter += miniMapLogic.ActivateMiniMap;
            _miniMapStayWatcher.onPlayerExit += miniMapLogic.DeactivateMiniMap;
        }
    }
}

