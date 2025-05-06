using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip FindObject;
    private AudioSource SoundPlay;
    public static SoundManager instance; 
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        SoundPlay = this.GetComponent<AudioSource>();
    }
    public void PlayButtonEffect()
    {
        SoundPlay.PlayOneShot(audioClip);
        //SoundPlay.Play();
        
    }
    public void PlayHelpEffect(AudioClip hiddendObjectSound)
    {
        Debug.Log(hiddendObjectSound.name);
        SoundPlay.PlayOneShot(hiddendObjectSound);

    }
}
