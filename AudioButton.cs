using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Button))]

public class AudioButton : MonoBehaviour
{
    [SerializeField] private AudioSource _mainSound;

    private AudioSource _buttonAudio;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _buttonAudio = GetComponent<AudioSource>();

        if (_button != null)
        {
            _button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        StartCoroutine(PlayButtonSound());
    }

    private IEnumerator PlayButtonSound()
    {
        if (_mainSound.isPlaying)
        {
            _mainSound.Stop();
        }

        _buttonAudio.Play();

        while (_buttonAudio.isPlaying)
        {
            yield return null;
        }

        _mainSound.Play();
    }
}
