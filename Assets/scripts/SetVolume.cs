using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixerGroup _mixer;
    public Slider _musicSlider;
    public Slider _effectSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            ChangeEffectVolume();
            ChangeMusicVolume();
        }
    }

    public void ChangeEffectVolume()
    {
        float volume = _effectSlider.value;
        _mixer.audioMixer.SetFloat("SoundParametrs", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("effectVolume", volume);
    }

    public void ChangeMusicVolume()
    {
        Debug.Log(_musicSlider);
        float volume = _musicSlider.value;
        _mixer.audioMixer.SetFloat("MasterParametrs", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _effectSlider.value = PlayerPrefs.GetFloat("effectVolume");

        ChangeMusicVolume();
        ChangeEffectVolume();
    }
}
