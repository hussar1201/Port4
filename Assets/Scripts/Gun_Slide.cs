using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Slide : MonoBehaviour
{
    private bool flag_pulled = false;
    private bool flag_released = false;
    public bool flag_loaded { get; private set; }
    private bool flag_played_animation_DustCover = false;
    public Collider collider_pulled;
    public Collider collider_released;
    public SoundPlayer soundPlayer;
    public Animator animator_DustCover;
    
    private void Start()
    {
        flag_loaded = false;
        soundPlayer = GetComponentInParent<SoundPlayer>();      
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().Equals(collider_pulled))
        {
            flag_pulled = true;
            soundPlayer.PlayOneShot(SoundPlayer.Part.slide, 0);           
        }

        if (other.GetComponent<Collider>().Equals(collider_released))
        {
            flag_released = true;

            if (flag_loaded = flag_pulled && flag_released)
            {
                Debug.Log(flag_pulled + " " + flag_released+ "    Handle_Loaded");
                soundPlayer.PlayOneShot(SoundPlayer.Part.slide, 1);
                flag_pulled = false;
            }            
        }  
    }

    public void OnGunLoaded()
    {
        flag_loaded = false;        
        Debug.Log("Handle_Loaded_Finished");
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.GetComponent<Collider>().Equals(collider_released))
        {
            flag_released = false;
            animator_DustCover.SetBool("Dustcover_Open", true);            
        }


    }




}









