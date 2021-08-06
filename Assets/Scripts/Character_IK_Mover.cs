using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public enum mode { head, hand};
    

    public Transform rig_Character, rig_Device;
    public Vector3 offset_position, offset_rotation;

    public void Map(mode selectedMode)
    {
        rig_Character.position = rig_Device.TransformPoint(offset_position);
        //if (selectedMode == mode.head) rig_Character.rotation = Quaternion.Euler(rig_Device.rotation.x * 0.8f, rig_Device.rotation.y, rig_Device.rotation.z * 0.8f);
        //else 
            rig_Character.rotation = rig_Device.rotation * Quaternion.Euler(offset_rotation);
    }
}


public class Character_IK_Mover : MonoBehaviour
{
    
    private Vector3 offset_head_body;
    private float turnSmoothness = 0.5f;
    public Transform headConstraint;

    public VRMap head;
    public VRMap device_Hand_L;
    public VRMap device_Hand_R;

    // Start is called before the first frame update
    void Start()
    {
        offset_head_body = transform.position - headConstraint.position; // 캐릭터 위치와 머리(헤드셋) 거리차를 계산해 놓음.
        
        
    }

    void FixedUpdate()
    {
               
        /* 공부 필요한 코드 */
        transform.position = headConstraint.position + offset_head_body; // 머리 이동한 대로 이동하게 해줌.
        //transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized,0.3f*Time.deltaTime);

        //transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized,3f*Time.deltaTime);
        //transform.forward = InputController_XR.instance.GetCameraTransform().forward;

        transform.forward = Vector3.ProjectOnPlane(InputController_XR.instance.GetCameraTransform().forward, Vector3.up).normalized;

        head.Map(VRMap.mode.head);
        device_Hand_L.Map(VRMap.mode.hand);
        device_Hand_R.Map(VRMap.mode.hand);

    }
}
