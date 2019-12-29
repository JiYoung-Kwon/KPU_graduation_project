using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Manager
{
    /// <summary>
    /// ID 와 Password 를 받고, 전달해주는 스크립트
    /// 
    /// 싱글톤으로 만들었고,
    /// ID와 PW는 여기서 받아서 쓰면 된다.
    /// </summary>
    public class ID_PW_Manager : MonoBehaviour
    {

        [SerializeField] private GameObject Input_ID;
        [SerializeField] private GameObject Input_PW;

        #region 프로퍼티
        public string ID
        {
            get; set;
        }
        public string PW
        {
            get; set;
        }
        #endregion

        private void Start()
        {
            Input_ID = GameObject.Find("InputID").gameObject;
            Input_PW = GameObject.Find("InputPW").gameObject; 
        }

        public void check_login()
        {
            try
            {
                ID = Input_ID.transform.GetChild(2).gameObject.GetComponent<Text>().text;
                PW = Input_PW.GetComponent<InputField>().text;

                Debug.Log(ID + "\n" + PW);
            }
            catch (NullReferenceException)
            {
                Debug.Log("입력이 없음.");
            }
        }
    }
}
