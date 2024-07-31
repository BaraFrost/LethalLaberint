
namespace Game {

    public class MineBehaviorTree : NpcBehaviorTree<MineEnemy> {

        public MineBehaviorTree(MineEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new MineNode(_enemy);
        }
    }
}

