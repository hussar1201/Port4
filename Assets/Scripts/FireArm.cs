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
    public Animator animator_Bolt;
    private FiringMode mode_fire = FiringMode.auto;

    public Transform transform_Firing;

    private Rigidbody rb;
    private float time_interval_autofire = .15f;
    private float time_passed_interval_fire = 0f;
    public bool isAutoFireAvailable = true;
    private bool flag_trigger_released = true;
    private float time_interval_btn_pressed = .15f;
    private float time_passed_interval_btn_pressed = 0f;
    private bool flag_played_empty_sound = false;

    private SoundPlayer soundPlayer;

    public void Grabbed()
    {
        Debug.Log("is grabbed");
        isGrabbed = true;
        rb.useGravity = false;
    }

    public void UnGrabbed()
    {
        Debug.Log("is ungrabbed");
        isGrabbed = false;
        rb.useGravity = true;
    }

    public void Fire()
    {
        if (chamber.isAmmoOnChamber)
        {
            Debug.Log("GUN: FIRE!!!");
            MakeRayCast();
            soundPlayer.PlayOneShot(SoundPlayer.Part.shot, 0);
            animator_Bolt.SetBool("Firing", true);
            StartCoroutine(ShowMuzzleBreak());           
            chamber.LoadAmmo();
            if (!chamber.isAmmoOnChamber)
            {
                Debug.Log("GUN: Out Of Ammo");
                chamber.isBoltOpened = true;
                animator_Bolt.SetBool("isBoltOpened", chamber.isBoltOpened);
                isLoaded = false;
                magCatcher.flag_new_mag_inserted = false;
                soundPlayer.PlayOneShot(SoundPlayer.Part.etc, 1);
            }
        }
        else
        {
            if (!flag_played_empty_sound)
            {
                soundPlayer.PlayOneShot(SoundPlayer.Part.shot, 1);
                flag_played_empty_sound = true;
            }
        }
    }

    private void MakeRayCast()
    {
        if (Physics.Raycast(transform_Firing.position, transform_Firing.forward, out RaycastHit hitInfo, 100f))
        {
            IDamageable tmp = hitInfo.collider.GetComponent<IDamageable>();
            if (tmp != null)
            {
                tmp.OnDamage(hitInfo.point, hitInfo.normal);
            }

        }
    }

    IEnumerator ShowMuzzleBreak()
    {

        if (!ps.isPlaying)
        {
            ps.gameObject.SetActive(true);
            ps.Play();
            
            yield return new WaitForSeconds(.2f);
            ps.transform.Rotate(0f, 0f, 30f);
            animator_Bolt.SetBool("Firing", false);

            /*
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(.05f);
                ps.transform.Rotate(0f, 0f, 60f);
            }
            animator_Bolt.SetBool("Firing", false);
            //yield return new WaitForSeconds(.05f);
            //ps.transform.Rotate(0f, 0f, -90f);
            */

            ps.gameObject.SetActive(false);
        }



    }


    private void Start()
    {
        //연발모드 
        if (!isAutoFireAvailable) mode_fire = FiringMode.semi;

        animator_Selecter.SetBool("flag_Auto", mode_fire == FiringMode.auto);

        rb = GetComponent<Rigidbody>();

        //소리 재생 컴포넌트 참조
        soundPlayer = GetComponent<SoundPlayer>();

        ps.Play();
        ps.Pause();
        ps.gameObject.SetActive(false);


    }

    private void CheckFireMode()
    {

        if (mode_fire == FiringMode.semi && isAutoFireAvailable)
        {
            mode_fire = FiringMode.auto;
            Debug.Log("Auto");
            animator_Selecter.SetBool("flag_Auto", true);
            return;
        }
        else if (mode_fire == FiringMode.auto)
        {
            Debug.Log("Semi");
            mode_fire = FiringMode.semi;
            animator_Selecter.SetBool("flag_Auto", false);
            return;
        }

    }


    private void Update()
    {
        if (!isGrabbed) return; // 플레이어가 잡고 있지 않으면 작동X
        time_passed_interval_fire += Time.deltaTime;//단발 발사 시, 인터벌 계산 위한 코드
        time_passed_interval_btn_pressed += Time.deltaTime; // 버튼 오작동 방지

        if (time_passed_interval_btn_pressed >= time_interval_btn_pressed)
        {
            time_passed_interval_btn_pressed = 0f;

            if (InputController_XR.instance.Btn_A) // 단발,연발 모드 변경 
            {
                CheckFireMode();
                soundPlayer.Play(SoundPlayer.Part.etc, 0);
            }

            if (mag_attached)
            {
                if (InputController_XR.instance.Btn_B) // 장전 관련
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

        if (InputController_XR.instance.trigger_R >= 0.8f) //사격
        {

            if (mode_fire == FiringMode.auto)
            {
                if (time_passed_interval_fire >= time_interval_autofire)
                {
                    Fire();
                    time_passed_interval_fire = 0f;
                    flag_trigger_released = false;
                }
            }
            else if (mode_fire == FiringMode.semi && flag_trigger_released)
            {
                Fire();
                flag_trigger_released = false;
            }
        }

        if (InputController_XR.instance.trigger_R <= 0.5f)
        {
            flag_trigger_released = true; //단발 사격시 방아쇠 관련 플래그
            flag_played_empty_sound = false;
        }
    }
}
