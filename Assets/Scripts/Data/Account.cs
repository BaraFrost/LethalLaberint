using Game;
using Infrastructure;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

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

        private int _gamesPlayed;
        public int GamesPlayed {
            get { return _gamesPlayed; }
            private set {
                _gamesPlayed = value;
                SaveAccountData();
            }
        }

        [SerializeField]
        private int _totalEarnedMoney;
        private int TotalEarnedMoney {
            get { return _totalEarnedMoney; }
            set {
                _totalEarnedMoney = value;
                SaveAccountData();
            }
        }
        [SerializeField]
        private int _miniGameRecord;
        public int MiniGameRecord {
            get { return _miniGameRecord; }
            private set {
                _miniGameRecord = value;
                SaveAccountData();
            }
        }

        [NonSerialized]
        public bool newEnemyOpened;
        [NonSerialized]
        public Enemy.EnemyType newEnemyType;

        public int CurrentAbilityId => _currentAbilityId;
        public AbilityData CurrentAbility => _abilityDataContainer.GetAbility(_currentAbilityId);
        public AbilityInitData CurrentAbilityInitData => new AbilityInitData() { abilityData = CurrentAbility, count = _abilitiesCountData[_currentAbilityId] };
        public Action onCurrentAbilityChanged;
        public bool GameStarted { get; private set; }
        public bool MiniGameStarted { get; private set; }
        public bool CanShowInterAdd { get; set; } = true;

        [SerializeField]
        private ShopItemsContainer _shopItemsContainer;

        protected override void Init() {
            base.Init();
            LoadAccountData();
            YG2.onCloseAnyAdv += StopPause;
            YG2.onErrorAnyAdv += StopPause;
        }

        private void StopPause() {
            YG2.PauseGame(false);
        }

        private void LoadAccountData() {
            _miniGameRecord = PlayerPrefs.GetInt(nameof(MiniGameRecord), 0);
            _totalEarnedMoney = PlayerPrefs.GetInt(nameof(TotalEarnedMoney), 0);
            _gamesPlayed = PlayerPrefs.GetInt(nameof(GamesPlayed), 0);
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
            PlayerPrefs.SetInt(nameof(MiniGameRecord), MiniGameRecord);
            PlayerPrefs.SetInt(nameof(TotalEarnedMoney), TotalEarnedMoney);
            PlayerPrefs.SetInt(nameof(GamesPlayed), GamesPlayed);
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
            PlayerPrefs.SetInt(nameof(MiniGameRecord), 0);
            PlayerPrefs.SetInt(nameof(TotalEarnedMoney), 0);
            PlayerPrefs.SetInt(nameof(GamesPlayed), 0);
            PlayerPrefs.SetInt(nameof(CurrentDay), 0);
            PlayerPrefs.SetInt(nameof(TotalMoney), 0);
            PlayerPrefs.SetInt(nameof(CurrentStageMoney), 0);
            PlayerPrefs.SetInt(nameof(CurrentStage), 0);
            foreach (var abilityCountData in _abilitiesCountData) {
                PlayerPrefs.SetInt(nameof(AbilitiesCountData) + abilityCountData.Key, 1);
            }
            foreach (var modifier in _modifiersCountData) {
                PlayerPrefs.SetInt(nameof(ModifiersCountData) + modifier.Key, 0);
            }
            foreach (var enemy in _openedEnemies) {
                PlayerPrefs.SetInt(nameof(OpenedEnemies) + enemy.Key, 0);
            }
            PlayerPrefs.Save();
            LoadAccountData();
        }

        public void StartGame() {
            GamesPlayed++;
            GameStarted = true;
            ScenesSwitchManager.Instance.LoadGameScene();
        }

        public void StartMiniGame() {
            MiniGameStarted = true;
            ScenesSwitchManager.Instance.LoadMiniGameScene();
        }

        public void HandleMiniGameEnd(int earnedMoney) {
            MiniGameStarted = false;
            if(earnedMoney > MiniGameRecord) {
                MiniGameRecord = earnedMoney;
            }
            if(GameStarted) {
                CurrentStageMoney += earnedMoney;
            }
            else {
                TotalMoney += earnedMoney;
            }
        }

        public bool TryToSwitchStage() {
            var success = false;
            GameStarted = false;
            TotalMoney += CurrentStageMoney;
            CurrentDay++;
            TotalEarnedMoney += CurrentStageMoney;
            YG2.SetLeaderboard("TotalEarnedMoneyLeaderboard", TotalEarnedMoney);
            var currentStageMoney = CurrentStageMoney;
            if (CurrentDay > _difficultyProgressionConfig.GetDaysByStage(CurrentStage)) {
                if (CurrentStageMoney >= RequiredMoney) {
                    CurrentStage++;
                    success = true;
                }
                CurrentStageMoney = 0;
                CurrentDay = 1;
            }
            var eventData = new Dictionary<string, object> {
                {"games_played", GamesPlayed},
                {"current_stage", CurrentStage},
                {"total_earned_money", TotalEarnedMoney},
                {"win", success },
            };
            YG2.MetricaSend("game_end", eventData);
            return success;
        }

        private void Update() {
#if DEV_BUILD
            if (Input.GetKeyDown(KeyCode.C)) {
                Clear();
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                _currentStage++;
            }
            if (Input.GetKeyDown(KeyCode.M)) {
                TotalMoney += 500;
            }
#endif
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
            if (!canBuyByMoney && shopItem.CanBuyByAdd) {
                YG2.RewardedAdvShow(type.ToString(), () => {
                    shopItem.GiveReward();
                });
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

