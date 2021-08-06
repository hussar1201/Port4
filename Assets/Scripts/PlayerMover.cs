using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerMover : MonoBehaviour
{
    private CharacterController cc;
    private XRRig xrRig;

    public float speed_player = 5f;
    private float speed_gravity = -9.81f;
    private float speed_falling = 0f;
    public LayerMask lm;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
        xrRig = GetComponent<XRRig>();
    }

    private void Update()
    {
        FollowCamera();
                
        Quaternion rotation = Quaternion.Euler(0f, xrRig.cameraGameObject.transform.eulerAngles.y, 0f);
        Vector3 direction = rotation * new Vector3(InputController_XR.instance.axis_XY_R.x, 0f, InputController_XR.instance.axis_XY_R.y) ;
        cc.Move(direction * speed_player * Time.deltaTime);
        
        if (!isGround())
        {
            speed_falling += speed_gravity * Time.deltaTime;
        }
        else speed_falling = 0f;
        cc.Move(Vector3.up * speed_falling * Time.deltaTime);

    }

    void FollowCamera()
    {
        cc.height = xrRig.cameraInRigSpaceHeight + 0.2f;
        Vector3 capsuleCenter = transform.InverseTransformPoint(xrRig.cameraGameObject.transform.position);
        cc.center = new Vector3(capsuleCenter.x, cc.height/2 + cc.skinWidth, capsuleCenter.z);       
    }

    bool isGround()
    {
        Vector3 pos = transform.TransformPoint(cc.center);
        float raySize = cc.center.y;
        bool res = Physics.SphereCast(pos, cc.radius, Vector3.down, out RaycastHit hitInfo, raySize, lm);    
        return res;
    }





}
