using Data;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
    public class MenuLogic : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _stageText;
        [SerializeField]
        private TextMeshProUGUI _dayText;
        [SerializeField]
        private TextMeshProUGUI _moneyText;
        [SerializeField]
        private TextMeshProUGUI _quotaText;
        [SerializeField]
        private Button _playButton;

        void Start() {
            _playButton.onClick.AddListener(LoadGameScreen);
        }

        private void LoadGameScreen() {
            ScenesSwitchManager.Instance.LoadGameScene();
        }

        void Update() {
            _stageText.text = $"�������:{Account.Instance.CurrentStage}";
            _dayText.text = $"����:{Account.Instance.CurrentDay} �� {Account.Instance.TotalDays}";
            _moneyText.text = $"{Account.Instance.TotalMoney}$";
            _quotaText.text = $"����� {Account.Instance.CurrentStageMoney}/{Account.Instance.RequiredMoney}$";
        }
    }
}

