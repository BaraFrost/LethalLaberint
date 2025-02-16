using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Game {

    public class StartBonusLogic : MonoBehaviour {

        [SerializeField]
        private StartBonusCollection _startBonusCollection;

        private GameEntitiesContainer _entitiesContainer;
        private Action _onApplyBonuses;

        public void Init(GameEntitiesContainer gameEntitiesContainer, Action onApplyBonuses) {
            if(TutorialLogic.Instance != null) {
                onApplyBonuses?.Invoke();
                return;
            }
            _onApplyBonuses = onApplyBonuses;
            _entitiesContainer = gameEntitiesContainer;
            gameEntitiesContainer.playerController.DisablePlayer();
            PopupManager.Instance.ShowStartBonusPopup(new StartBonusPopup.Data() {
                onBonusesSelectedCallback = ApplyStartBonuses,
                startBonuses = _startBonusCollection.StartBonuses,
            });

        }

        private void ApplyStartBonuses(List<AbstractStartBonus> startBonuses) {
            foreach(var bonus in startBonuses) {
                bonus.Apply(_entitiesContainer);
            }
            _entitiesContainer.playerController.EnablePlayer();
            _onApplyBonuses?.Invoke();
        }
    }
}

