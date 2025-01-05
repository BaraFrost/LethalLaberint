using Infrastructure;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class ScenesSwitchManager : SingletonCrossScene<ScenesSwitchManager> {

        [SerializeField]
        private string _menuSceneName;
        [SerializeField]
        private string _gameSceneName;
        [SerializeField]
        private string[] _miniGamesScenes;

        public void LoadMiniGameScene() {
            var randomMiniGame = _miniGamesScenes[Random.Range(0, _miniGamesScenes.Length)];
            LoadScene(randomMiniGame);
        }

        public void LoadMenuScene() {
            LoadScene(_menuSceneName);
        }

        public void LoadGameScene() {
            LoadScene(_gameSceneName);
        }

        private void LoadScene(string sceneName) {
            PopupManager.Instance.ShowFadePopup(
                () => {
                    SceneManager.LoadScene(sceneName);
                }
            );
        }
    }
}
