using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip, AudioSource sourse)
    {
        sourse.clip = clip;
        sourse.Play();
    }

    public void PlayRandomClip(AudioClip[] clips, AudioSource sourse )
    {
        int randomIndex = Random.Range(0, clips.Length) ;

        sourse.clip = clips[randomIndex];
        sourse.Play();
    }

}
