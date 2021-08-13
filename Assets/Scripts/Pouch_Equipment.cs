using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Pouch_Equipment : XRSocketInteractor
{
    public Mag prefab_mag;
    private Mag tmp_Mag;


    private bool is_Mag_Existed = false;
    private bool is_Mag_Drawed = false;

    protected override void Start()
    {
        this.socketActive = false;         
    }


    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log("OnHoverExited");

        if (args.interactable.gameObject.GetInstanceID() == tmp_Mag.gameObject.GetInstanceID())
        {
            is_Mag_Drawed = true;
            is_Mag_Existed = false;
            tmp_Mag = null;
            Debug.Log("Mag Drawed");
        }
        else
        {
            is_Mag_Drawed = false;
        }


        base.OnHoverExited(args);
    }

    public void CreateItem()
    {
        if (!is_Mag_Existed)
        {
            //tmp_Mag = Instantiate(prefab_mag, transform.position, transform.rotation * Quaternion.Euler(0f, 90f, 90f));
            tmp_Mag = Instantiate(prefab_mag, transform.position, transform.rotation);
            is_Mag_Existed = true;
            is_Mag_Drawed = false;
            Debug.Log("Mag Created");
        }
    }

    public void DestroyItem()
    {
        if (!is_Mag_Drawed && tmp_Mag!=null)
        {
            Destroy(tmp_Mag.gameObject);
            tmp_Mag = null;
            is_Mag_Existed = false;
            is_Mag_Drawed = false;
            Debug.Log("Undrawed Mag Destroyed");
        }
    }

}
