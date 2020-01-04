using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TOBII
{
    [SerializeField] private string stage_name;
    [SerializeField] private float eyes_time;
    [SerializeField] private float brake_time;
    [SerializeField] private Is_Danger is_danger;

    public TOBII() { }
    public TOBII(string _stage_name, float _eyes_time, float _brake_time, Is_Danger _is_danger)
    {
        stage_name = _stage_name;
        eyes_time = _eyes_time;
        brake_time = _brake_time;
        is_danger = _is_danger;
    }
}
