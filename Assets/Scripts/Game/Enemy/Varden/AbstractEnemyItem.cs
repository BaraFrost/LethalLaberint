using System;
using UnityEngine;

namespace Game {

    public abstract class AbstractEnemyItem : MonoBehaviour {

        public Action<AbstractEnemyItem> onItemActivate;
    
    }
}
