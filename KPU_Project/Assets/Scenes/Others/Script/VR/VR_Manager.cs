using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class VR_Manager : MonoBehaviour
    {
        [SerializeField] private List<VR> L_VR = new List<VR>();

        bool scenario1Danger = false;   // true : 통과, false : 위험군
        bool scenario2Danger = false;   // true : 통과, false : 위험군
        bool scenario3Danger = false;   // true : 통과, false : 위험군
        bool scenario4Danger = false;   // true : 통과, false : 위험군
        bool TotalDanger = false;

        #region singleton
        private static VR_Manager instance = null;
        public static VR_Manager Instance
        {
            get { return instance; }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion



        /// <summary>
        /// 스테이지 클리어 시, 넣어주면 됨.
        /// </summary>
        /// <param name="_stage_name"> 스테이지 이름 </param>
        /// <param name="_eyes_time"> 시각 관련 초</param>
        /// <param name="_brake_time"> 브레이크 관련 초 </param>
        public void Add_VR_Data(string _stage_name, float _eyes_time, float _brake_time) //데이터 스크립트에 저장
        {
            L_VR.Add(new VR(_stage_name, _eyes_time, _brake_time));
        }

        public void Is_Danger() //위험군 여부 확인
        {
            if (check_ToalDanger())
            {
                Debug.Log("안전해");
                // 위험하지 않음... 처리할꺼 넣어주면 됨.
            }
            else
            {
                Debug.Log("위험해");
                // 위험군...
            }
        }

        #region 위험군 판별
        /// <summary>
        /// 위험군 판별
        /// </summary>
        /// <returns> 
        /// true : 통과
        /// false : 위험군
        /// </returns>
        public bool check_ToalDanger()
        {
            if (scenario1Danger && scenario2Danger && scenario3Danger && scenario4Danger)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void check_Danger()
        {
            scenario1Danger = scenario1(0);
            scenario2Danger = scenario2(1);
            scenario3Danger = scenario3(2);
            scenario4Danger = scenario4(3);
            TotalDanger = check_ToalDanger();
        }

        /// <summary>
        /// 시나리오 1 신호등 체크
        /// </summary>
        /// <param name="index"> 0 임</param>
        /// <returns>
        /// True -> 통과
        /// false -> 위험군
        /// </returns>
        private bool scenario1(int index)
        {
            if (L_VR[index].Eyes_Time < 2.1f)
            {
                if (L_VR[index].Brake_Time < 2.1f)
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario1(1)");
                    return true;
                }
                else
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario1(0)");
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 시나리오2
        /// 앞 차량 급정지
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// True -> 통과
        /// false -> 위험군
        /// </returns>
        private bool scenario2(int index)
        {
            if (L_VR[index].Eyes_Time < 2.0f)
            {
                if (L_VR[index].Brake_Time < 2.0f)
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario2(1)");
                    return true;
                }
                    
                else
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario2(0)");
                    return false;
                }
                    
            }
            return false;
        }
        /// <summary>
        /// 시나리오3
        /// 다른차량 끼어들기
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// True -> 통과
        /// false -> 위험군
        /// </returns>
        private bool scenario3(int index)
        {
            if (L_VR[index].Eyes_Time < 1.9f)
            {
                if (L_VR[index].Brake_Time < 1.9f)
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario3(1)");
                    return true;
                }
                    
                else
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario3(0)");
                    return false;
                }
                    
            }
            return false;
        }
        /// <summary>
        /// 시나리오4
        /// 교차로 내 반응속도
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// True -> 통과
        /// false -> 위험군
        /// </returns>
        private bool scenario4(int index)
        {
            if (L_VR[index].Eyes_Time < 1.6f)
            {
                if (L_VR[index].Brake_Time < 1.6f)
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario4(1)");
                    return true;
                }

                else
                {
                    Manager.DB_sqlite_Manager.Instance.DB_Query("INSERT INTO Scenario4(0)");
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}