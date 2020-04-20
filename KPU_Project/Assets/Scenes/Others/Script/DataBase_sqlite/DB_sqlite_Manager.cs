using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using UnityEngine.Networking;

namespace Manager
{
    public class DB_sqlite_Manager : MonoBehaviour
    {
       

        [SerializeField]private List<user_data> Users = new List<user_data>();
        public List<user_data> Get_Users_data
        {
            get { return Users; }
        }

        #region singleton
        private static DB_sqlite_Manager instance;
        public static DB_sqlite_Manager Instance
        {
            get { return instance; }
        }
        private void Awake()
        {
            if (instance == null)
                instance = this;
            DBCreate();
        }
        #endregion
        private void Start()
        {
            DBConnectionCheck();
            DB_Read("Select * From Account");
        }
        #region DB 연결 관련
        private void DBCreate()
        {
            string filepath = string.Empty;

            filepath = Application.dataPath + "/Account.db"; // 경로 설정
            if (!File.Exists(filepath)) // 파일 없으면
            {
                File.Copy(Application.streamingAssetsPath + "/Account.db", filepath); // 경로로 파일 복사
            }

            Debug.Log("파일 생성 완료");

        }
        private string GetDBFilePath()
        {
            string str = string.Empty;
            str = "URI=file:" + Application.dataPath + "/Account.db";
            return str;
        }

        /// <summary>
        /// DB 연결 테스트
        /// </summary>
        /// <returns></returns>
        private void DBConnectionCheck()
        {
            try
            {
                IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
                dbConnection.Open(); // DB 열기

                if (dbConnection.State == ConnectionState.Open) // 디비가 열려있다면
                {
                    Debug.Log("DB연결 성공");
                }
                else
                {
                    Debug.Log("DB연결 실패");
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        #endregion


        #region DB 읽기 쓰기

        /// <summary>
        /// DB 읽기
        /// </summary>
        /// <param name="query">인자로 쿼리문을 받는다</param>
        public void DB_Read(string query)
        {
            Users.Clear();

            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open(); // db 열기
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = query; // 쿼리 입력
            IDataReader dataReader = dbCommand.ExecuteReader(); // 쿼리 실행
            while (dataReader.Read())
            {
                Debug.Log(dataReader.GetString(0) + "," + dataReader.GetString(1) + "," + dataReader.GetString(2) + "," + dataReader.GetString(3)
                     + "," + dataReader.GetString(4) + "," + dataReader.GetString(5) + "," + dataReader.GetString(6) + "," + dataReader.GetString(7));

                Users.Add(new user_data(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetInt32(4),
                    dataReader.GetInt32(5), dataReader.GetInt32(6), dataReader.GetInt32(7)));

            }

            dataReader.Dispose(); //생성 순서와 반대로 닫아주기
            dataReader = null;
            dbCommand.Dispose();
            dbCommand = null;
            dbConnection.Close(); //DB에는 1개의 쓰레드만이 접근할수있고 동시에 접근시 에러 발생
            dbConnection = null;
        }

        /// <summary>
        /// 
        /// DB 쿼리문 작성 하는 것
        /// 
        /// </summary>
        /// <param name="query"> 쿼리문 작성 </param>
        public void DB_Query(string query)
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open(); // db 열기
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = query; // 쿼리 입력

            dbCommand.CommandText = query; // 쿼리 입력
            dbCommand.ExecuteNonQuery(); // 쿼리 실행

            dbCommand.Dispose();
            dbCommand = null;
            dbConnection.Close();
            dbConnection = null;

            // DB_Read("Select * From Account");
        }
        #endregion

        public string Check_have_ID(string ID, string PW)
        {
            DB_Read("Select * From Account");
            for(int i = 0; i < Users.Count; ++i)
            {
                if (ID.Equals(Users[i].Get_ID))
                {
                    if (PW.Equals(Users[i].Get_PW))
                        return string.Empty;
                    else
                        return "비밀번호가 잘못되었습니다.";
                }
                
            }
            return "아이디가 존재하지 않습니다.";
        }

        public bool Check_Muti_ID(string ID)
        {
            DB_Read("Select * From Account");

            for(int i = 0; i < Users.Count; ++i)
            {
                if (ID.Equals(Users[i].Get_ID))
                    return true;
            }
            return false;
        }
    }
}
