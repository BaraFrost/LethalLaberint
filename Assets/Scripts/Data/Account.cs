using Game;
using Infrastructure;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Data {

    public class Account : SingletonCrossScene<Account> {

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
            set {
                _currentStageMoney = value;
                SaveAccountData();
            }
        }

        public int RequiredMoney => _difficultyProgressionConfig.TotalRequiredMoney;

        [SerializeField]
        private AbilityDataContainer _abilityDataContainer;
        public AbilityDataContainer AbilityDataContainer => _abilityDataContainer;

        [SerializeField]
        private int _currentAbilityId = 1;

        private Dictionary<ModifierType, int> _modifiersCountData = new Dictionary<ModifierType, int>();
        public Dictionary<ModifierType, int> ModifiersCountData {
            get {
                return _modifiersCountData;
            }
            private set {
                _modifiersCountData = value;
                SaveAccountData();
            }
        }

        [SerializeField]
        private LevelsModifiersContainer _levelsModifiersContainer;
        public LevelsModifiersContainer LevelsModifiersContainer => _levelsModifiersContainer;

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

        private Dictionary<Enemy.EnemyType, bool> _openedEnemies;
        public Dictionary<Enemy.EnemyType, bool> OpenedEnemies {
            get {
                return _openedEnemies;
            }
            private set {
                _openedEnemies = value;
                SaveAccountData();
            }
        }

        public bool newEnemyOpened;
        public Enemy.EnemyType newEnemyType;

        public int CurrentAbilityId => _currentAbilityId;
        public AbilityData CurrentAbility => _abilityDataContainer.GetAbility(_currentAbilityId);
        public AbilityInitData CurrentAbilityInitData => new AbilityInitData() { abilityData = CurrentAbility, count = _abilitiesCountData[_currentAbilityId] };
        public Action onCurrentAbilityChanged;
        public bool GameStarted { get; private set; }

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
            _modifiersCountData = new Dictionary<ModifierType, int>();
            _openedEnemies = new Dictionary<Enemy.EnemyType, bool>();
            foreach (var abilityId in _abilityDataContainer.GetAllAbilityIds()) {
                _abilitiesCountData.Add(abilityId, PlayerPrefs.GetInt(nameof(AbilitiesCountData) + abilityId, 1));
            }
            foreach (var modifier in LevelsModifiersContainer.Modifiers) {
                _modifiersCountData.Add(modifier.Key, PlayerPrefs.GetInt(nameof(ModifiersCountData) + modifier.Key, 0));
            }
            foreach (var enemyType in Enum.GetValues(typeof(Enemy.EnemyType)).Cast<Enemy.EnemyType>()) {
                _openedEnemies.Add(enemyType, Convert.ToBoolean(PlayerPrefs.GetInt(nameof(OpenedEnemies) + enemyType)));
            }
        }

        private void SaveAccountData() {
            PlayerPrefs.SetInt(nameof(CurrentDay), CurrentDay);
            PlayerPrefs.SetInt(nameof(TotalMoney), TotalMoney);
            PlayerPrefs.SetInt(nameof(CurrentStageMoney), CurrentStageMoney);
            PlayerPrefs.SetInt(nameof(CurrentStage), CurrentStage);
            foreach (var abilityCountData in _abilitiesCountData) {
                PlayerPrefs.SetInt(nameof(AbilitiesCountData) + abilityCountData.Key, abilityCountData.Value);
            }
            foreach (var modifier in _modifiersCountData) {
                PlayerPrefs.SetInt(nameof(ModifiersCountData) + modifier.Key, modifier.Value);
            }
            foreach (var enemy in _openedEnemies) {
                PlayerPrefs.SetInt(nameof(OpenedEnemies) + enemy.Key, Convert.ToInt32(enemy.Value));
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

        public void StartGame() {
            GameStarted = true;
            ScenesSwitchManager.Instance.LoadGameScene();
        }

        public bool TryToSwitchStage() {
            var success = false;
            GameStarted = false;
            TotalMoney += CurrentStageMoney;
            CurrentDay++;
            if (CurrentDay > _difficultyProgressionConfig.GetDaysByStage(CurrentStage)) {
                if (CurrentStageMoney >= RequiredMoney) {
                    CurrentStage++;
                    success = true;
                }
                CurrentStageMoney = 0;
                CurrentDay = 1;
            }
            return success;
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
            onCurrentAbilityChanged?.Invoke();
            return true;
        }

        public ShopItem GetShopItemByType(ShopItem.Type type) {
            return _shopItemsContainer.GetShopItemByType(type);
        }

        public bool TryToByItem(ShopItem.Type type) {
            var shopItem = _shopItemsContainer.GetShopItemByType(type);
            var canBuyByMoney = Account.Instance.TotalMoney >= shopItem.Price && shopItem.CanBuyByMoney;
            if(!canBuyByMoney && shopItem.CanBuyByAdd) {
                shopItem.GiveReward();
            }
            if (TotalMoney < shopItem.Price || !shopItem.CanBuyByMoney) {
                return false;
            }
            TotalMoney -= shopItem.Price;
            shopItem.GiveReward();
            return true;
        }

        public void AddAbility(int id) {
            AbilitiesCountData[id]++;
        }

        public void OnAbilityUsed(int id) {
            AbilitiesCountData[id]--;
        }
    }
}

