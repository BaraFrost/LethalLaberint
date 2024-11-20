using Infrastructure;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data {

    public class Account : SingletonCrossScene<Account> {

        public struct MatchDoneEvent {
            public int EarnedMoney;
        }

        [SerializeField]
        private int _currentStage;
        public int CurrentStage {
            get {
                return _currentStage;
            }
            private set {
                _currentStage = value;
                SaveAccountData();
            }
        }

        private int _currentDay;
        public int CurrentDay {
            get {
                return _currentDay;
            }
            private set {
                _currentDay = value;
                SaveAccountData();
            }
        }
        public int TotalDays => DifficultyProgressionConfig.GetDaysByStage(CurrentStage);

        [SerializeField]
        private DifficultyProgressionConfig _difficultyProgressionConfig;
        public DifficultyProgressionConfig DifficultyProgressionConfig => _difficultyProgressionConfig;

        private int _totalMoney;
        public int TotalMoney {
            get {
                return _totalMoney;
            }
            private set {
                _totalMoney = value;
                SaveAccountData();
            }
        }

        private int _currentStageMoney;
        public int CurrentStageMoney {
            get {
                return _currentStageMoney;
            }
            private set {
                _currentStageMoney = value;
                SaveAccountData();
            }
        }

        public int RequiredMoney => _difficultyProgressionConfig.RequiredMoney;

        protected override void Init() {
            base.Init();
            LoadAccountData();
        }

        private void LoadAccountData() {
            _currentDay = PlayerPrefs.GetInt(nameof(CurrentDay), 1);
            _totalMoney = PlayerPrefs.GetInt(nameof(TotalMoney), 0);
            _currentStageMoney = PlayerPrefs.GetInt(nameof(CurrentStageMoney), 0);
            _currentStage = PlayerPrefs.GetInt(nameof(CurrentStage), _currentStage);
        }

        private void SaveAccountData() {
            PlayerPrefs.SetInt(nameof(CurrentDay), CurrentDay);
            PlayerPrefs.SetInt(nameof(TotalMoney), TotalMoney);
            PlayerPrefs.SetInt(nameof(CurrentStageMoney), CurrentStageMoney);
            PlayerPrefs.SetInt(nameof(CurrentStage), CurrentStage);
            PlayerPrefs.Save();
        }
#if UNITY_EDITOR
        [Button]
#endif
        public void Clear() {
            PlayerPrefs.DeleteAll();
            LoadAccountData();
        }

        public void HandleMatchDoneEvent(MatchDoneEvent matchDoneEvent) {
            TotalMoney += matchDoneEvent.EarnedMoney;
            CurrentStageMoney += matchDoneEvent.EarnedMoney;
            CurrentDay++;
            if (CurrentDay > _difficultyProgressionConfig.GetDaysByStage(CurrentStage)) {
                if(CurrentStageMoney >= RequiredMoney) {
                    CurrentStage++;
                }
                CurrentStageMoney = 0;
                CurrentDay = 1;
            }
            SceneManager.LoadScene(0);
        }

        void OnGUI() {
            var textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color.white;
            textStyle.fontSize = 40;
            GUI.Label(new Rect(100, 200, 100, 25), _currentStage.ToString(), textStyle);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                Clear();
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                _currentStage++;
            }
        }
    }
}

