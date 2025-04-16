using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptableObjectManager : MonoBehaviour
{
    public ScriptableObject[] MapObjects;
    public MapDisplayManager MapDisplayManager;
    private int _currentIndex;

    public void Awake()
    {
        ChangeScriptableObject(0);
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
            MapDisplayManager.DisplayMap((Map)MapObjects[_currentIndex]);
        }
    }

    public void StartGame()
    {
        Map currentMap = (Map)MapObjects[_currentIndex];

        if (currentMap.SceneToload != null)
        {
            Debug.Log(currentMap.SceneToload.name);
            SceneManager.LoadScene(currentMap.SceneToload.name);
        }
    }
}