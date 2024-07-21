using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public abstract class EnemyNode<T> : Node where T: Enemy {

        protected T _enemy;

        protected EnemyNode(T enemy) {
            _enemy = enemy;
        }
    }
}

