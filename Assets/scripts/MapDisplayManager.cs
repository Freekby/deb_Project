using UnityEngine;
using UnityEngine.UI;

public class MapDisplayManager : MonoBehaviour
{
    public Text _mapName;
    public Text _mapdescription;
    public Image _mapImage;

    public void DisplayMap(Map map)
    {
        _mapName.text = map.MapName;
        _mapdescription.text = map.MapDescription;
        _mapImage.sprite = map.MapSprite;
    }
}
