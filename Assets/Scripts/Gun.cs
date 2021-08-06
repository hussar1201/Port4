using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Grabbed()
    {
        Debug.Log("is grabbed");
    }

    public void UnGrabbed()
    {
        Debug.Log("is ungrabbed");
    }


    public void Fire()
    {
        Debug.Log("Firing");
    }

}
