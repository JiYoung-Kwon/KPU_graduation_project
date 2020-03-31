using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;

public class AI_Car_Wheel_Control : MonoBehaviour
{

    [SerializeField] private Transform LeftWheel;
    [SerializeField] private Transform LeftWheel1;
    [SerializeField] private Transform RightWheel;
    [SerializeField] private Transform RightWheel1;
    public float Times = 0;
    public float Speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Times += Time.deltaTime;
        RightWheel.localRotation = Quaternion.Euler(100 * Times * Speed, 0, 0);
        RightWheel1.localRotation = Quaternion.Euler(100 * Times * Speed, 0, 0);
        LeftWheel.localRotation = Quaternion.Euler(100 * Times * Speed, 0, 0);
        LeftWheel1.localRotation = Quaternion.Euler(100 * Times * Speed, 0, 0);
    }
}
