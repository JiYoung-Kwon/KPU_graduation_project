using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class VR_Manager : MonoBehaviour
    {
        [SerializeField] private List<VR> L_VR = new List<VR>();

        /// <summary>
        /// 스테이지 클리어 시, 넣어주면 됨.
        /// </summary>
        /// <param name="_stage_name"></param>
        /// <param name="_eyes_time"></param>
        /// <param name="_brake_time"></param>
        /// <param name="_is_danger"></param>
        public void Set_VR(string _stage_name, float _eyes_time, float _brake_time, Is_Danger _is_danger)
        {
            L_VR.Add(new VR(_stage_name, _eyes_time, _brake_time, _is_danger));
        }


    }
}