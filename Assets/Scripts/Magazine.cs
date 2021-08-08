using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int ammo_initial;
    public int ammo_internal;
    private Rigidbody rb;

    public bool flag_IsAmmoLeft { get; private set; }

    private void OnEnable()
    {
        ammo_internal = ammo_initial;
        if(ammo_internal > 0) flag_IsAmmoLeft = true;
        rb = GetComponent<Rigidbody>();
    }

    public bool ConsumeAmmo()
    {

        if (ammo_internal-- > 0)
        { 
            flag_IsAmmoLeft = true;
        }
        else
        {
            flag_IsAmmoLeft = false;
            ammo_internal = 0;
        }
        
        Debug.Log(ammo_internal);
        
        return flag_IsAmmoLeft;
    }


    public void Eject()
    {
        Debug.Log("Ejected");
        
        rb.AddForce(transform.forward * -1f, ForceMode.Impulse);
       
    }




}
