using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public abstract class Enemy : MonoBehaviour, ILabyrinthEntity {

        public class EnemyData {

            public PlayerController Player { get; private set; }

            public SpawnedLabyrinthCellsContainer CellContainer { get; private set; }

            public List<CollectibleItem> CollectibleItems { get; private set; }

            public EnemyData(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
                Player = player;
                CellContainer = cellsContainer;
                CollectibleItems = collectibleItems;
            }
        }

        [SerializeField]
        private AbstractEnemyVisionLogic _visionLogic;
        public AbstractEnemyVisionLogic VisionLogic => _visionLogic;

        [SerializeField]
        private AbstractMovementLogic _movementLogic;
        public AbstractMovementLogic MovementLogic => _movementLogic;

        [SerializeField]
        private AbstractEnemyAttackLogic _attackLogic;
        public AbstractEnemyAttackLogic AttackLogic => _attackLogic;

        [SerializeField]
        private HealthLogic _healthLogic;
        public HealthLogic HealthLogic => _healthLogic;

        public abstract BehaviorTree NpcBehaviorTree { get; }

        public EnemyData Data { get; private set; }

        public IEnemyLogic[] _allLogics;

        private bool _isInited = false;

        public virtual void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            Data = new EnemyData(player, cellsContainer, collectibleItems);
            InitAllLogic();
            _isInited = true;
        }

        private void InitAllLogic() {
            _allLogics = GetComponents<IEnemyLogic>();
            for (var i = 0; i < _allLogics.Length; i++) {
                _allLogics[i].Init(this);
            }
        }

        private void UpdateAllLogic() {
            for (var i = 0; i < _allLogics.Length; i++) {
                _allLogics[i].UpdateLogic();
            }
        }

        private void Update() {
            if (!_isInited) {
                return;
            }
            UpdateEnemy();
        }

        protected virtual void UpdateEnemy() {
            UpdateAllLogic();
            NpcBehaviorTree.Evaluate();
        }
    }
}
