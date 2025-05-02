using UnityEngine;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField]
    private List <HiddenObjectData> hiddenObjectList;

    private List<HiddenObjectData> activeHiddenObjectsList;

    public int maxObject;
    private int totalHiddenObjectsCount = 0;
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
    void AssignHiddenObjects()
    {
        activeHiddenObjectsList.Clear();
        for(int i = 0; i < hiddenObjectList.Count;i++)
        {
            hiddenObjectList[i].hiddenObject.GetComponent<Collider2D>().enabled = false;
        }

        int k = 0;
        while (k < maxObject)
        {
            int randomVal = Random.Range(0, hiddenObjectList.Count);

            if (!hiddenObjectList[randomVal].makeHidden)
            {
                hiddenObjectList[randomVal].name = "" + k;
                hiddenObjectList[randomVal].makeHidden = true;
                hiddenObjectList[randomVal].hiddenObject.GetComponent<Collider2D>().enabled = true;
                activeHiddenObjectsList.Add(hiddenObjectList[randomVal]);
                k++;
            }
            
        }
        UIManager.instance.PopulateHiddenObjectIcon(activeHiddenObjectsList);
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 position = new Vector2(mousePos.origin.x, mousePos.origin.y);
            Collider2D hit = Physics2D.OverlapPoint(position);

            if (hit)
            {
                //Debug.Log("Object Name" + hit.gameObject.name);

                hit.gameObject.SetActive(false);
                UIManager.instance.CheckSelectedHiddenObject(hit.gameObject.name);
                for (int i = 0; i < activeHiddenObjectsList.Count; i++)
                {
                    if (activeHiddenObjectsList[i].hiddenObject.name == hit.gameObject.name)
                    {
                        activeHiddenObjectsList.RemoveAt(i);
                        break;
                    }
                }
                totalHiddenObjectsCount++;
                    Debug.Log(totalHiddenObjectsCount);
                    Debug.Log(maxObject);
                if (maxObject <= totalHiddenObjectsCount) 
                {
                    Debug.Log("Ты победил. Иди  нахуй");
                    UIManager.instance.GamePanel.SetActive(true);
                    UIManager.instance.GamePanel.transform.position = new Vector3(0, 0, -2);


                }

            }

            Debug.Log(hit);
        }
    }
}
[System.Serializable]
public class HiddenObjectData
{
    public string name;
    public GameObject hiddenObject;
    public bool makeHidden = false;
}