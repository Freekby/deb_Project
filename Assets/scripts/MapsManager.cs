using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapsManager : MonoBehaviour
{
    public static MapsManager instance;

    public Map[] MapObjects;
    public MapDisplayManager MapDisplayManager;
    private int _currentIndex;
    public AudioSource SoundPlay;
    public Animator transition;

    public void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);

        _currentIndex = 0;
        ChangeScriptableObject(0);
    }

    public int GetCurrentMapIndex()
    {
        return MapObjects[_currentIndex].MapIndex;
    }

    public void ChangeScriptableObject(int change)
    {
        _currentIndex += change;
        if (_currentIndex < 0)
        {
            _currentIndex = MapObjects.Length - 1;
        }
        else if (_currentIndex > MapObjects.Length - 1)
        {
            _currentIndex = 0;
        }

        if (MapDisplayManager != null)
        {
            MapDisplayManager.DisplayMap(MapObjects[_currentIndex]);
        }
    }

    public void StartGame()
    {
        SoundManager.instance.PlayButtonEffect();
        //SceneManager.LoadScene(_currentIndex + 3);
        StartCoroutine(PlayTransition(_currentIndex + 3));

    }
    public IEnumerator PlayTransition(int indexScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(indexScene);

    }

}