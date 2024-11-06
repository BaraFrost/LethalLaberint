using NaughtyAttributes;
using UnityEditor;
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
        private Enemy[] _startEnemies;
        public Enemy[] StartEnemies => _startEnemies;

        [SerializeField]
        private bool _needGenerateEnemy;
        public bool NeedGenerateEnemy => _needGenerateEnemy;

        [SerializeField]
        private int _startEpoch;
        public int StartEpoch => _startEpoch;

        [SerializeField]
        private CollectibleItem[] _startCollectibleItems;
        public CollectibleItem[] StartCollectibleItems => _startCollectibleItems;

        [SerializeField]
        private bool _needGenerateCells;
        public bool NeedGenerateCells => _needGenerateCells;

        [SerializeField]
        private ShipLogic _shipLogic;
        public ShipLogic ShipLogic => _shipLogic;


#if UNITY_EDITOR
        [Button]
        public void FillArrays() {
            _startCells = gameObject.GetComponentsInChildren<LabyrinthCell>();
            _startEnemies = gameObject.GetComponentsInChildren<Enemy>();
            _startCollectibleItems = gameObject.GetComponentsInChildren<CollectibleItem>();
            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}
