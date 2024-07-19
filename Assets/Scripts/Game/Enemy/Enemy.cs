using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public abstract class Enemy : MonoBehaviour, ILabyrinthEntity {

        public class EnemyData {

            public PlayerController Player { get; private set; }

            public SpawnedLabyrinthCellsContainer CellContainer { get; private set; }

            public EnemyData(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer) {
                Player = player;
                CellContainer = cellsContainer;
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

        public abstract BehaviorTree NpcBehaviorTree { get; }

        public EnemyData Data { get; private set; }

        private bool _isInited = false;

        public virtual void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer) {
            Data = new EnemyData(player, cellsContainer);
            _isInited = true;
        }

        private void Update() {
            if (!_isInited) {
                return;
            }
            UpdateEnemy();
        }

        protected virtual void UpdateEnemy() {
            NpcBehaviorTree.Evaluate();
        }
    }
}
