using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenStopCar : MonoBehaviour
{
    private static SuddenStopCar suddenstopcar;
    public static SuddenStopCar SUDDENSTOPCAR
    {
        get { return suddenstopcar; }
    }

    private void Awake()
    {
        suddenstopcar = GetComponent<SuddenStopCar>();
    }

    public GameObject FrontCar;
    public GameObject Light;
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    public bool CarStop = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Light_On();
        }
    }
    public void Light_On()
    {
        CarStop = true;
        Light.gameObject.SetActive(true);
        Light1.gameObject.SetActive(true);
        Light2.gameObject.SetActive(true);
        Light3.gameObject.SetActive(true);

        // 지정차량 부모자식 해제하여 차 멈추게 하기
        // Make_Rigidbody.MAKE_RIGIDBODY.Rigidbody();
        // FrontCar.transform.parent = FrontCar.transform.parent.parent;
    }
}
