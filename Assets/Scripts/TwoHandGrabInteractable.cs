using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class TwoHandGrabInteractable : XRGrabInteractable
{

    public XRSimpleInteractable grabPoint_second;
    private XRBaseInteractor second_hand;
    public enum TwoHandRotationType {None, First, Second};
    public TwoHandRotationType hrt;
    
    //���� ������ �κ�. �� ����.
    public bool snapToSecondHand = true;
    private Quaternion initialRotationOffset;

    private void Start()
    {
        grabPoint_second.selectEntered.AddListener(OnSecondPointGrabEnter);
        grabPoint_second.selectExited.AddListener(OnSecondPointGrabExit);

        //grabPoint_second.onSelectEntered.AddListener(OnSecondPointGrabEnter);
        //grabPoint_second.onSelectExited.AddListener(OnSecondPointGrabExit);

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(second_hand && selectingInteractor)
        {

            
            if(snapToSecondHand)
                selectingInteractor.attachTransform.rotation = GetTwoRotation() * initialRotationOffset; // ���ؾȵǴºκ� �߰��Ѱ�
            else
                selectingInteractor.attachTransform.rotation = GetTwoRotation(); //���� �ڵ�


        }
        
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoRotation()
    {
        Quaternion tgtRotation;
        if (hrt == TwoHandRotationType.None)
            tgtRotation = Quaternion.LookRotation(second_hand.attachTransform.position - selectingInteractor.attachTransform.position);
        else if (hrt == TwoHandRotationType.First)
            tgtRotation = Quaternion.LookRotation(second_hand.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        else
            tgtRotation = Quaternion.LookRotation(second_hand.attachTransform.position - selectingInteractor.attachTransform.position, second_hand.attachTransform.up);
        return tgtRotation;
    }

    public void OnSecondPointGrabEnter(SelectEnterEventArgs args)
    {
        Debug.Log("Second Grabbed");
        second_hand = args.interactor;
        
        // ���ؾȵǴºκ� �߰��Ѱ�
        initialRotationOffset = Quaternion.Inverse(GetTwoRotation()) * selectingInteractor.attachTransform.rotation;
    }

    public void OnSecondPointGrabExit(SelectExitEventArgs args)
    {              
        Debug.Log("Second Grabbed Released");
        second_hand = null;               
    }


    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {

        if (second_hand && second_hand.CompareTag(interactor.tag))
        {
            Debug.Log("XXXXX");
            return false;

        }

        bool flag_already_selected = selectingInteractor && !selectingInteractor.Equals(interactor);
        
        return base.IsSelectableBy(interactor) && !flag_already_selected;

    }
}
