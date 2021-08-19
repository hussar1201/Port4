using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Animator animator_target;
    public GameObject prefab_target;
    private GameObject target_body;
    private PracticeTarget_Collider[] arr_colliders;

    public int num_point_hit
    {
        get;
        private set;
    }

    private int hitpoint_remain;

    private void OnEnable()
    {                
        target_body = Instantiate(prefab_target,transform.position,Quaternion.identity);
        target_body.transform.SetParent(transform);
        arr_colliders = GetComponentsInChildren<PracticeTarget_Collider>();
        hitpoint_remain = num_point_hit = arr_colliders.Length;       

        for(int i = 0;i< num_point_hit;i++)
        {
            arr_colliders[i].OnParentAcquired();
        }

    }

    private void Start()
    {
        animator_target = GetComponent<Animator>();
    }


    public void SetUpTarget()
    {
        animator_target.SetBool("Rise", true);
    }

    public void ResetTarget()
    {
        animator_target.SetBool("Rise", false);
        Destroy(target_body,0.3f);
        OnEnable();
        SetUpTarget();
    }

    public void RemoveHitPoint()
    {
        hitpoint_remain--;
        Debug.Log("one Part gone");
    }







}
