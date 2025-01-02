using System;
using UnityEngine;

namespace Infrastructure {

    [Serializable]
    public class LocalizationText {

        [SerializeField]
        [TextArea(3, 5)]
        private string _defaultText;

        public string GetText() {
            return _defaultText;
        }
    }
}
