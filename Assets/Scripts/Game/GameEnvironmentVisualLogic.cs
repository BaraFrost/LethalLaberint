using UnityEngine;

namespace Game {

    public class GameEnvironmentVisualLogic: MonoBehaviour {

        [SerializeField]
        private GameLightLogic _lightLogic;
        public GameLightLogic LightLogic => _lightLogic;
    }
}

