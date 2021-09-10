using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    public int num_initial;
    public int ammo_present;

    private bool flag_setDestroy = false;
    public Rigidbody rb;
    public Collider coll;
    public bool flag_IsAmmoLeft { get; private set; }
    public GameObject[] arr_objects_child;
    private BoxCollider[] arr_Colliders;

    private void OnEnable()
    {
        ammo_present = num_initial;
        coll = GetComponent<Collider>();
        if (ammo_present > 0) flag_IsAmmoLeft = true;
        rb = GetComponent<Rigidbody>();
        arr_Colliders = GetComponentsInChildren<BoxCollider>();
    }

    public bool SendAmmoToChamber()
    {
        if (ammo_present > 0)
        {
            ammo_present--;
            flag_IsAmmoLeft = true;
        }
        else
        {
            ammo_present = 0;
            flag_IsAmmoLeft = false;
        }
        return flag_IsAmmoLeft;

    }

    public void Eject()
    {
        rb.AddForce(transform.forward * -0.7f, ForceMode.Impulse);
    }


    ~Mag()
    {

        for (int i = 0; i < arr_objects_child.Length; i++)
        {
            Destroy(arr_objects_child[i]);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Hand"))
        {
            rb.useGravity = false;
            for(int i =0;i<arr_Colliders.Length;i++)
            {
                arr_Colliders[i].isTrigger = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Hand"))
        {
            rb.useGravity = true;
            for (int i = 0; i < arr_Colliders.Length; i++)
            {
                arr_Colliders[i].isTrigger = false;
            }
        }
    }




}
