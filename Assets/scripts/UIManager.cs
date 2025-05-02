using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject hiddenIvonHolder;
    [SerializeField] private GameObject hiddenIvonPrefab;
    [SerializeField] private GameObject gamePanel;

    private List<GameObject> hiddenObjectIconList;
    public GameObject GamePanel { get { return gamePanel; } }
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);

        hiddenObjectIconList = new List<GameObject>();
    }
    

    public void PopulateHiddenObjectIcon (List<HiddenObjectData> activeHiddenObjectList)
    {
        hiddenObjectIconList.Clear();
        for (int i = 0; i < activeHiddenObjectList.Count; i++)
        {
            GameObject icon = Instantiate(hiddenIvonPrefab, hiddenIvonHolder.transform);
            icon.name = activeHiddenObjectList[i].hiddenObject.name;
            Image childImg = icon.transform.GetChild(0).GetComponent<Image>();
            Text childText = icon.transform.GetChild(1).GetComponent<Text>();

            childImg.sprite = activeHiddenObjectList[i].hiddenObject.GetComponent<SpriteRenderer>().sprite;
            childText.text = activeHiddenObjectList[i].name;

            hiddenObjectIconList.Add(icon);
        }
    }
    public void CheckSelectedHiddenObject(string objectName)
    {
        for (int i = 0; i < hiddenObjectIconList.Count; i++)
        {
            if (hiddenObjectIconList[i].name == objectName)
            {
                hiddenObjectIconList[i].SetActive(false);
                break;
            }
        }
    }
}
