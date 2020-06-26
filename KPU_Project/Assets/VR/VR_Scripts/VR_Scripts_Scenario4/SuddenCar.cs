using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenCar : MonoBehaviour
{
    public GameObject Target;
    public GameObject Car;

    //private Animator anim;
    public bool CarGo = false;
    public bool IsScenario4 = false;
    private static SuddenCar suddencar;
    public static SuddenCar SUDDENCAR
    {
        get { return suddencar; }
    }

    private void Awake()
    {
        suddencar = GetComponent<SuddenCar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CarGo = true;
            IsScenario4 = true;
            StartCoroutine(Go());
        }
    }
    IEnumerator Go()
    {
        while (Car.transform.position != Target.transform.position)
        {
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, Target.transform.position, 0.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
