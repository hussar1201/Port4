using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    public Vector3 offset_pos;
    public Vector3 offset_rot;
    public Transform tgt_follow;

    // Update is called once per frame
    void Update()
    {
        transform.position = tgt_follow.position + offset_pos;
        transform.forward = Vector3.ProjectOnPlane(tgt_follow.forward, Vector3.up).normalized;
    }

}
