using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenCar : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    private Animator anim;
    public bool PersonRun = false;
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
            PersonRun = true;
            StartCoroutine(go());
        }
    }
    IEnumerator go()
    {
        anim = start.GetComponent<Animator>();
        anim.SetBool("Run", true);
        while (start.transform.position != end.transform.position)
        {
            if (anim.GetBool("Dead"))
            {
                break;
            }
            start.transform.position = Vector3.MoveTowards(start.transform.position, end.transform.position, 0.5f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetBool("Run", false);
    }
}
