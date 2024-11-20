using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class WardenBehaviorTree : NpcBehaviorTree<WardenEnemy> {

        public WardenBehaviorTree(WardenEnemy enemy) : base(enemy) {
        }

        protected override void GenerateTree() {
            _root = new Selector(
                new List<Node>() {
                    new WardenWaitSpawnItemNode(_enemy),
                    new AttackNode(_enemy),
                    new HuntingNode(_enemy),
                    new WardenPatrollingNode(_enemy)
                });
        }
    }
}
