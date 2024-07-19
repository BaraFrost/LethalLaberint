using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public abstract class NpcBehaviorTree<T> : BehaviorTree where T : Enemy {

        protected T _enemy;

        protected NpcBehaviorTree(T enemy){
            _enemy = enemy;
            GenerateTree();
        }
    }
}
