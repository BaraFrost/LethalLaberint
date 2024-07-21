using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class TurretBehaviorTree : NpcBehaviorTree<TurretEnemy> {

        public TurretBehaviorTree(TurretEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new Selector (
                new List<Node>() {
                    new TurretAttackNode(_enemy),
                    new TurretSearchingNode(_enemy),
                }
            );
        }
    }
}
