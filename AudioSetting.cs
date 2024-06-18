using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    private readonly string MasterVolume = "MasterVolume";
    private readonly string AmbientVolume = "AmbientVolume";
    private readonly string ButtonVolume = "ButtonVolume";


    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _sliderMasterVolume;
    [SerializeField] private Slider _sliderAmbientVolume;
    [SerializeField] private Slider _sliderButtonVolume;
    [SerializeField] private Button _buttonToggleVolume;

    private float _currentMasterVolume;
    private float _currentButtonVolume;
    private float _currentAmbientVolume;
    private bool _isMuted = false;

    private void Start()
    {
        if (_buttonToggleVolume != null)
        {
            _buttonToggleVolume.onClick.AddListener(ToggleMusic);
        }

        InitializeSlider(_sliderMasterVolume, MasterVolume);
        InitializeSlider(_sliderAmbientVolume, AmbientVolume);
        InitializeSlider(_sliderButtonVolume, ButtonVolume);
    }

    private void InitializeSlider(Slider slider, string parameter)
    {
        if (_mixer.audioMixer.GetFloat(parameter, out float currentVolume))
        {
            slider.value = Mathf.Pow(10, currentVolume / 20);
        }

        slider.onValueChanged.AddListener(volume => OnChangedVolume(volume, parameter));
    }

    private void ToggleMusic()
    {
        _isMuted = !_isMuted;

        if (_isMuted)
        {
            _mixer.audioMixer.SetFloat(MasterVolume, -80f);
        }
        else
        {
            _mixer.audioMixer.SetFloat(MasterVolume, _currentMasterVolume);
            _mixer.audioMixer.SetFloat(MasterVolume, _currentAmbientVolume);
            _mixer.audioMixer.SetFloat(MasterVolume, _currentButtonVolume);
        }
    }

    private void OnChangedVolume(float volume, string parameter)
    {
        float currentVolume = Mathf.Log10(volume) * 20;

        if (volume > 0)
        {
            _mixer.audioMixer.SetFloat(parameter, currentVolume);
        }
        else
        {
            _mixer.audioMixer.SetFloat(parameter, -80f);
        }
    }
}
