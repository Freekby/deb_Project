using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Map")]
public class Map : ScriptableObject
{
    public int MapIndex;
    public string MapName;
    public string MapDescription;
    public Sprite MapSprite;
    public Object SceneToload;
}
