using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GalleryManager : MonoBehaviour
{
    public Map[] MapObjects;
    [SerializeField] private GameObject hiddenIvonHolder;
    [SerializeField] private GameObject hiddenIvonPrefab;

    public AudioSource SoundPlay;

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

        Image childImg = icon.transform.GetChild(0).GetComponent<Image>();
        childImg.sprite = map.RewardSprite;

        if (PlayerPrefs.GetInt("CurrentLevel", 0) - 1 >= map.MapIndex)
        {
            childImg.color = Color.white;
        }
        else
        {
            childImg.color = Color.black;
        }
    }


    public void LoadMenuScene()
    {
        SoundManager.instance.PlayButtonEffect();
        SceneManager.LoadScene("MenuScene");
    }


}
