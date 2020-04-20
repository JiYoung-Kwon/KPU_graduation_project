using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class user_data
{
    [SerializeField] private string ID = string.Empty;
    [SerializeField] private string PW = string.Empty;
    [SerializeField] private List<int> results = new List<int>();
    [SerializeField] private int Is_Danger = 0;

    public string Get_ID
    {
        get { return ID; }
    }
    public string Get_PW
    {
        get { return PW; }
    }
    public List<int> Get_Result
    {
        get { return results; }
    }
    public int Get_Is_Danger
    {
        get { return Is_Danger; }
    }

    public user_data() { }

    public user_data(string ID,string PW,string name,int scenario1, int scenario2, int scenario3, int scenario4,int danger)
    {
        this.ID = ID;
        this.PW = PW;
        results.Add(scenario1);
        results.Add(scenario2);
        results.Add(scenario3);
        results.Add(scenario4);
        Is_Danger = danger;
    }
}
