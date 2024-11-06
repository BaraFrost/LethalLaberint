using Data;
using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class StageCounter : SingletonCrossScene<StageCounter> {

        [SerializeField]
        private int _currentStage;
        public int CurrentStage => _currentStage;

        [SerializeField]
        private DifficultyProgressionConfig _difficultyProgressionConfig;

        [SerializeField]
        private int day;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.T)) {
                _currentStage++;
            }
            if(Input.GetKeyDown(KeyCode.Y)) {
                _currentStage = 0;
            }
        }

        void OnGUI() {
            var textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color.white;
            textStyle.fontSize = 40;
            GUI.Label(new Rect(100, 200, 100, 25), _currentStage.ToString(), textStyle);
        }

        public void Exit() {
            day++;
            if (day > _difficultyProgressionConfig.GetDaysByStage(_currentStage)) {
                _currentStage++;
                day = 1;
            }
            SceneManager.LoadScene(0);
        }
    }
}
