using UnityEngine;

namespace Game {

    public class StartLabyrinthCells : MonoBehaviour {

        [SerializeField]
        private LabyrinthCell[] _startCells;
        public LabyrinthCell[] StartCells => _startCells;

        [SerializeField]
        private LabyrinthCell _startCell;
        public LabyrinthCell StartCell => _startCell;

        [SerializeField]
        private WarehouseArea _warehouseArea;
        public WarehouseArea WarehouseArea => _warehouseArea;

        [SerializeField]
        private Enemy[] _startEnemies;
        public Enemy[] StartEnemies => _startEnemies;

        [SerializeField]
        private CollectibleItem[] _startCollectibleItems;
        public CollectibleItem[] StartCollectibleItems => _startCollectibleItems;

        [SerializeField]
        private bool _needGenerateCells;
        public bool NeedGenerateCells => _needGenerateCells;
    }
}
