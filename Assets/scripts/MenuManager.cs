using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator transition;

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
    
  
}
