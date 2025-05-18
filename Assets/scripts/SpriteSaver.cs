using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteSaver : MonoBehaviour
{
    [SerializeField] private Texture2D targetSprite;

    public void SaveSpriteToFile()
    {

        Texture2D texture = targetSprite;
        byte[] bytes = texture.EncodeToPNG();


 
        string filename = "sprite_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        string path = System.IO.Path.Combine(Application.persistentDataPath, filename);
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("Saved to: " + path);

    }

    public void GetReadableTexture(Sprite sprite)
    {
        // —оздаем новую текстуру с правильными размерами
        Texture2D newTexture = new Texture2D(
            (int)sprite.rect.width,
            (int)sprite.rect.height,
            sprite.texture.format,
            false
        );

        //  опируем пиксели из оригинальной текстуры
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x,
            (int)sprite.rect.y,
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );

        newTexture.SetPixels(pixels);
        newTexture.Apply();
        targetSprite = newTexture;
        
    }
}