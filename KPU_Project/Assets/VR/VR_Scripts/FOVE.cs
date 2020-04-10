using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove.Unity;
using UnityEngine.SceneManagement;

public class FOVE : MonoBehaviour
{
    [SerializeField] public GameObject red_Signal;
    [SerializeField] public GameObject StopCar_Red;
    [SerializeField] public bool IsEvent = true;
    [SerializeField] public bool IsSee = true;

    [SerializeField] public float Times = 0f;
    [SerializeField] public float EyesTime = 0f;
    [SerializeField] public float BrakeTime = 0f;
   

    public enum EYE_enum
    {
        Left_EYE, Right_EYE
    }
    public EYE_enum instanceEye;   
    private static FoveInterface foveInterfaces;
    
    public static FoveInterface FoveInterface
    {
        get
        {
            if (foveInterfaces == null)
            {
                foveInterfaces = FindObjectOfType<FoveInterface>();
            }
            return foveInterfaces;
        }
    }


    private static FOVE fv;
    public static FOVE Fove
    {
        get { return fv; }
    }

    private void Awake()
    {
        fv = GetComponent<FOVE>();
    }
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
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

    void Check_Scenario1()
    {
        var rays = FoveInterface.GetGazeRays();
        Ray r = instanceEye == EYE_enum.Left_EYE ? rays.left : rays.right;
        RaycastHit hit;

        if (red_Signal.active)
        {
            Times += Time.deltaTime;
            if (EyesTime ==0 && IsSee && Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 9))
            {
                EyesTime = Times;
                IsSee = false;
            }

            if (BrakeTime == 0 && Input.GetKeyDown("space")&& IsSee == false)
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario1", EyesTime, BrakeTime);
                UI_Manager.Instance.ViewResult();
            }
        }
    }

    void Check_Scenario2()
    {
        var rays = FoveInterface.GetGazeRays();
        Ray r = instanceEye == EYE_enum.Left_EYE ? rays.left : rays.right;
        RaycastHit hit;
        if (StopCar_Red.active)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee && Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 10))
            {
                EyesTime = Times;
                IsSee = false;
            }

            if (BrakeTime == 0 && Input.GetKeyDown("space") && IsSee == false)
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
        var rays = FoveInterface.GetGazeRays();
        Ray r = instanceEye == EYE_enum.Left_EYE ? rays.left : rays.right;
        RaycastHit hit;
        
        if (VR_CarStop.INTERRUPTIONCAR.CarStop)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee && Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 10))
            {
                EyesTime = Times;
                IsSee = false;
            }

            if (Input.GetKeyDown("space"))
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
        var rays = FoveInterface.GetGazeRays();
        Ray r = instanceEye == EYE_enum.Left_EYE ? rays.left : rays.right;
        RaycastHit hit;
        if (SuddenCar.SUDDENCAR.CarGo)
        {
            Times += Time.deltaTime;
            if (EyesTime == 0 && IsSee && Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 12))
            {
                EyesTime = Times;
                IsSee = false;
            }

            if (BrakeTime == 0 && Input.GetKeyDown("space") && IsSee == false)
            {
                BrakeTime = Times;
                Debug.Log(BrakeTime);
                Manager.VR_Manager.Instance.Add_VR_Data("VR_Scenario4", EyesTime, BrakeTime);
                UI_Manager.Instance.ViewResult();
            }
        }
    }
    public void InitEvent()
    {
        IsSee = false;
        IsEvent = false;
        EyesTime = 0f;
        BrakeTime = 0f;
    }
}
