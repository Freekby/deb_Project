using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
                activeHiddenObjectsList.RemoveAt(i);
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject ClickedObject = GetClickedObject();

            if (ClickedObject)
            {
                ClickedObject.SetActive(false);
                UIManager.instance.DisableSelectedHiddenObject(ClickedObject.name);

                RemoveActiveHiddenObject(ClickedObject.name);

                foundHiddenObjectsCount++;

                if (maxObject <= foundHiddenObjectsCount) 
                {
                    UIManager.instance.ShowEndGameWindow();
                }
            }
        }
    }
    public IEnumerator HelpMethod()
    {
        int randomVal = UnityEngine.Random.Range(0, activeHiddenObjectsList.Count);
        Vector3 originalScale = activeHiddenObjectsList[randomVal].hiddenObject.transform.localScale;
        activeHiddenObjectsList[randomVal].hiddenObject.transform.localScale = originalScale * 1.25f;
        yield return new WaitForSeconds(0.25f);
        activeHiddenObjectsList[randomVal].hiddenObject.transform.localScale = originalScale;


    }
}
