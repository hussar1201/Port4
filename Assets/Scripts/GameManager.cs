using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Target[] arr_target;

    public static GameManager instance
    {
        get
        {
            if (m_instance==null) m_instance = FindObjectOfType<GameManager>();
            return m_instance;
        }
        private set { }
    }
    private static GameManager m_instance; 


    private void Awake()
    {
        if(instance!=this)
        {
            Destroy(gameObject);
        }
    }


    public void SetTargets()
    {
        for(int i = 0;i< arr_target.Length;i++)
        {
            arr_target[i].SetUpTarget();
        }

    }




}
