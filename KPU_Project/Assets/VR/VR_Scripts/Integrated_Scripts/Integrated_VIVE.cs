using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Integrated_VIVE : MonoBehaviour
{
    #region singleton
    private static Integrated_VIVE instance = null;
    public static Integrated_VIVE Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }
    #endregion

    [SerializeField] public GameObject red_Signal;
    [SerializeField] public GameObject StopCar_Red;
    [SerializeField] public GameObject Interrupt_Car;
    [SerializeField] public GameObject PassCar;
    [SerializeField] public bool IsEvent = false;
    [SerializeField] public bool IsSee = false;
    [SerializeField] public bool OverTime = true;

    [SerializeField] public float Times = 0f;
    [SerializeField] public float EyesTime = 0f;
    [SerializeField] public float BrakeTime = 0f;

    public bool Scenario3Check = false;

    // Start is called before the first frame update
    void Start()
    {
        InitEvent();
        
    }

    // Update is called once per frame
    void Update()
    {
        Check_Scenario1();
        Check_Scenario2();
        if (Scenario3Check)
        {
            Check_Scenario3();
        }
        
        Debug.Log("t");
        Check_Scenario4();

        if (Integrated_What_Scenario.Instance.IsScenario1)
        {
            Integrated_TrafficLight.Instance.IsScenario1 = false;
            Times = 0;
            InitEvent();
        }
        else if (Tobii.EventTrigger.ET.IsScenario3)
        {
            Tobii.EventTrigger.ET.IsScenario3 = false;
            InitEvent();
        }
        else if (SuddenCar.SUDDENCAR.IsScenario4)
        {
            SuddenCar.SUDDENCAR.IsScenario4 = false;
            Times = 0;
            InitEvent();
        }
        
    }

    void Check_Scenario1()
    {
        if (Integrated_TrafficLight.Instance.GreenOff)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee)
            {
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUser.Instance.break_Input > 0)// Input.GetKeyDown("space"))//)
            {
                OverTime = false;
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                Debug.Log("VR_Scenario1"+ EyesTime + ","+ BrakeTime);
                Integrated_TrafficLight.Instance.GreenOff = false;
            }
        }
        if (Integrated_TrafficLight.Instance.GreenOff && Times > 3 && OverTime)
        {
            EyesTime = Times;
            BrakeTime = Times;
            OverTime = false;
            Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
            Debug.Log("VR_Scenario1" + EyesTime + "," + BrakeTime);
            Integrated_TrafficLight.Instance.GreenOff = false;
        }
    }

    void Check_Scenario2()
    {
        if (StopCar_Red.active)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee)
            {
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUser.Instance.break_Input > 0) //Input.GetKeyDown("space"))
            {
                OverTime = false;
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario2", EyesTime, BrakeTime);
                Debug.Log("VR_Scenario2" + EyesTime + "," + BrakeTime);
                StopCar_Red.active = false;
            }
        }
        else if (StopCar_Red.active && Times > 3 && OverTime)
        {
            OverTime = false;
            EyesTime = Times;
            BrakeTime = Times;
            OverTime = true;
            Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario2", EyesTime, EyesTime);
            Debug.Log("VR_Scenario2" + EyesTime + "," + BrakeTime);
            StopCar_Red.active = false;
        }
    }
    void Check_Scenario3()
    {
        if (VR_NaviThree.NAVI.CarStop)
        {

            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee)
            {
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUser.Instance.break_Input > 0) //Input.GetKeyDown("space"))
            {

                OverTime = false;
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario3", EyesTime, EyesTime);
                Debug.Log("VR_Scenario3" + EyesTime + "," + BrakeTime);
                VR_NaviThree.NAVI.CarStop = false;
            }
        }
        else if (VR_NaviThree.NAVI.CarStop && Times > 3 && OverTime)
        {

            EyesTime = Times;
            BrakeTime = Times;
            OverTime = false;
            Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario3", EyesTime, EyesTime);
            Debug.Log("VR_Scenario3" + EyesTime + "," + BrakeTime);
            VR_NaviThree.NAVI.CarStop = false;

        }        
    }
    void Check_Scenario4()
    {
        if (SuddenCar.SUDDENCAR.CarGo)
        {
            
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee)
            {
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUser.Instance.break_Input > 0) //Input.GetKeyDown("space"))
            {
                OverTime = false;
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario4", EyesTime, EyesTime);
                Debug.Log("VR_Scenario4" + EyesTime + "," + BrakeTime);
                UI_Manager.Instance.ViewResult();
                Manager.VR_Manager.Instance.Is_Danger();
                SuddenCar.SUDDENCAR.CarGo = false;
            }
        }
        else if (SuddenCar.SUDDENCAR.CarGo && Times > 3 && OverTime)
        {
            EyesTime = Times;
            BrakeTime = Times;
            OverTime = false;
            Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario4", EyesTime, EyesTime);
            Debug.Log("VR_Scenario4" + EyesTime + "," + BrakeTime);
            SuddenCar.SUDDENCAR.CarGo = false;
            UI_Manager.Instance.ViewResult();
            Manager.VR_Manager.Instance.Is_Danger();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name.Equals(red_Signal.name))
        {
            Debug.Log(other.name);
            IsSee = true;
        }
        if (other.name.Equals(StopCar_Red.name))
        {
            Debug.Log(other.name);
            IsSee = true;
        }
        if (other.name.Equals(Interrupt_Car.name))
        {
            Debug.Log(other.name);
            IsSee = true;
        }
        if (other.name.Equals(PassCar.name))
        {
            Debug.Log(other.name);
            IsSee = true;
        }
    }


    // 각 시나리오 전환이벤트 받을 때 마다 초기화 기켜주기
    public void InitEvent()
    {
        OverTime = true;
        IsSee = false;
        IsEvent = false;
        EyesTime = 0f;
        BrakeTime = 0f;
    }
}
