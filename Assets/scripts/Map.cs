using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Map", menuName = "Map")]
public class Map : ScriptableObject
{
    public int MapIndex;
    public string MapName;
    public string MapDescription;
    public Sprite MapSprite;
    public Sprite RewardSprite;
    public Scene SceneToload;
}
