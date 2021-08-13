using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouch_Outer : MonoBehaviour
{
    private Pouch_Equipment p_e;
    private bool touched = false;

    // Start is called before the first frame update
    void Start()
    {
        p_e = GetComponentInChildren<Pouch_Equipment>();           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && !touched)
        {
            Debug.Log("Hand IN");
            touched = true;
            p_e.socketActive = true;
            p_e.CreateItem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            Debug.Log("Hand OUT");
            touched = false;
            p_e.DestoryItem();
            p_e.socketActive = false;
        }

    }


}
