using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouch_for_Item_Spawn : MonoBehaviour
{

    public GameObject prefab_item;
    private GameObject item_created;
    private bool flag_touched = false;
    private bool flag_created = false;
    private bool hand_L = false;

    private void OnTriggerEnter(Collider other)
    {
        item_created = null;
        if (other.gameObject.tag.Contains("Hand"))
        {
            if (other.gameObject.tag.Contains("_L")) hand_L = true;
            else hand_L = false;
            flag_touched = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log(InputController_XR.instance.grip_L);
        if (!other.gameObject.tag.Contains("Hand")) return;

            if (flag_touched && !flag_created)
        {
            if (InputController_XR.instance.grip_R > 0.99f || InputController_XR.instance.grip_L > 0.99f)
            {
                if (item_created == null) { 
                              item_created = Instantiate(prefab_item, transform.position, transform.rotation);
                flag_created = true;
                }
            }

        }

    }


    private void OnTriggerExit(Collider other)
    {
        item_created = null;
            flag_touched = false;
            flag_created = false;        
    }


}
