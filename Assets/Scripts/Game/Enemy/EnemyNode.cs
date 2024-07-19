using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public abstract class EnemyNode : Node {

        protected Enemy _enemy;

        protected EnemyNode(Enemy enemy) {
            _enemy = enemy;
        }
    }
}

