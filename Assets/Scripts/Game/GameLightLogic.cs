using UnityEngine;

namespace Game {

    public class GameLightLogic : MonoBehaviour {

        [SerializeField]
        private GameObject _defaultLight;
        [SerializeField]
        private GameObject _darkLight;

        public void ActivateDefaultLight() {
            _defaultLight.gameObject.SetActive(true);
            _darkLight.gameObject.SetActive(false);
        }

        public void ActivateDarkLight() {
            _defaultLight.gameObject.SetActive(false);
            _darkLight.gameObject.SetActive(true);
        }
    }
}

