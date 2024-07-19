namespace Scripts.Infrastructure.BehaviorTree {

    public abstract class BehaviorTree {

        protected Node _root = null;

        protected abstract void GenerateTree();

        public NodeState Evaluate() {
            return _root.Evaluate();
        }
    }
}