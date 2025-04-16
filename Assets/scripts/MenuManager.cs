using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("�����");
        Application.Quit();
    }

    public void GoToGalery()
    {
        SceneManager.LoadScene("GaleryScene");
    }

    public void GoToLevels()
    {
        SceneManager.LoadScene("LevelsScene");
    }
}
