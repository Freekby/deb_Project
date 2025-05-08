using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    //public string Name;
    //public Animation animation;
    public static LevelLoader instance;

    public void LoadNextScene(string NameScene)
    {
        StartCoroutine(PlayTransition(NameScene));
    }
    public IEnumerator PlayTransition(string NameScene)
    {
        transition.SetTrigger("Start");
        //Debug.Log(NameScene);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NameScene);
        
    }


}
