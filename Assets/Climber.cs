using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Climber : MonoBehaviour
{
    private CharacterController cc;
    public static XRController hand_climbing;
    private PlayerMover pm;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        pm = GetComponent<PlayerMover>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
        if(hand_climbing)
        {
            pm.enabled = false;
            Climb();
        }
        else
        {
            pm.enabled = true;
        }


    }

    void Climb()
    {
        InputDevices.GetDeviceAtXRNode(hand_climbing.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        cc.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }

}
