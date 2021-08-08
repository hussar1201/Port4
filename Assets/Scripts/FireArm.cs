using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{

    private bool isGrabbed = false;
    public bool isLoaded { get; private set; }

    public Mag mag_attached = null;
    public Gun_Slide handle = null;
    public MagCatcher magCatcher = null;
    public GunChamber chamber = null;

    private enum FiringMode { semi, auto };
    private FiringMode mode_fire = FiringMode.auto;
    private float time_interval_autofire = .1f;
    private float time_passed = 0f;
    public bool isAutoFireAvailable = true;
    private bool flag_trigger_released = true;

    private SoundPlayer soundPlayer;
    private enum num_sound { releaseBoltCatch, single_shot, switch_fake, empty_gun, silencer_shot };


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
        if (chamber.isAmmoOnChamber)
        {
            Debug.Log("GUN: FIRE!!!");
            //�� �߻��� ���
            chamber.LoadAmmo();
            if (!chamber.isAmmoOnChamber)
            {
                Debug.Log("GUN: Out Of Ammo");
                chamber.isBoltOpened = true;
                isLoaded = false;
                magCatcher.flag_new_mag_inserted = false;
                //��Ʈ ���� �Ҹ� ���

            }
        }
        else
        {
            //���� �߻��� ���
        }
    }

    private void Start()
    {
        //���߸�� 
        if (!isAutoFireAvailable) mode_fire = FiringMode.semi;

        //�Ҹ� ��� ������Ʈ ����
        soundPlayer = GetComponent<SoundPlayer>();

        //���� ������ǰ ������Ʈ ����
        mag_attached = GetComponentInChildren<Mag>();
        handle = GetComponentInChildren<Gun_Slide>();
        magCatcher = GetComponentInChildren<MagCatcher>();
        chamber = GetComponentInChildren<GunChamber>();
    }


    private void Update()
    {
        if (!isGrabbed) return; // �÷��̾ ��� ���� ������ �۵�X
        
        time_passed += Time.deltaTime; //�ܹ� �߻� ��, ���͹� ��� ���� �ڵ�

        if (InputController_XR.instance.Btn_A) // �ܹ�,���� ��� ���� <- �Ҹ� ����� �ణ ���� �������� ���� �ʿ�
        {
            if (mode_fire == FiringMode.semi && isAutoFireAvailable) mode_fire = FiringMode.auto;
            if (mode_fire == FiringMode.auto) mode_fire = FiringMode.semi;
        }

        if (mag_attached)
        {                   
            if (InputController_XR.instance.Btn_B) // ���� ����
            {
                Debug.Log("New MAG: " + magCatcher.flag_new_mag_inserted);
                Debug.Log("Bolt: " + chamber.isBoltOpened + "   AmmoOnChamber" + chamber.isAmmoOnChamber);

                if(chamber.isBoltOpened && !chamber.isAmmoOnChamber)
                {
                    //soundPlayer.PlaySound(SoundPlayer.Part.frame, (int)num_sound.releaseBoltCatch);                  
                    if (magCatcher.flag_new_mag_inserted)
                    {
                        Debug.Log("RELOADED BY BOLTCATCHER");
                        chamber.LoadAmmo();
                        isLoaded=true;
                    }
                    else magCatcher.EjectMag();
                }else magCatcher.EjectMag();
            }
            else if (handle.flag_loaded)
            {
                chamber.LoadAmmo();
                handle.OnGunLoaded();
                isLoaded = true;
            }
        }

        if (InputController_XR.instance.trigger_R >= 0.8f) //���
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
        if (InputController_XR.instance.trigger_R <= 0.5f) flag_trigger_released = true; //�ܹ� ��ݽ� ��Ƽ� ���� �÷���
    }
}
