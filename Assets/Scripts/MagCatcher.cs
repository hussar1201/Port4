using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class MagCatcher : XRSocketInteractor
{
    public string tag_AvailableMag;
    private Mag mag;

    private FireArm fireArm;
    
    private bool flag_isCorrectObject = false;
    private bool flag_ejected = false;
    public bool flag_new_mag_inserted;


    public SoundPlayer soundPlayer;

    protected override void Start()
    {
        fireArm = GetComponentInParent<FireArm>();
        soundPlayer = GetComponentInParent<SoundPlayer>();
        base.Start();
    }

    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        if (args.interactable.CompareTag(tag_AvailableMag)==false)
        {
            this.allowSelect = false;
            flag_isCorrectObject = false;
        }
        else flag_isCorrectObject = true;
        base.OnHoverEntering(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        this.allowSelect = true;
        flag_isCorrectObject = false;
        base.OnHoverExited(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // interactor = 소켓, interactable = 소켓에 들어온 오브젝트
        mag = args.interactable.gameObject.GetComponent<Mag>();      
        fireArm.mag_attached = mag;
        flag_ejected = false;
        if(mag.flag_IsAmmoLeft) flag_new_mag_inserted = true;
        soundPlayer.PlayOneShot(SoundPlayer.Part.mag, 0+Random.Range(0,2));
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (!flag_ejected) RemoveMag();
        base.OnSelectExited(args);
    }


    public void RemoveMag()
    {
        if (mag != null)
        {                       
            if (!fireArm.chamber.isAmmoOnChamber && fireArm.isLoaded) mag.SendAmmoToChamber();           
            flag_new_mag_inserted = false;
            soundPlayer.PlayOneShot(SoundPlayer.Part.mag, 2 + Random.Range(0, 2));
            flag_isCorrectObject = false;
            fireArm.mag_attached = null;
            mag = null;
        }
    }

    public void EjectMag()
    {
        socketActive = this.allowHover = this.allowSelect = false;
        flag_ejected = true;
        StartCoroutine(Ejecting());
    }

    IEnumerator Ejecting()
    {
        fireArm.mag_attached.Eject();
        yield return new WaitForSeconds(.5f);
        RemoveMag();
        socketActive = this.allowHover = this.allowSelect = true;
    }


}
