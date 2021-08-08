using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    public int num_initial;
    public int ammo_present;

    private Rigidbody rb;
    public bool flag_IsAmmoLeft { get; private set; }

    private void OnEnable()
    {
        ammo_present = num_initial;
        if(ammo_present > 0) flag_IsAmmoLeft = true;
        rb = GetComponent<Rigidbody>();
    }

    public bool SendAmmoToChamber()
    {
        if(ammo_present > 0)
        {
            ammo_present--;
             flag_IsAmmoLeft=true;
        }
        else
        {
            ammo_present = 0;
             flag_IsAmmoLeft = false;
        }
        Debug.Log("Mag: " + ammo_present);
        return flag_IsAmmoLeft;
        
    }

    public void Eject()
    {
        rb.AddForce(transform.forward * -0.1f, ForceMode.Impulse);     
    }
}
