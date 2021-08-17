using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    public Vector3 offset_pos;
    public Vector3 offset_rot;
    public Transform tgt_follow;
    public bool flag_set_forward_with_tgt;


    // Update is called once per frame
    void Update()
    {
        transform.position = tgt_follow.position + offset_pos;
        transform.rotation = tgt_follow.rotation * Quaternion.Euler(offset_rot);
        
        if(flag_set_forward_with_tgt) transform.forward = Vector3.ProjectOnPlane(tgt_follow.forward, Vector3.up).normalized;
    }

}
