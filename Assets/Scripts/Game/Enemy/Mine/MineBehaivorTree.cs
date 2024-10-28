
namespace Game {

    public class ZoneBehaviorTree : NpcBehaviorTree<ZoneEnemy> {

        public ZoneBehaviorTree(ZoneEnemy enemy) : base(enemy) { }

        protected override void GenerateTree() {
            _root = new ZoneEnemyNode(_enemy);
        }
    }
}

