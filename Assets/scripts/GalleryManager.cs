using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class GalleryManager : MonoBehaviour
{
    public Map[] MapObjects;
    [SerializeField] private GameObject hiddenIvonHolder;
    [SerializeField] private GameObject hiddenIvonPrefab;
    [SerializeField] private GameObject descriptionRewardHolder;

    public AudioSource SoundPlay;
    public Animator transition;
    private Stopwatch stopwatch = new Stopwatch();
    private SpriteSaver spriteSaver;
    private Map currentMap;

    private void Start()
    {
        InitialiseRewards(MapObjects);
    }

    private void InitialiseRewards(Map[] maps)
    {
        foreach (Map map in maps)
        {
            CreateRewardIcon(map);
        }
    }

    private void CreateRewardIcon(Map map)
    {
        GameObject icon = Instantiate(hiddenIvonPrefab, hiddenIvonHolder.transform);
        icon.GetComponent<RewardMapHolder>().SetRewardMap(map);
    }


    public void LoadMenuScene()
    {
        SoundManager.instance.PlayButtonEffect();
        //SceneManager.LoadScene("MenuScene");
       StartCoroutine(PlayTransition("MenuScene"));
    }
    public IEnumerator PlayTransition(string NameScene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NameScene);

    }
    private GameObject GetClickedObject()
    {
        var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 position = new Vector2(mousePos.origin.x, mousePos.origin.y);
        Collider2D hit = Physics2D.OverlapPoint(position);
        if (!hit)
        {
            return null;
        }
        return hit.gameObject;
    }
   
    public void Update()
    {

       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stopwatch.Start();
        }

        if ((Input.GetKeyUp(KeyCode.Mouse0)))
        {
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds <= 200)
            {
                
                GameObject ClickedObject = GetClickedObject();


                if (ClickedObject)
                {
                    RewardMapHolder rewardMapHolder = ClickedObject.GetComponent<RewardMapHolder>();
                    ShowRewardPanel(rewardMapHolder.Map);
                    currentMap = rewardMapHolder.Map;
                }
                else
                {
                   descriptionRewardHolder.SetActive(false);
                }

            }
            stopwatch.Reset();

        }

    }
    public void ShowRewardPanel(Map map)
    {
        descriptionRewardHolder.SetActive(true);
        var rewardHolder = descriptionRewardHolder.transform.GetChild(0);
            Image icon = rewardHolder.transform.GetChild(0).GetComponent<Image>();
            Text text = rewardHolder.transform.GetChild(1).GetComponent<Text>();
            //GameObject button = rewardHolder.transform.GetChild(2).gameObject;

        icon.sprite = map.RewardSprite;
        

        if (PlayerPrefs.GetInt("CurrentLevel", 3) - 1 >= map.MapIndex)
        {
            icon.color = Color.white;
            text.text = $"Получен за прохождение уровня {map.MapName}";
            //button.SetActive(true);
            
        }
        else
        {
            icon.color = Color.black;
            text.text = $"Можно получить за прохождение уровня {map.MapName}";
            //button.SetActive(false);
        }
    }
    public void SaveButtonClick()
    {
        spriteSaver.GetReadableTexture(currentMap.RewardSprite);
        spriteSaver.SaveSpriteToFile();
    }

}
