using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Magazine mag_attached = null;

    private bool isGrabbed = false;
    public bool isLoaded { get; private set; }

    public Gun_Slide handle;
    public CheckableXRSocketInteractor magCatcher;
    

    private enum FiringMode { semi, auto };
    private FiringMode mode_fire = FiringMode.auto;

    private bool isBoltCatchOpened = false;
    public bool isAutoFireAvailable = true;
    private bool flag_trigger_released = true;


    private float time_interval_autofire = .1f;
    private float time_passed = 0f;

    private SoundPlayer soundPlayer;

    private enum num_sound { releaseBoltCatch, single_shot, switch_fake, empty_gun, silencer_shot };
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
        
        if (mag_attached && mag_attached.flag_IsAmmoLeft && isLoaded)
        {
            Debug.Log("Firing_onMag");
            mag_attached.ConsumeAmmo();
            soundPlayer.PlaySound(SoundPlayer.Part.frame, (int)num_sound.single_shot);
            isBoltCatchOpened = false;
        }
        else if (isAmmoOnChamber)
        {
            Debug.Log("Firing_onChamber");
            isAmmoOnChamber = false;
            isBoltCatchOpened = true;
            soundPlayer.PlaySound(SoundPlayer.Part.frame, (int)num_sound.single_shot);
        }
        else
        {
            Debug.Log("Ammo Low");
            isLoaded = false;
            isBoltCatchOpened = true;
            soundPlayer.PlaySound(SoundPlayer.Part.frame, (int)num_sound.empty_gun);
        }

    }

    private void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
        if (!isAutoFireAvailable) mode_fire = FiringMode.semi;
    }


    private void Update()
    {
        if (!isGrabbed) return; // 플레이어가 잡고 있지 않으면 작동X
        time_passed += Time.deltaTime; 

        if (InputController_XR.instance.Btn_A) // 단발,연발 모드 변경
        {
            if (mode_fire == FiringMode.semi && isAutoFireAvailable) mode_fire = FiringMode.auto;
            if (mode_fire == FiringMode.auto) mode_fire = FiringMode.semi;
            
        }

        if (mag_attached != null)
        {
            if (InputController_XR.instance.Btn_B && !isBoltCatchOpened && isLoaded) // 탄창 사출
            {
                magCatcher.EjectMag();              
                isLoaded = false;
                return;
            }
        }


        if (InputController_XR.instance.trigger_R >= 0.8f) //사격
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
        if (InputController_XR.instance.trigger_R <= 0.5f) flag_trigger_released = true; //단발 사격시 방아쇠 관련 플래그


        if (!isLoaded && mag_attached)
        {
            bool boltcatch = false;
            if (boltcatch = (InputController_XR.instance.Btn_B && isBoltCatchOpened) || handle.flag_loaded) // 장전 관련
            {
                isLoaded = true;
                if (boltcatch) soundPlayer.PlaySound(SoundPlayer.Part.frame, (int)num_sound.releaseBoltCatch);

                if (!isAmmoOnChamber && mag_attached.flag_IsAmmoLeft)
                {
                    isAmmoOnChamber = true;
                }
                Debug.Log("Gun_Loaded");
                handle.OnGunLoaded();
            }
        }





    }


}
