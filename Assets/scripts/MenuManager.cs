using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator transition;
    public AudioMixerGroup _mixer;
    public void Start()
    {
        ChangeEffectVolume();
        ChangeMusicVolume();
    }

    public void ExitGame()
    {
        SoundManager.instance.PlayButtonEffect();
        Application.Quit();
    }

    public void GoToGalery()
    {
        SoundManager.instance.PlayButtonEffect();
        StartCoroutine(PlayTransition("GaleryScene"));
        //SceneManager.LoadScene("GaleryScene");

    }
    public IEnumerator PlayTransition(string NameScene)
    {
        transition.SetTrigger("Start");
       
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NameScene);

    }

    public void GoToLevels()
    {
        SoundManager.instance.PlayButtonEffect();
        //SceneManager.LoadScene("LevelsScene");
        StartCoroutine(PlayTransition("LevelsScene"));
        //PlayerPrefs.SetInt("CurrentLevel", 3);

    }
    public void ChangeEffectVolume()
    {
        float volume = PlayerPrefs.GetFloat("effectVolume");
        _mixer.audioMixer.SetFloat("SoundParametrs", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("effectVolume", volume);
    }

    public void ChangeMusicVolume()
    {
        float volume = PlayerPrefs.GetFloat("musicVolume");
        _mixer.audioMixer.SetFloat("MasterParametrs", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("musicVolume", volume);
    }


}
