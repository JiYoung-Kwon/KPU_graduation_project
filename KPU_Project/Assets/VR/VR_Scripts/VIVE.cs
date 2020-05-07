using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VIVE : MonoBehaviour
{
    #region singleton
    private static VIVE instance = null;
    public static VIVE Instance
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

    [SerializeField] public float Times = 0f;
    [SerializeField] public float EyesTime = 0f;
    [SerializeField] public float BrakeTime = 0f;

    // Start is called before the first frame update
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            //case "VR_VIVE_Test":
            //    Check_VR_Test();
            //    break;
            case "VR_Scenario1":
                Check_Scenario1();
                break;
            case "VR_Scenario2":
                Check_Scenario2();
                break;
            case "VR_Scenario3":
                Check_Scenario3();
                break;
            case "VR_Scenario4":
                Check_Scenario4();
                break;
        }
    }

    void Check_VR_Test()
    {
        if (red_Signal.active)
        {
            Debug.Log("불 들어옴");
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee)
            {

                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && Input.GetKeyDown("space"))//UnityStandardAssets.Vehicles.Car.CarUserControl.Instance.break_Input > 0) )
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                UI_Manager.Instance.ViewResult();
            }
        }
    }


    void Check_Scenario1()
    {
        if (red_Signal.active)
        {
            Debug.Log("불 들어옴");
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee )
            {
                
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && Input.GetKeyDown("space"))//UnityStandardAssets.Vehicles.Car.CarUserControl.Instance.break_Input > 0) )
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                UI_Manager.Instance.ViewResult();
            }
        }
    }

    void Check_Scenario2()
    {
        if (StopCar_Red.active)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee )
            {
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUserControl.Instance.break_Input > 0 )
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario2", EyesTime, BrakeTime);
                UI_Manager.Instance.ViewResult();
            }
        }
    }
    void Check_Scenario3()
    {
        if (VR_CarStop.INTERRUPTIONCAR.CarStop)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee)
            {
                EyesTime = Times;
                IsEvent = true;
            }

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUserControl.Instance.break_Input > 0)
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario3", EyesTime, BrakeTime);
                UI_Manager.Instance.ViewResult();
            }
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

            if (BrakeTime == 0 && IsEvent && UnityStandardAssets.Vehicles.Car.CarUserControl.Instance.break_Input > 0 )
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario4", EyesTime, BrakeTime);

                UI_Manager.Instance.ViewResult();
                Manager.VR_Manager.Instance.Is_Danger();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(red_Signal.name))
        {
            IsSee = true;
            Debug.Log(other.name);
        }
            
        else if (other.name.Equals(StopCar_Red.name))
            IsSee = true;
        else if (other.name.Equals(Interrupt_Car.name))
            IsSee = true;
        //else if (other.name.Equals(red_Signal.name))
        //    IsSee = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals(red_Signal.name))
            IsSee = false;
            
        else if (other.name.Equals(StopCar_Red.name))
            IsSee = false;
        else if (other.name.Equals(Interrupt_Car.name))
            IsSee = false;
        else if (other.name.Equals(Interrupt_Car.name))
            IsSee = false;
    }

    public void InitEvent()
    {
        IsSee = false;
        IsEvent = false;
        EyesTime = 0f;
        BrakeTime = 0f;
    }
}
