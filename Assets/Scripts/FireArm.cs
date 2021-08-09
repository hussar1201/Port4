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
    public ParticleSystem ps = null;

    private enum FiringMode { semi, auto };
    public Animator animator_Selecter;
    private FiringMode mode_fire = FiringMode.auto;
    private float time_interval_autofire = .1f;
    private float time_passed_interval_fire = 0f;
    public bool isAutoFireAvailable = true;
    private bool flag_trigger_released = true;
    private float time_interval_btn_pressed = .2f;
    private float time_passed_interval_btn_pressed = 0f;

    private SoundPlayer soundPlayer;
    
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
            soundPlayer.PlayOneShot(SoundPlayer.Part.shot, 0);
            chamber.LoadAmmo();
            if (!chamber.isAmmoOnChamber)
            {
                Debug.Log("GUN: Out Of Ammo");
                chamber.isBoltOpened = true;
                isLoaded = false;
                magCatcher.flag_new_mag_inserted = false;
                soundPlayer.PlayOneShot(SoundPlayer.Part.etc, 1);

            }
        }
        else
        {
            soundPlayer.PlayOneShot(SoundPlayer.Part.shot, 1);
        }
    }

    IEnumerator ShowMuzzleBreak()
    {
        ps.gameObject.SetActive(true);
        ps.Play();
        for(int i=0; i < 3; i++) { 
            yield return new WaitForSeconds(.1f);
            ps.transform.Rotate(30f, 0f, 0f);
        }
        yield return new WaitForSeconds(.05f);
        ps.transform.Rotate(-90f, 0f, 0f);
        yield return new WaitForSeconds(.05f);
        ps.Pause();
        ps.gameObject.SetActive(false);
    }


    private void Start()
    {
        //���߸�� 
        if (!isAutoFireAvailable) mode_fire = FiringMode.semi;
        
        animator_Selecter.SetBool("isAuto", mode_fire == FiringMode.auto);


        //�Ҹ� ��� ������Ʈ ����
        soundPlayer = GetComponent<SoundPlayer>();

        ps.Play();
        ps.Pause();
        ps.gameObject.SetActive(false);       
        
  
    }

    private void CheckFireMode()
    {
        Debug.Log("Check Selecter");
        if (mode_fire == FiringMode.semi && isAutoFireAvailable)
        {
            mode_fire = FiringMode.auto;
            animator_Selecter.SetBool("isAuto", true);
        }
        if (mode_fire == FiringMode.auto)
        {
            mode_fire = FiringMode.semi;
            animator_Selecter.SetBool("isAuto", false);
        }

    }


    private void Update()
    {
        if (!isGrabbed) return; // �÷��̾ ��� ���� ������ �۵�X
        time_passed_interval_fire += Time.deltaTime;//�ܹ� �߻� ��, ���͹� ��� ���� �ڵ�
        time_passed_interval_btn_pressed += Time.deltaTime; // ��ư ���۵� ����

        if (time_passed_interval_btn_pressed >= time_interval_btn_pressed)
        {
            time_passed_interval_btn_pressed = 0f;

            if (InputController_XR.instance.Btn_A) // �ܹ�,���� ��� ���� 
            {
                CheckFireMode();
                soundPlayer.Play(SoundPlayer.Part.etc, 0);
            }

            if (mag_attached)
            {
                if (InputController_XR.instance.Btn_B) // ���� ����
                {
                    Debug.Log("New MAG: " + magCatcher.flag_new_mag_inserted);
                    Debug.Log("BoltOpen: " + chamber.isBoltOpened + "   AmmoOnChamber: " + chamber.isAmmoOnChamber);

                    if (chamber.isBoltOpened && !chamber.isAmmoOnChamber)
                    {

                        if (magCatcher.flag_new_mag_inserted)
                        {
                            Debug.Log("RELOADED BY BOLTCATCHER");
                            soundPlayer.PlayOneShot(SoundPlayer.Part.etc, 1);
                            chamber.LoadAmmo();
                            isLoaded = true;
                        }
                        else magCatcher.EjectMag();
                    }
                    else magCatcher.EjectMag();
                }
                else if (handle.flag_loaded)
                {
                    chamber.LoadAmmo();
                    handle.OnGunLoaded();
                    isLoaded = true;
                }
            }

        }

        if (InputController_XR.instance.trigger_R >= 0.8f) //���
        {
            if (mode_fire == FiringMode.auto)
            {
                if (time_passed_interval_fire >= time_interval_autofire)
                {
                    Fire();
                    time_passed_interval_fire = 0f;
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
