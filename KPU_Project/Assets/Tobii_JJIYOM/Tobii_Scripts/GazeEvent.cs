using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tobii
{
    //시선 후 -> 브레이크만 측정되게(그 전값 무시)
    public class GazeEvent : MonoBehaviour
    {
        public float EyesTime = 0f;
        public float BrakeTime = 0f;

        public bool IsSee = false;
        public bool IsEvent = false;

        #region singleton
        private static GazeEvent instance = null;
        public static GazeEvent Instance
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
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Tobii_Scenario1":
                    Check_Scenario1();
                    break;
                case "Tobii_Scenario2":
                    Check_Scenario2();
                    break;
                case "Tobii_Scenario3":
                    Check_Scenario3();
                    break;
                case "Tobii_Scenario4":
                    Check_Scenario4();
                    break;
            }
        }

        private void OnTriggerEnter(Collider other) //최초 시선 측정
        {
            Debug.Log(other.name);
            //공이 나타나면 시선 측정하는데
            //시나리오 1
            if (other.name == "Red_Light" && EyesTime == 0) //see_time자리에 Tobii_Manager Param
            {
                EyesTime = Tobii_TrafficLight.TT.times; //see_time자리에 Tobii_Manager Param
                IsSee = true;
                Debug.Log("공맞음 " + EyesTime);
            }
            //시나리오 2
            if (other.name == "FColliders" && EyesTime == 0 && IsEvent)
            {
                EyesTime = Tobii_Navation.NAVATION.times; //see_time자리에 Tobii_Manager Param
                IsSee = true;
                Debug.Log("차봤어 " + EyesTime);
            }
            //시나리오 3
            if (other.name == "BlueCar" && EyesTime == 0 && IsEvent)
            {
                EyesTime = Tobii_Navigation.NAVI.times;
                IsSee = true;
                Debug.Log("끼-차봤어" + EyesTime);
            }
            //시나리오 4
            if (other.name == "EnemyCar" && EyesTime == 0 && IsEvent)
            {
                EyesTime = Tobii_Navigation.NAVI.times;
                IsSee = true;
                Debug.Log("교-차봤어" + EyesTime);
            }
        }

        public void Check_Scenario1()
        {
            //스페이스 키 누르면 //이벤트 발생이후 //눈으로 본 후 //한번만 발생하게
            if (Input.GetKeyDown("space") && BrakeTime == 0 && IsEvent && IsSee)
            {
                BrakeTime = Tobii_TrafficLight.TT.times;
                Debug.Log("브레이크 반응 시간 : " + Tobii_TrafficLight.TT.times);

                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario1", EyesTime, BrakeTime);
                
                //시나리오4까지 미완성이므로 일단 값 넣어놓기              
                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario1", EyesTime, BrakeTime);

                UIManager.Instance.ViewResult();               
            }
        }

        public void Check_Scenario2()
        {
            if (Input.GetKeyDown("space") && BrakeTime == 0 && IsEvent && IsSee)
            {
                BrakeTime = Tobii_Navation.NAVATION.times;
                Debug.Log("브레이크 반응 시간 : " + Tobii_Navation.NAVATION.times);

                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario2", EyesTime, BrakeTime);
                UIManager.Instance.ViewResult();                
            }
        }

        public void Check_Scenario3()
        {
            if (Input.GetKeyDown("space") && BrakeTime == 0 && IsEvent && IsSee)
            {
                BrakeTime = Tobii_Navigation.NAVI.times;
                Debug.Log("브레이크 반응 시간 : " + Tobii_Navigation.NAVI.times);

                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario3", EyesTime, BrakeTime);
                UIManager.Instance.ViewResult();               
            }
        }

        public void Check_Scenario4()
        {
            if (Input.GetKeyDown("space") && BrakeTime == 0 && IsEvent && IsSee)
            {
                BrakeTime = Tobii_Navigation.NAVI.times;
                Debug.Log("브레이크 반응 시간 : " + Tobii_Navigation.NAVI.times);

                Manager.TOBII_Manager.Instance.Add_TOBII_Data("Tobii_Scenario4", EyesTime, BrakeTime);
                UIManager.Instance.ViewResult();
                Manager.TOBII_Manager.Instance.Is_Danger();
            }
        }


        public void InitEvent()
        {
            IsSee = false;
            IsEvent = false;
            EyesTime = 0f;
            BrakeTime = 0f;
        }
    }
}