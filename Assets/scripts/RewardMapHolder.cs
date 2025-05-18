using UnityEngine;
using UnityEngine.UI;

public class RewardMapHolder : MonoBehaviour
{
    public Map Map { get; private set;}
    public void SetRewardMap(Map map)
    {
        Map = map;

        Image icon = this.transform.GetChild(0).GetComponent<Image>();

        icon.sprite = map.RewardSprite;

        if (PlayerPrefs.GetInt("CurrentLevel", 3) - 1 >= map.MapIndex)
        {
            icon.color = Color.white;
        }
        else
        {
            icon.color = Color.black;
        }
    }


}
