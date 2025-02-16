using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI {

    public class StartBonusPopup : AbstractPopup {

        public struct Data {
            public AbstractStartBonus[] startBonuses;
            public Action<List<AbstractStartBonus>> onBonusesSelectedCallback;
        }

        [SerializeField]
        private StartBonusItem[] _startBonusItems;

        [SerializeField]
        private Button _getAllButton;

        private Data _data;

        private void Awake() {
            _getAllButton.onClick.AddListener(GiveAllBonuses);
        }

        public void SetData(Data data) {
            _data = data;
            var startBonuses = new List<AbstractStartBonus>(_data.startBonuses);
            foreach (var item in _startBonusItems) {
                var randomBonus = startBonuses[UnityEngine.Random.Range(0, startBonuses.Count)];
                startBonuses.Remove(randomBonus);
                item.Init(randomBonus, GiveStartBonus);
            }
        }

        private void GiveStartBonus(AbstractStartBonus startBonus) {
            _data.onBonusesSelectedCallback?.Invoke(new List<AbstractStartBonus>() { startBonus });
            HidePopup(immediately: true);
        }

        private void GiveAllBonuses() {
            YG2.RewardedAdvShow("start_bonus", () => {
                _data.onBonusesSelectedCallback?.Invoke(_startBonusItems.Select(item => item.StartBonus).ToList());
                HidePopup(immediately: true);
            });
        }
    }
}