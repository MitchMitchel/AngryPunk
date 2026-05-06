using UnityEngine;

public class LevelSoundClip : MonoBehaviour
{
    public AudioClip levelClip;



    public void LevelSound()
    {
        AudioSource.PlayClipAtPoint(levelClip, Camera.main.transform.position);
    }
}
