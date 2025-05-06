using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void ExitGame()
    {
        SoundManager.instance.PlayButtonEffect();
        Application.Quit();
    }

    public void GoToGalery()
    {
        SoundManager.instance.PlayButtonEffect();
        SceneManager.LoadScene("GaleryScene");

    }

    public void GoToLevels()
    {
        SoundManager.instance.PlayButtonEffect();
        SceneManager.LoadScene("LevelsScene");

    }
  
}
