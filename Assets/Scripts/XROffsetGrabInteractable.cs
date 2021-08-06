using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    private void Start()
    {
        if(!attachTransform)
        {
            GameObject grab = new GameObject("Grab_Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {

        if(interactor is XRRayInteractor)
        { 
        attachTransform.position = interactor.transform.position;
        attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }


        base.OnSelectEntered(interactor);



    }

    


}
