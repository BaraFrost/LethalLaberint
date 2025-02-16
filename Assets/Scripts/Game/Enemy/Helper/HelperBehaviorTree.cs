using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class HelperBehaviorTree : NpcBehaviorTree<HelperEnemy> {

        public HelperBehaviorTree(HelperEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new Selector(
                new List<Node>() {
                    new ItemDeliveryNode(_enemy),
                    new ItemsProtectionNode(_enemy),
                    new ItemsCollectionNode(_enemy),
                    new PatrollingNode(_enemy),
                }
            );
        }
    }
}