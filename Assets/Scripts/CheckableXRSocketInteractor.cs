using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class CheckableXRSocketInteractor : XRSocketInteractor
{
    public string tag_tgt;
    private Gun gun;
    private Magazine mag;
    private bool flag_isCorrectObject = false;
    private bool flag_ejected = false;

    public SoundPlayer soundPlayer;

    protected override void Start()
    {
        gun = GetComponentInParent<Gun>();
        soundPlayer = GetComponentInParent<SoundPlayer>();
        base.Start();
    }

    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        if (!args.interactable.CompareTag(tag_tgt))
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
        mag = args.interactable.gameObject.GetComponent<Magazine>();
        gun.mag_attached = mag;
        flag_ejected = false;
        //soundPlayer.PlaySound(SoundPlayer.Part.mag, 0+Random.Range(0,1));
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
            if (!gun.isAmmoOnChamber && gun.isLoaded)
            {
                if (mag.ConsumeAmmo()) gun.isAmmoOnChamber = true;
                else gun.isAmmoOnChamber = false;
            }            
            //soundPlayer.PlaySound(SoundPlayer.Part.mag, 2 + Random.Range(0, 1));
            flag_isCorrectObject = false;
            gun.mag_attached = null;
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
        gun.mag_attached.Eject();
        yield return new WaitForSeconds(.5f);
        RemoveMag();

        socketActive = this.allowHover = this.allowSelect = true;

    }


}
