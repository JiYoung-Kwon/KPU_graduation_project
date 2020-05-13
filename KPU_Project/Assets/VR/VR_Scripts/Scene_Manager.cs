using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fove.Unity;

public class Scene_Manager : MonoBehaviour
{
    public float Times = 0f;
    private static Scene_Manager instance = null;
    public static Scene_Manager Instance
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

    //public enum EYE_enum
    //{
    //    Left_EYE, Right_EYE
    //}
    //public EYE_enum instanceEye;
    //private static FoveInterface foveInterfaces;

    //public static FoveInterface FoveInterface
    //{
    //    get
    //    {
    //        if (foveInterfaces == null)
    //        {
    //            foveInterfaces = FindObjectOfType<FoveInterface>();
    //        }
    //        return foveInterfaces;
    //    }
    //}

    void Start()
    {
        Times = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //var rays = FoveInterface.GetGazeRays();
        //Ray r = instanceEye == EYE_enum.Left_EYE ? rays.left : rays.right;
        //RaycastHit hit;
        //if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 13))
        //{
        //    Times += Time.deltaTime;
        //    if (Times > 1)
        //        NextScenario();
            
        //}
        //if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 14))
        //{
        //    Times += Time.deltaTime;
        //    if (Times > 1)
        //        SceneManager.LoadScene("Main_Scene");
        //}
        ////Times = 0f;
        //if (Physics.Raycast(r, out hit, Mathf.Infinity, 1 << 15))
        //{
        //    Times += Time.deltaTime;
        //    if (Times > 1)
        //        Application.Quit();
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Next"))
        {
            Times += Time.deltaTime;
            if (Times > 1)
                NextScenario();
        }
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name.Equals("Next"))
    //    {
    //        Times += Time.deltaTime;
    //        if (Times > 0.5)
    //            NextScenario();
    //    }
    //    if (other.name.Equals("Main"))
    //    {
    //        Times += Time.deltaTime;
    //        if (Times > 0.5)
    //            SceneManager.LoadScene("Main_Scene");
    //    }
    //    if (other.name.Equals("GameExit"))
    //    {
    //        Times += Time.deltaTime;
    //        if (Times > 0.5)
    //            Application.Quit();
    //    }
    //}

    public void NextScenario() //다음 시나리오 (OK)
    {
        //FOVE.Fove.InitEvent();
        VIVE.Instance.InitEvent();
        int curScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = curScene + 1;
        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1f;
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("VR_Scenario1");
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene("VR_Scenario2");
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene("VR_Scenario3");
    }
    public void LoadScene4()
    {
        SceneManager.LoadScene("VR_Scenario4");
    }

}
