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
        //transform.rotation = tgt_follow.rotation * Quaternion.Euler(offset_rot);
        transform.position = tgt_follow.position + offset_pos;        

    }
}
