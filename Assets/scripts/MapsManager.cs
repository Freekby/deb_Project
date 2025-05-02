using UnityEngine;
using UnityEngine.SceneManagement;

public class MapsManager : MonoBehaviour
{
    public Map[] MapObjects;
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
            MapDisplayManager.DisplayMap(MapObjects[_currentIndex]);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_currentIndex+3);
    }
}