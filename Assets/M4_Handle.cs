using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4_Handle : MonoBehaviour
{
    private bool flag_pulled = false;
    private bool flag_released = false;
    public bool flag_loaded { get; private set; }
    public Collider collider_pulled;
    public Collider collider_released;

    private void Start()
    {
        flag_loaded = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Collider>().Equals(collider_pulled))
        {
            flag_pulled = true;            
        }

        if (other.GetComponent<Collider>().Equals(collider_released))
        {
            flag_released = true;

            if (flag_loaded = flag_pulled && flag_released)
            {
                Debug.Log("Handle_Loaded");
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
        }


    }




}









