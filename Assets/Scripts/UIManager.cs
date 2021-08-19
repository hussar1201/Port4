using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject canvas_wrist_GPS;
    private Text[] arr_text_wrist_GPS;

    public static UIManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<UIManager>();
            return m_instance;
        }
        private set { }
    }
    private static UIManager m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }       
    }

    // Start is called before the first frame update
    void Start()
    {
        arr_text_wrist_GPS = canvas_wrist_GPS.gameObject.GetComponentsInChildren<Text>();
        Debug.Log(arr_text_wrist_GPS.Length);
    }

    public void ChangeTextState(string msg)
    {
        arr_text_wrist_GPS[2].text = msg;
    }



}
