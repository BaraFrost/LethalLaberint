using System;
using UnityEngine;

namespace MiniGames.FlappyBird {

    public class FlappyBirdWallet : MonoBehaviour {

        private int _value;
        public int Value => _value;

        [SerializeField]
        public int _additionalMoney;

        public Action _onValueUpdate;

        public void AddMoney() {
            _value += _additionalMoney;
            _onValueUpdate?.Invoke();
        }
    }
}

