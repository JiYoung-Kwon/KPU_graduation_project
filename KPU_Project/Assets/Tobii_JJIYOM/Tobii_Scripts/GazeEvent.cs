using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tobii
{
    public class GazeEvent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //스페이스 키 누르면
            if (Input.GetKeyDown("space") && GameManager.GM.resultTime == 0)
            {
                Debug.Log(GameManager.GM.time);
                GameManager.GM.resultTime = GameManager.GM.time;
                GameManager.GM.ViewResult();
                Manager.TOBII_Manager.Instance.Is_Danger();
            }
        }

        private void OnTriggerEnter(Collider other) //최초 시선 측정
        {
            //공이 나타나면 시선 측정하는데
            if (other.name == "Red_Light" && GameManager.GM.see_time == 0) //see_time자리에 Tobii_Manager Param
            {
                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario1", Tobii_TrafficLight.TT.times, 0);
                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario1", Tobii_TrafficLight.TT.times, 0);
                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario1", Tobii_TrafficLight.TT.times, 0);
                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario1", Tobii_TrafficLight.TT.times, 0);
                GameManager.GM.see_time = Tobii_TrafficLight.TT.times; //see_time자리에 Tobii_Manager Param
                Debug.Log("공맞음 " + GameManager.GM.see_time);
            }
        }
    }
}