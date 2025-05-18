using UnityEngine;
using UnityEngine.UI;

public class RewardPanelInfo : MonoBehaviour   
{
    public void ShowRewardPanel(Map map)
    {
        this.gameObject.SetActive(true);
        Image icon = this.transform.GetChild(0).GetComponent<Image>();
        Text text = this.transform.GetChild(1).GetComponent<Text>();

        icon.sprite = map.RewardSprite;
        text.text = $"Получен за прохождение уровня {map.MapName}";


    }
   
}
