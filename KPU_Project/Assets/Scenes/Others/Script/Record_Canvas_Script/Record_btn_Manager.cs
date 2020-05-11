using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class Record_btn_Manager : MonoBehaviour
    {
        [Header("Scenario")]
        [SerializeField] Image[] Scenario_sprite = null;
        [SerializeField] Image Is_Danger_sprite = null;
        [Header("Sprite")]
        [SerializeField] private Sprite non_pass_sprite = null;
        [SerializeField] private Sprite pass_sprite = null;
        private static Record_btn_Manager instance;
        public static Record_btn_Manager Instance
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
        private void Start()
        {
            Show_result();
            save_user_data.Instance.Record_to_Main = false;
        }
        public void Press_backbtn()
        {
            save_user_data.Instance.Record_to_Main = true;
            SceneManager.LoadScene("Main_Scene");
        }
        // 보이게 하는 건, MainScene_Manager 스크립트 187줄 확인. public void Show_Record()

        private void Show_result()
        {
            Manager.DB_sqlite_Manager.Instance.DB_Read("Select * From Account");

            List<user_data> user_Datas = Manager.DB_sqlite_Manager.Instance.Get_Users_data;

            for (int i = 0; i < user_Datas.Count; ++i)
            {
                if (save_user_data.Instance.Save_ID.Equals(user_Datas[i].Get_ID))
                {
                    for (int j = 0; j < user_Datas[i].Get_Result.Count; ++j)
                    {
                        int result = user_Datas[i].Get_Result[j];
                        switch (result)
                        {
                            case 0:
                                Scenario_sprite[j].sprite = non_pass_sprite;
                                break;
                            case 1:
                                Scenario_sprite[j].sprite = pass_sprite;
                                break;

                        }
                    }

                    if (user_Datas[i].Get_Is_Danger.Equals(1))
                    {
                        Is_Danger_sprite.sprite = pass_sprite;
                    }
                    else
                    {
                        Is_Danger_sprite.sprite = non_pass_sprite;
                    }

                    return;
                }
            }
        }
    }
}
