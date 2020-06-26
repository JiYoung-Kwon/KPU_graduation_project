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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // 각 시나리오 전환이벤트 받을 때 마다 초기화 기켜주기
    public void InitEvent()
    {
        IsSee = false;
        IsEvent = false;
        EyesTime = 0f;
        BrakeTime = 0f;
    }
}
