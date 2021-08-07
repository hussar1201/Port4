using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Magazine mag_attached = null;

    private bool isGrabbed = false;
    private bool isLoaded = false;

    public M4_Handle handle;
    public CheckableXRSocketInteractor magCatcher;

    private enum FiringMode { semi, auto };
    private FiringMode mode_fire = FiringMode.auto;

    private bool flag_trigger_released = true;

    private float time_interval_autofire = 1f;
    private float time_passed = 0f;


    public bool isAmmoOnChamber = false;


    public void Grabbed()
    {
        Debug.Log("is grabbed");
        isGrabbed = true;
    }

    public void UnGrabbed()
    {
        Debug.Log("is ungrabbed");
        isGrabbed = false;
    }

    public void Fire()
    {


        if (mag_attached && mag_attached.IsAmmoLeft())
        {
            Debug.Log("Firing_onMag");
        }
        else if (isAmmoOnChamber)
        {
            Debug.Log("Firing_onChamber");
            isAmmoOnChamber = false;
        }
        else
        {
            Debug.Log("Ammo Low");
            isLoaded = false;
        }


    }


    private void Update()
    {
        
        if (isGrabbed)
        {
            time_passed += Time.deltaTime;

            if (InputController_XR.instance.Btn_A)
            {
               //사격모드 변환 
            }


            if (mag_attached != null)
            {
                if (InputController_XR.instance.Btn_B)
                {
                    magCatcher.EjectMag();
                    return;
                }

                if (InputController_XR.instance.trigger_R >= 0.9f)
                {
                    if (mode_fire == FiringMode.auto)
                    {
                        if (time_passed >= time_interval_autofire)
                        {
                            Fire();
                            time_passed = 0f;
                        }
                    } 
                    else if (mode_fire == FiringMode.semi && flag_trigger_released)
                    {
                        Fire();
                        flag_trigger_released = false;
                    }
                }
            }

            if (InputController_XR.instance.trigger_R <= 0.5f) flag_trigger_released = true;


            /*
            if (!isLoaded && mag_attached)
            {
                if (InputController_XR.instance.Btn_B || handle.flag_loaded)
                {
                    isLoaded = true;
                    Debug.Log("Gun_Loaded");
                    handle.OnGunLoaded();
                }

            }
            */

        }
    }


}
