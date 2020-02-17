using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VR
{
    [SerializeField] private string stage_name = string.Empty;
    [SerializeField] private float eyes_time = 0.0f;
    [SerializeField] private float brake_time = 0.0f;

    #region 프로퍼티
    public string Stage_Name
    {
        get {return stage_name; }
    }
    public float Eyes_Time
    {
        get { return eyes_time; }
    }
    public float Brake_Time
    {
        get { return brake_time; }
    }
    #endregion

    public VR() { }
    public VR(string _stage_name,float _eyes_time, float _brake_time)
    {
        stage_name = _stage_name;
        eyes_time = _eyes_time;
        brake_time = _brake_time;
    }
}
