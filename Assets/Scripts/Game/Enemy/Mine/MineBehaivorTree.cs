using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class ZoneBehaviorTree : NpcBehaviorTree<ZoneEnemy> {

        public ZoneBehaviorTree(ZoneEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new Selector(
            new List<Node>() {
                new ZoneEnemyNode(_enemy),
            }); 
        }
    }
}

