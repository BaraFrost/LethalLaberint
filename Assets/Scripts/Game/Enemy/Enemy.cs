using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public abstract class Enemy : MonoBehaviour, ILabyrinthEntity {

        public enum EnemyType {
            Default,
            Electric,
        }

        [SerializeField]
        private EnemyType _type;
        public EnemyType Type => _type;

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

        [SerializeField]
        private bool _needShowArrow;
        public bool NeedShowArrow => _needShowArrow;

        public GameEntitiesContainer EntitiesContainer  { get; private set; }

        public IEnemyLogic[] _allLogics;

        private bool _isInited = false;

        public virtual void Init(GameEntitiesContainer entitiesContainer) {
            EntitiesContainer = entitiesContainer;
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
