using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log("Select Enter");
        if(interactor is XRDirectInteractor) Climber.hand_climbing = interactor.GetComponent<XRController>();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("Select Exit");
        if(Climber.hand_climbing && Climber.hand_climbing.name == interactor.name)
        {
            Climber.hand_climbing = null;
        }

        base.OnSelectExited(interactor);
    }






}
