using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class TOBII_Manager : MonoBehaviour
    {
        [SerializeField] private List<TOBII> L_TOBII = new List<TOBII>();

        //0 : false ,1 : true
        public int scenario1Danger = 0;   // true : 통과, false : 위험군
        public int scenario2Danger = 0;   // true : 통과, false : 위험군
        public int scenario3Danger = 0;   // true : 통과, false : 위험군
        public int scenario4Danger = 0;   // true : 통과, false : 위험군
        public int TotalDanger = 0;
        #region singleton
        private static TOBII_Manager instance = null;
        public static TOBII_Manager Instance
        {
            get { return instance; }
        }
        private void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }
        #endregion

        /// <summary>
        /// 스테이지 클리어 시, 넣어주면 됨.
        /// </summary>
        /// <param name="_stage_name"></param>
        /// <param name="_eyes_time"></param>
        /// <param name="_brake_time"></param>
        /// <param name="_is_danger"></param>
        /// 
        public void Add_TOBII_Data(string _stage_name, float _eyes_time, float _brake_time)
        {
            L_TOBII.Add(new TOBII(_stage_name, _eyes_time, _brake_time));
        }

        public void Is_Danger()
        {
            if (check_TotalDanger() == 1)
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
        public int check_TotalDanger()
        {
            if ((scenario1Danger == 1) && (scenario2Danger == 1) && (scenario3Danger == 1) && (scenario4Danger == 1))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void check_Danger()
        {
            scenario1Danger = scenario1(2);
            scenario2Danger = scenario2(0);
            scenario3Danger = scenario3(1);
            scenario4Danger = scenario4(3);
            TotalDanger = check_TotalDanger();
        }

        /// <summary>
        /// 시나리오 1 신호등 체크
        /// </summary>
        /// <param name="index"> 0 임</param>
        /// <returns>
        /// True -> 통과
        /// false -> 위험군
        /// </returns>
        private int scenario1(int index)
        {
            if (L_TOBII[index].Eyes_Time < 2.1f)
            {
                if (L_TOBII[index].Brake_Time < 2.1f)
                    return 1;
                else
                    return 0;
            }
            return 0;
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
        private int scenario2(int index)
        {
            if (L_TOBII[index].Eyes_Time < 2.0f)
            {
                if (L_TOBII[index].Brake_Time < 2.0f)
                    return 1;
                else
                    return 0;
            }
            return 0;
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
        private int scenario3(int index)
        {
            if (L_TOBII[index].Eyes_Time < 1.9f)
            {
                if (L_TOBII[index].Brake_Time < 1.9f)
                    return 1;
                else
                    return 0;
            }
            return 0;
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
        private int scenario4(int index)
        {
            if (L_TOBII[index].Eyes_Time < 1.6f)
            {
                if (L_TOBII[index].Brake_Time < 1.6f)
                    return 1;
                else
                    return 0;
            }
            return 0;
        }
        #endregion

    }
}