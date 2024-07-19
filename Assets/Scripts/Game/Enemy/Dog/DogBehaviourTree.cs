using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class DogBehaviorTree : NpcBehaviorTree<DogEnemy> {

        public DogBehaviorTree(DogEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new Selector(
            new List<Node>() {
                new AttackNode(_enemy),
                new HuntingNode(_enemy),
                new PatrollingNode(_enemy)
            });
        }
    }
}


