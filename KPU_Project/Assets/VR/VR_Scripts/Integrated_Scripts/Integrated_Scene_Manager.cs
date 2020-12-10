using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fove.Unity;

public class Integrated_Scene_Manager : MonoBehaviour
{
    public float Times = 0f;
    private static Integrated_Scene_Manager instance = null;
    public static Integrated_Scene_Manager Instance
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
        Times = 0f;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Main"))
        {
            Times += Time.deltaTime;
            if (Times > 1)
                SceneManager.LoadScene("Main_Scene");
        }
        if (other.name.Equals("GameExit"))
        {
            Times += Time.deltaTime;
            if (Times > 1)
                Application.Quit();
        }
        if (other.name.Equals("Result"))
        {
            Times += Time.deltaTime;
            if (Times > 1)
            {
                SceneManager.LoadScene("Record_Scene");
            }
        }
    }

    public void Result()
    {
        Debug.Log("눌림");
        SceneManager.LoadScene("Record_Scene");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
