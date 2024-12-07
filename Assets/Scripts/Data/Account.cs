using Infrastructure;
using NaughtyAttributes;
using System.Collections.Generic;
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

        [SerializeField]
        private AbilityDataContainer _abilityDataContainer;
        public AbilityDataContainer AbilityDataContainer => _abilityDataContainer;

        [SerializeField]
        private int _currentAbilityId = 1;

        private Dictionary<int, int> _abilitiesCountData;

        public Dictionary<int, int> AbilitiesCountData {
            get {
                return _abilitiesCountData;
            }
            private set {
                _abilitiesCountData = value;
                SaveAccountData();
            }
        }

        public int CurrentAbilityId => _currentAbilityId;
        public AbilityData CurrentAbility => _abilityDataContainer.GetAbility(_currentAbilityId);
        public AbilityInitData CurrentAbilityInitData => new AbilityInitData() { abilityData = CurrentAbility, count = _abilitiesCountData[_currentAbilityId] };

        [SerializeField]
        private ShopItemsContainer _shopItemsContainer;

        protected override void Init() {
            base.Init();
            LoadAccountData();
        }

        private void LoadAccountData() {
            _currentDay = PlayerPrefs.GetInt(nameof(CurrentDay), 1);
            _totalMoney = PlayerPrefs.GetInt(nameof(TotalMoney), 0);
            _currentStageMoney = PlayerPrefs.GetInt(nameof(CurrentStageMoney), 0);
            _currentStage = PlayerPrefs.GetInt(nameof(CurrentStage), _currentStage);
            _abilitiesCountData = new Dictionary<int, int>();
            foreach (var abilityId in _abilityDataContainer.GetAllAbilityIds()) {
                _abilitiesCountData.Add(abilityId, PlayerPrefs.GetInt(nameof(AbilityData) + abilityId, 1));
            }
        }

        private void SaveAccountData() {
            PlayerPrefs.SetInt(nameof(CurrentDay), CurrentDay);
            PlayerPrefs.SetInt(nameof(TotalMoney), TotalMoney);
            PlayerPrefs.SetInt(nameof(CurrentStageMoney), CurrentStageMoney);
            PlayerPrefs.SetInt(nameof(CurrentStage), CurrentStage);
            foreach (var abilityCountData in _abilitiesCountData) {
                PlayerPrefs.SetInt(nameof(AbilityData) + abilityCountData.Key, abilityCountData.Value);
            }
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
                if (CurrentStageMoney >= RequiredMoney) {
                    CurrentStage++;
                }
                CurrentStageMoney = 0;
                CurrentDay = 1;
            }
            SceneManager.LoadScene(0);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                Clear();
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                _currentStage++;
            }
            if (Input.GetKeyDown(KeyCode.M)) {
                TotalMoney += 500;
            }
        }

        public bool TryToSelectAbility(int abilityId) {
            if (!_abilitiesCountData.ContainsKey(abilityId) || _abilitiesCountData[abilityId] <= 0) {
                return false;
            }
            _currentAbilityId = abilityId;
            return true;
        }

        public int GetShopItemPrice(ShopItem.Type type) {
            return _shopItemsContainer.GetShopItemByType(type).Price;
        }

        public bool TryToByItem(ShopItem.Type type) {
            var shopItem = _shopItemsContainer.GetShopItemByType(type);
            if (TotalMoney < shopItem.Price) {
                return false;
            }
            TotalMoney -= shopItem.Price;
            shopItem.GiveReward();
            return true;
        }

        public void AddAbility(int id) {
            AbilitiesCountData[id] ++;
        }

        public void OnAbilityUsed(int id) {
            AbilitiesCountData[id]--;
        }
    }
}

