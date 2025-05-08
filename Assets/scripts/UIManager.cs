using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject hiddenIvonHolder;
    [SerializeField] private GameObject hiddenIvonPrefab;
    [SerializeField] private GameObject gamePanel;

    private List<GameObject> hiddenObjectIconList;
    public Animator transition;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);

        hiddenObjectIconList = new List<GameObject>();
    }
    
    public void ShowEndGameWindow()
    {
        gamePanel.SetActive(true);
    }

    private GameObject CreateHiddenObjectIcon(HiddenObjectData hiddenObjectData)
    {
        GameObject icon = Instantiate(hiddenIvonPrefab, hiddenIvonHolder.transform);

        icon.name = hiddenObjectData.hiddenObject.name;
        Image childImg = icon.transform.GetChild(0).GetComponent<Image>();
        Text childText = icon.transform.GetChild(1).GetComponent<Text>();
        //AudioSource childSound = icon.transform.GetChild(2).GetComponent<AudioSource>();

        childImg.sprite = hiddenObjectData.hiddenObject.GetComponent<SpriteRenderer>().sprite;
        childText.text = hiddenObjectData.Name;
        

        return icon;
    }

    public void PopulateHiddenObjectIcon (List<HiddenObjectData> activeHiddenObjectList)
    {
        hiddenObjectIconList.Clear();
        for (int i = 0; i < activeHiddenObjectList.Count; i++)
        {
            GameObject icon = CreateHiddenObjectIcon(activeHiddenObjectList[i]);
            hiddenObjectIconList.Add(icon);
        }
    }

    public void DisableSelectedHiddenObject(string clickedObject)
    {
        for (int i = 0; i < hiddenObjectIconList.Count; i++)
        {
            if (hiddenObjectIconList[i].name == clickedObject)
            {
                hiddenObjectIconList[i].SetActive(false);
                break;
            }
        }
    }

    public void HelpButton()
    {
        StartCoroutine(LevelManager.instance.HelpMethod());
    }
    public void RetryButton()
    {
        SoundManager.instance.PlayButtonEffect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
 

    public void GoToLevelsList()
    {
        //SceneManager.LoadScene("LevelsScene");
        SoundManager.instance.PlayButtonEffect();
        StartCoroutine(PlayTransition("LevelsScene"));
    }
    public IEnumerator PlayTransition(string NameScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NameScene);

    }
}
