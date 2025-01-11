using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI {

    public class AudioSettingsPopup : ClosablePopup {

        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _effectsSlider;

        [SerializeField]
        private string _musicParameters;
        [SerializeField]
        private string _effectsParameters;
        [SerializeField]
        private AudioMixer _mixer;

        [SerializeField]
        private float _minVolumeValue;
        [SerializeField]
        private float _maxVolumeValue;

        protected override void Awake() {
            base.Awake();
            _musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
            _effectsSlider.onValueChanged.AddListener(UpdateEffectsVolume);
        }

        public override void ShowPopup() {
            base.ShowPopup();
            UpdateSliderValue(_musicSlider, _musicParameters);
            UpdateSliderValue(_effectsSlider, _effectsParameters);
        }

        private void UpdateSliderValue(Slider slider, string mixerParams) {
            _mixer.GetFloat(mixerParams, out var volumeValue);
            slider.SetValueWithoutNotify((volumeValue - _minVolumeValue) / (_maxVolumeValue - _minVolumeValue));
        }

        private void UpdateMusicVolume(float value) {
            UpdateMixerVolume(_musicParameters, value);
        }

        private void UpdateEffectsVolume(float value) {
            UpdateMixerVolume(_effectsParameters, value);
        }

        private void UpdateMixerVolume(string mixerParams, float value) {
            _mixer.SetFloat(mixerParams, value * (_maxVolumeValue - _minVolumeValue) + _minVolumeValue);
        }
    }
}
