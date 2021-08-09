using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public enum Part { shot, slide, mag, etc };

    public AudioClip[] audioClips_shot;
    public AudioClip[] audioClips_slide;
    public AudioClip[] audioClips_mag;
    public AudioClip[] audioClips_etc;

    private List<AudioClip[]> list_arrClips = new List<AudioClip[]>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        list_arrClips.Add(audioClips_shot);
        list_arrClips.Add(audioClips_slide); 
        list_arrClips.Add(audioClips_mag);
        list_arrClips.Add(audioClips_etc);
    }

    public void PlayOneShot(Part part, int num)
    {
        audioSource.PlayOneShot(list_arrClips[(int)part][num]);
    }

    public void Play(Part part, int num)
    {
        audioSource.clip = list_arrClips[(int)part][num];
        audioSource.Play();
    }


}
