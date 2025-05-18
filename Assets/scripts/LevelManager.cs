using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField]
    private List <HiddenObjectData> hiddenObjectList;

    private List<HiddenObjectData> activeHiddenObjectsList;

    public int maxObject;
    private int foundHiddenObjectsCount = 0;


    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
    }

    private void Start()
    {
        activeHiddenObjectsList = new List<HiddenObjectData>();
        AssignHiddenObjects();
    }

    /// <summary>
    /// Отключает коллайдеры всех объектов на карте.
    /// </summary>
    private void DisableColliders()
    {
        for (int i = 0; i < hiddenObjectList.Count; i++)
        {
            hiddenObjectList[i].hiddenObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void SelectActiveHiddenObjects()
    {
        int k = 0;
        while (k < maxObject)
        {
            int randomVal = Random.Range(0, hiddenObjectList.Count);

            if (!hiddenObjectList[randomVal].makeHidden)
            {
                hiddenObjectList[randomVal].makeHidden = true;
                hiddenObjectList[randomVal].hiddenObject.GetComponent<Collider2D>().enabled = true;
                activeHiddenObjectsList.Add(hiddenObjectList[randomVal]);
                k++;
            }
        }
    }

    public void AssignHiddenObjects()
    {
        activeHiddenObjectsList.Clear();
        DisableColliders();
        SelectActiveHiddenObjects();
        
        UIManager.instance.PopulateHiddenObjectIcon(activeHiddenObjectsList);
    }

    private GameObject GetClickedObject()
    {
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 position = new Vector2(mousePos.origin.x, mousePos.origin.y);
        Collider2D hit = Physics2D.OverlapPoint(position);

        return hit.gameObject;
    }

    private void RemoveActiveHiddenObject(string objectName)
    {
        for (int i = 0; i < activeHiddenObjectsList.Count; i++)
        {
            if (activeHiddenObjectsList[i].hiddenObject.name == objectName)
            {
                AudioSource audioSource = activeHiddenObjectsList[i].soundObject.GetComponent<AudioSource>();
                StartCoroutine(PlaySound(audioSource));
                activeHiddenObjectsList.RemoveAt(i);
                break;
            }
        }
    }
    private IEnumerator PlaySound(AudioSource audioSource)
    {
        audioSource.Play();
        yield return new WaitForSeconds(2f);

    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject ClickedObject = GetClickedObject();

            if (ClickedObject)
            {
                RemoveActiveHiddenObject(ClickedObject.name);
                ClickedObject.GetComponent<SpriteRenderer>().enabled = false;

                UIManager.instance.DisableSelectedHiddenObject(ClickedObject.name);
                

                foundHiddenObjectsCount++;
                Debug.Log(foundHiddenObjectsCount);

                if (maxObject <= foundHiddenObjectsCount) 
                {
                    if (MapsManager.instance.GetCurrentMapIndex() >= PlayerPrefs.GetInt("CurrentLevel", 3))
                    {
                        PlayerPrefs.SetInt("CurrentLevel", MapsManager.instance.GetCurrentMapIndex()+1);
                    }

                    UIManager.instance.ShowEndGameWindow();
                }
            }
        }
    }

    public IEnumerator HelpMethod()
    {
        int randomVal = UnityEngine.Random.Range(0, activeHiddenObjectsList.Count);
        Vector3 originalScale = activeHiddenObjectsList[randomVal].hiddenObject.transform.localScale;
        activeHiddenObjectsList[randomVal].soundObject.GetComponent<AudioSource>().Play();
        Debug.Log(activeHiddenObjectsList[randomVal].soundObject.name);


        activeHiddenObjectsList[randomVal].hiddenObject.transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.25f);
        activeHiddenObjectsList[randomVal].hiddenObject.transform.localScale = originalScale;
    }
}
