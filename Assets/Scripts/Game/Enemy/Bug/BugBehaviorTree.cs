using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class BugBehaviorTree : NpcBehaviorTree<BugEnemy> {

        public BugBehaviorTree(BugEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new Selector(
            new List<Node>() {
                new RunningAwayNode(_enemy),
                new ItemsTrackingNode(_enemy),
                new PatrollingNode(_enemy),
                /*new ItemDeliveryNode(_enemy),
                new AttackNode(_enemy),
                new HuntingNode(_enemy),
                new ItemsProtectionNode(_enemy),
                new ItemsCollectionNode(_enemy),
                new PatrollingNode(_enemy),*/
            });
        }
    }
}