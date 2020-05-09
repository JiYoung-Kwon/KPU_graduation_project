using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Manager
{
    public class Create_Account_Manager : MonoBehaviour
    {
        [SerializeField] private GameObject Create_Account_Canvas;
        [SerializeField] private GameObject ID_PW_Canvas;
        [SerializeField] private GameObject Input_ID;
        [SerializeField] private GameObject Input_PW;
        [SerializeField] private GameObject Input_Name;
        [SerializeField] private GameObject inform_Text;

        [System.Serializable]
        struct Account
        {
            public string ID;
            public string PW;
            public string Name;
        }
        [SerializeField] Account account;

        public void Btn_Create_Account()
        {
            ID_PW_Canvas.SetActive(false);
            Create_Account_Canvas.SetActive(true);

            Input_ID.GetComponent<InputField>().text = string.Empty;
            Input_PW.GetComponent<InputField>().text = string.Empty;
            Input_Name.GetComponent<InputField>().text = string.Empty;

            Input_ID.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "ID 입력 ( 12글자 이내 )";
            Input_PW.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "비밀번호 입력 ( 12글자 이내 )";
            Input_Name.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "영어 이름 입력";

            inform_Text.GetComponent<Text>().text = string.Empty;

        }

        public void Btn_Back()
        {
            ID_PW_Canvas.SetActive(true);
            Create_Account_Canvas.SetActive(false);
        }
        public void Btn_Add_Account()
        {
            try
            {
                account.ID = Input_ID.GetComponent<InputField>().text;
                account.PW = Input_PW.GetComponent<InputField>().text;
                account.Name = Input_Name.GetComponent<InputField>().text;

                if (account.ID.Length == 0 || account.PW.Length == 0 || account.Name.Length == 0)
                    throw new NullReferenceException();

                if (Manager.DB_sqlite_Manager.Instance.Check_Muti_ID(account.ID))
                {
                    inform_Text.GetComponent<Text>().text = "아이디가 중복됩니다.";
                    return;
                }

                string query = "Insert Into Account(ID, Password, Name, Scenario1, Scenario2, Scenario3, Scenario4, Is_Danger)";
                query += string.Format("VALUES (\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\")", account.ID, account.PW, account.Name, 0, 0, 0, 0, 0);
                Debug.Log(query);

                Manager.DB_sqlite_Manager.Instance.DB_Query(query);

                ID_PW_Canvas.SetActive(true);
                Create_Account_Canvas.SetActive(false);

            }
            catch (NullReferenceException)
            {
                if (account.ID.Length == 0)
                {
                    inform_Text.GetComponent<Text>().text = "아이디를 입력해 주세요.";
                }
                else if (account.PW.Length == 0)
                {
                    inform_Text.GetComponent<Text>().text = "비밀번호를 입력해 주세요.";
                }
                else if (account.Name.Length == 0)
                {
                    inform_Text.GetComponent<Text>().text = "이름을 입력해 주세요.";
                }
            }
        }
    }
}