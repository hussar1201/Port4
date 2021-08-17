using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Animator animator_target;
    public GameObject prefab_target;
    private GameObject target_body;


    private void OnEnable()
    {
        target_body = Instantiate(prefab_target);
    }

    private void Start()
    {
        animator_target = GetComponent<Animator>();
        SetUpTarget();
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



}
