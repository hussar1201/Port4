using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InputController_XR : MonoBehaviour
{
    public XRRig xrRig;


    //입력 컨트롤러 관련 변수들
    public XRController Con_L_XR;
    public XRController Con_R_XR;
    private InputDevice Controller_L;
    private InputDevice Controller_R;

    //손 애니메이션 관련 변수들
    public GameObject prefab_hand_L;
    public GameObject prefab_hand_R;
    private Animator hand_L;
    private Animator hand_R;

    //텔레포트 이동 관련 변수
    //public GameObject teleporter_R;



    //입력갑 관련 변수들 -> 차후 클래스 하나 만들어서 관리할 것
    public float trigger_L
    {
        get;
        private set;
    }
    public float trigger_R
    {
        get;
        private set;
    }

    public bool trigger_L_bool
    {
        get;
        private set;
    }
    public bool trigger_R_bool
    {
        get;
        private set;
    }

    public float grip_L
    {
        get;
        private set;
    }
    public float grip_R
    {
        get;
        private set;
    }
    public Vector2 axis_XY_L
    {
        get;
        private set;
    }
    public Vector2 axis_XY_R
    {
        get;
        private set;
    }
    public bool Btn_A
    {
        get;
        private set;
    }
    public bool Btn_B
    {
        get;
        private set;
    }


    private static InputController_XR m_instance;

    public static InputController_XR instance
    {
        get {
            if (m_instance == null) m_instance = FindObjectOfType<InputController_XR>();
            return m_instance;
        }         
    }

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
            return;
        }
        axis_XY_L = new Vector2(0f, 0f);
        axis_XY_R = new Vector2(0f, 0f);
    }


    // Start is called before the first frame update
    void Start()
    {               
        Controller_L = Con_L_XR.inputDevice;
        Controller_R = Con_R_XR.inputDevice;
        hand_L = Instantiate(prefab_hand_L, Con_L_XR.transform).GetComponent<Animator>();
        hand_R = Instantiate(prefab_hand_R, Con_R_XR.transform).GetComponent<Animator>();       
    }


    public Transform GetCameraTransform() 
    {
        return xrRig.cameraGameObject.transform;
    }



    // Update is called once per frame
    void Update()
    {
        
        //왼쪽 컨트롤러 입력
        if (Controller_L.TryGetFeatureValue(CommonUsages.trigger, out float trigger_L) && trigger_L >0.1f)
        {                       
            hand_L.SetFloat("Trigger", trigger_L);
            this.trigger_L = trigger_L;
        }
        if (Controller_L.TryGetFeatureValue(CommonUsages.grip, out float grip_L) && grip_L > 0.1f)
        {
            hand_L.SetFloat("Grip", grip_L);
            this.grip_L = grip_L;
        }

        //우측 컨트롤러 입력
        if (Controller_R.TryGetFeatureValue(CommonUsages.trigger, out float trigger_R) && trigger_R > 0.1f)
        {
            hand_R.SetFloat("Trigger", trigger_R);
            
            this.trigger_R = trigger_R;
        }

        if (Controller_R.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger_R_bool))
        {
            this.trigger_R_bool = trigger_R_bool;
        }

            if (Controller_R.TryGetFeatureValue(CommonUsages.grip, out float grip_R) && grip_R > 0.1f)
        {
            hand_R.SetFloat("Grip", grip_R);
            
            this.grip_R = grip_R;
        }

        if (Controller_R.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axis_XY_R))
        {
            
            this.axis_XY_R = axis_XY_R;
        }
        if(Controller_R.TryGetFeatureValue(CommonUsages.primaryButton,out bool Btn_A))
        {
            this.Btn_A = Btn_A;
            
        }
        if (Controller_R.TryGetFeatureValue(CommonUsages.secondaryButton, out bool Btn_B))
        {
            this.Btn_B = Btn_B;
            
        }



        /*
        //텔레포트 관련 코드
        if(Controller_R.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value) && value.y>0.5f)
        {
            teleporter_R.SetActive(true);
        }
        else
        {
            teleporter_R.SetActive(false);
        }
        */




    }
}
