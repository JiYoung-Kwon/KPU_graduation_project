using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrated_What_Scenario : MonoBehaviour
{
    private static Integrated_What_Scenario instance = null;
    public static Integrated_What_Scenario Instance
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("신호등반응 On");
            Integrated_TrafficLight.Instance.Change_Traffic_Light_Start = true;
        }
    }
}
