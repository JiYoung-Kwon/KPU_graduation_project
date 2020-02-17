using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class TOBII_Manager : MonoBehaviour
    {
        [SerializeField] private List<TOBII> L_TOBII = new List<TOBII>();

        #region singleton
        private static TOBII_Manager instance = null;
        public static TOBII_Manager Instance
        {
            get { return instance; }
        }
        private void Awake()
        {
            if(instance == null)
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
        public void Add_TOBII_Data(string _stage_name, float _eyes_time, float _brake_time)
        {
            L_TOBII.Add(new TOBII(_stage_name, _eyes_time, _brake_time));
        }

        public void Is_Danger()
        {
            if (check_Danger())
            {
                // 위험하지 않음... 처리할꺼 넣어주면 됨.
            }
            else
            {
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
        public bool check_Danger()
        {
            if (scenario1(0))
            {
                return true;
            }
            return false;
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
            if (L_TOBII[index].Eyes_Time < 2.1f)
            {
                if (L_TOBII[index].Brake_Time < 2.1f)
                    return scenario2(1);
                else
                    return false;
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
            if (L_TOBII[index].Eyes_Time < 2.0f)
            {
                if (L_TOBII[index].Brake_Time < 2.0f)
                    return scenario3(2);
                else
                    return false;
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
            if (L_TOBII[index].Eyes_Time < 1.9f)
            {
                if (L_TOBII[index].Brake_Time < 1.9f)
                    return scenario4(3);
                else
                    return false;
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
            if (L_TOBII[index].Eyes_Time < 1.6f)
            {
                if (L_TOBII[index].Brake_Time < 1.6f)
                    return true;
                else
                    return false;
            }
            return false;
        }
        #endregion

    }
}