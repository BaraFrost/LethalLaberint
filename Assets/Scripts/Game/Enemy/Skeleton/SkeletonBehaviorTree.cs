using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {
    public class SkeletonBehaviorTree : NpcBehaviorTree<SkeletonEnemy> {

        public SkeletonBehaviorTree(SkeletonEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new Selector(new List<Node>() {
                new SkeletonWaitStateNode(_enemy),
                new AttackNode(_enemy),
                new HuntingNode(_enemy),
                new SkeletonHideNode(_enemy),
                new PatrollingNode(_enemy),
            });
        }
    }
}

