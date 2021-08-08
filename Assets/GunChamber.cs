using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChamber : MonoBehaviour
{
    private FireArm fireArm;
    private Mag mag;
    public bool isBoltOpened;
    public bool isAmmoOnChamber;

    // Start is called before the first frame update
    void Start()
    {
        fireArm = GetComponentInParent<FireArm>();
        mag = fireArm.mag_attached;
        isBoltOpened = false;
        isAmmoOnChamber = false;
    }

    public void LoadAmmo()
    {
        mag = fireArm.mag_attached;
        if (mag!=null)
        {
            bool result = mag.SendAmmoToChamber();
            if(result)
            {
                isBoltOpened = false;
                isAmmoOnChamber = true;
                Debug.Log("Chamber:  Ammo Loaded");
            }
            else
            {
                isBoltOpened = false;
                isAmmoOnChamber = false;
            }          
        }
        else
        {
            isBoltOpened = false;
            isAmmoOnChamber = false;
        }

    }




}
