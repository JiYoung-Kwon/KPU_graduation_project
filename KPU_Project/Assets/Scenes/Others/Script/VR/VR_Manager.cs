using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class VR_Manager : MonoBehaviour
    {
        [SerializeField] private List<VR> L_VR = new List<VR>();

        bool scenario1Danger;   // true : 통과, false : 위험군
        bool scenario2Danger;   // true : 통과, false : 위험군
        bool scenario3Danger;   // true : 통과, false : 위험군
        bool scenario4Danger;   // true : 통과, false : 위험군
        bool TotalDanger;

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
        /// <param name="_is_danger"></param>
        public void Add_VR_Data(string _stage_name, float _eyes_time, float _brake_time) //데이터 스크립트에 저장
        {
            L_VR.Add(new VR(_stage_name, _eyes_time, _brake_time));
        }

        public void Is_Danger() //위험군 여부 확인
        {
            scenario1(0);
            scenario2(1);
            scenario3(2);
            scenario4(3);

            if (scenario1Danger && scenario2Danger && scenario3Danger && scenario4Danger)
            {
                TotalDanger = true;
                Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Is_Danger = 1 Where ID = " + save_user_data.Instance.Save_ID);
                Debug.Log("안전해");
                // 위험하지 않음... 처리할꺼 넣어주면 됨.
            }
            else
            {
                TotalDanger = false;
                Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Is_Danger = 0 Where ID = " + save_user_data.Instance.Save_ID);
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

        //public bool check_Danger()
        //{
        //    scenario1(0);
        //    scenario2(1);
        //    scenario3(2);
        //    scenario4(3);
        //    if(scenario1Danger && scenario2Danger && scenario3Danger && scenario4Danger)
        //    {
        //        TotalDanger = true;
        //        return true;
        //    }
        //    return false;
        //}


        /// <summary>
        /// 시나리오 1 신호등 체크
        /// </summary>
        /// <param name="index"> 0 임</param>
        /// <returns>
        /// True -> 통과
        /// false -> 위험군
        /// </returns>
        public bool scenario1(int index)
        {
            if (L_VR[index].Eyes_Time < 2.1f)
            {
                if (L_VR[index].Brake_Time < 2.1f)
                {
                    scenario1Danger = true;
                    Debug.Log("시나리오1 통과");
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario1 = 1 Where ID = " + save_user_data.Instance.Save_ID);
                    return true;
                }
                else
                {
                    scenario1Danger = false;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario1 = 0 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오1 불통");
                    return false;
                }
            }
            else
                scenario1Danger = false;
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
        public bool scenario2(int index)
        {
            if (L_VR[index].Eyes_Time < 2.0f)
            {
                if (L_VR[index].Brake_Time < 2.0f)
                {
                    scenario2Danger = true;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario2 = 1 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오2 통과");
                    return true;
                }
                    
                else
                {
                    scenario2Danger = false;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario2 = 0 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오2 불통");
                    return false;
                }
                    
            }
            else
                scenario2Danger = false;
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
        public bool scenario3(int index)
        {
            if (L_VR[index].Eyes_Time < 1.9f)
            {
                if (L_VR[index].Brake_Time < 1.9f)
                {
                    scenario3Danger = true;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario3 = 1 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오3 통과");
                    return true;
                }
                    
                else
                {
                    scenario3Danger = false;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario3 = 0 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오2 불통");
                    return false;
                }
                    
            }
            else
                scenario3Danger = false;
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
        public bool scenario4(int index)
        {
            if (L_VR[index].Eyes_Time < 1.6f)
            {
                if (L_VR[index].Brake_Time < 1.6f)
                {
                    scenario4Danger = true;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario4 = 1 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오4 통과");
                    return true;
                }

                else
                {
                    scenario4Danger = false;
                    Manager.DB_sqlite_Manager.Instance.DB_Query("UPDATE Account SET Scenario4 = 0 Where ID = " + save_user_data.Instance.Save_ID);
                    Debug.Log("시나리오4 불통");
                    return false;
                }
            }
            else
                scenario4Danger = false;
            return false;
        }
        #endregion
    }
}