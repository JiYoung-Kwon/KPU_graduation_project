using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_user_data : MonoBehaviour
{
    [SerializeField] private string user_id = string.Empty;

    public string Save_ID
    {
        set { user_id = value; }
        get { return user_id; }
    }

    private static save_user_data instance;
    public static save_user_data Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
