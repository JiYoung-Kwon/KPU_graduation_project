
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
    public class login_btn_Manager : MonoBehaviour
    {

        [SerializeField] private GameObject Input_ID;
        [SerializeField] private GameObject Input_PW;
        [SerializeField] private GameObject inform_Text;
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
            Input_ID.GetComponent<InputField>().text = string.Empty;
            Input_PW.GetComponent<InputField>().text = string.Empty;
            inform_Text.GetComponent<Text>().text = string.Empty;
        }

        public void check_login()
        {
            try
            {
                ID = Input_ID.GetComponent<InputField>().text;
                PW = Input_PW.GetComponent<InputField>().text;

                if (ID.Length == 0 || PW.Length == 0)
                    throw new NullReferenceException();


                if (Manager.DB_sqlite_Manager.DB_SQLITE_MANAGER.Check_have_ID(ID, PW) == string.Empty)
                {
                    MainScene_Manager.mainscene_manager.Show_Menu();
                }
                else
                {
                    inform_Text.GetComponent<Text>().text = Manager.DB_sqlite_Manager.DB_SQLITE_MANAGER.Check_have_ID(ID, PW);
                    return;
                }


            }
            catch (NullReferenceException)
            {
                if (ID.Length == 0)
                {
                    inform_Text.GetComponent<Text>().text = "아이디를 입력해 주세요.";
                }
                else if (PW.Length == 0)
                {
                    inform_Text.GetComponent<Text>().text = "비밀번호를 입력해 주세요.";
                }
            }
        }
    }
}
