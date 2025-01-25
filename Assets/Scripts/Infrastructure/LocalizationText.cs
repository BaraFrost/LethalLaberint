using NaughtyAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Infrastructure {

    [Serializable]
    public class LocalizationText {

        public enum Language {
            ru,
            en,
        }

        [Serializable]
        private struct TextWithLanguage {

            public Language language;

            [TextArea(3, 5)]
            public string text;
        }

        [SerializeField]
        [TextArea(3, 5)]
        private string _defaultText;

        [SerializeField]
        private List<TextWithLanguage> _localizations;

        private Dictionary<Language, string> _cashedLocalizations;
        private Dictionary<Language, string> CashedLocalizations {
            get {
                if (_cashedLocalizations == null) {
                    _cashedLocalizations = _localizations.ToDictionary(localization => localization.language, localization => localization.text);
                }
                return _cashedLocalizations;
            }
        }

        public string GetText() {
            return _defaultText;
        }

#if UNITY_EDITOR
        private const string DomainAutoLocalization = "com";
        private const string LocalizationURL = "/translate_a/single?client=gtx&dt=t&sl={0}&tl={1}&q={2}";

        [Button]
        public void Translate() {
            _localizations = new List<TextWithLanguage>();
            foreach (var lang in (Language[])Enum.GetValues(typeof(Language))) {
                var text = TranslateGoogle(_defaultText, lang.ToString());
                if (string.IsNullOrEmpty(text)) {
                    continue;
                }
                _localizations.Add(new TextWithLanguage() { language = lang, text = text });
            }
        }

        private string TranslateGoogle(string text, string translationTo = "en") {

            var url = String.Format("https://translate.google." + DomainAutoLocalization + LocalizationURL,
                "auto", translationTo, WebUtility.UrlEncode(text));
            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();
            while (!www.isDone) {

            }
            string response = www.downloadHandler.text;

            try {
                JArray jsonArray = JArray.Parse(response);
                response = jsonArray[0][0][0].ToString();
            } catch {
                Debug.LogError("Translation error");
            }

            return response;
        }
#endif
    }
}
