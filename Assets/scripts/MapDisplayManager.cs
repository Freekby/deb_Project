using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapDisplayManager : MonoBehaviour
{
    public Text _mapName;
    public Text _mapdescription;
    public Image _mapImage;
    public Button _playbutton;
    public GameObject LockImage;
    public Image _rewardImage;

    public AudioSource SoundPlay;
    public Animator transition;

    public void DisplayMap(Map map)
    {
        _mapName.text = map.MapName;
        _mapdescription.text = map.MapDescription;
        _mapImage.sprite = map.MapSprite;
        _rewardImage.sprite = map.RewardSprite;

        bool MapUnlocked = PlayerPrefs.GetInt("CurrentLevel", 0) >= map.MapIndex;

        LockImage.SetActive(!MapUnlocked);
        _playbutton.interactable = MapUnlocked;
        if (MapUnlocked)
        {
            _mapImage.color = Color.white;
        }
        else
        {
            _mapImage.color = Color.gray;
        }

        if (PlayerPrefs.GetInt("CurrentLevel", 0)-1 >= map.MapIndex)
        {
            _rewardImage.color = Color.white;
        }
        else
        {
            _rewardImage.color = Color.black;
        }
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
}
