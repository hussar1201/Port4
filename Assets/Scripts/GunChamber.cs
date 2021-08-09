using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChamber : MonoBehaviour
{
    private FireArm fireArm;
    private Mag mag;
    public bool isBoltOpened;
    public bool isAmmoOnChamber;
    private Animator animator;
    public GameObject prefab_EmptyCase;
    public GameObject prefab_Ammo;
    public Transform pos_eject_case_spawn;
    public Transform pos_eject_case_headingto;



    // Start is called before the first frame update
    void Start()
    {
        fireArm = GetComponentInParent<FireArm>();
        animator = GetComponent<Animator>();

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
            animator.SetBool("isBoltOpened", isBoltOpened);
        }
        else
        {
            isBoltOpened = false;
            isAmmoOnChamber = false;
            animator.SetBool("isBoltOpened", isBoltOpened);
        }
        
    }

    public void ReleaseCase()
    {
        Debug.Log("case created");
        //GameObject tmp = Instantiate(prefab_EmptyCase, pos_eject_case_spawn.position, transform.rotation);
        GameObject tmp = Instantiate(prefab_EmptyCase, pos_eject_case_spawn.position, Quaternion.Euler(0f,180f,0f)*pos_eject_case_spawn.rotation);
        
        
        

        Rigidbody rb = tmp.GetComponent<Rigidbody>();
        rb.AddForce((pos_eject_case_headingto.position - pos_eject_case_spawn.position).normalized*3, ForceMode.Impulse);

    }




}
