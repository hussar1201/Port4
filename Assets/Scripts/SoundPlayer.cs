using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public enum Part { frame, slide, mag };

    public AudioClip[] audioClips_frame;
    public AudioClip[] audioClips_slide;
    public AudioClip[] audioClips_mag;

    private List<AudioClip[]> list_arrClips = new List<AudioClip[]>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        list_arrClips.Add(audioClips_frame);
        list_arrClips.Add(audioClips_slide); 
        list_arrClips.Add(audioClips_mag);
    }

    public void PlaySound(Part part, int num)
    {
        audioSource.PlayOneShot(list_arrClips[(int)part][num]);
    }   


}
