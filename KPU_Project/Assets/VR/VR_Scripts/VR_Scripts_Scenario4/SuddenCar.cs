using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenCar : MonoBehaviour
{
    public GameObject Target;
    public GameObject Car;

    //private Animator anim;
    public bool CarGo = false;
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
            StartCoroutine(Go());
        }
    }
    IEnumerator Go()
    {
        //anim = start.GetComponent<Animator>();
        //anim.SetBool("Run", true);
        while (Car.transform.position != Target.transform.position)
        {
            //if (anim.GetBool("Dead"))
            //{
            //    break;
            //}
            Car.transform.position = Vector3.MoveTowards(Car.transform.position, Target.transform.position, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        //anim.SetBool("Run", false);
    }
}
