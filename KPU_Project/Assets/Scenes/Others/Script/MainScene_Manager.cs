using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    /// <summary>
    ///  싱글톤 변수 관리
    /// </summary>
    public class MainScene_Manager : MonoBehaviour
    {
        #region Canvas
        [Header("Canvas")]
        [Tooltip("loading canvas")]
        [SerializeField] private GameObject Loading_canvas;

        [Tooltip("ID PW canvas")]
        [SerializeField] private GameObject ID_PW_canvas;

        [Tooltip("Menu canvas")]
        [SerializeField] private GameObject Menu_canvas;

        #endregion

        #region Scene
        [Header("Scene Name")]

        [Tooltip("VR 씬 이름")]
        [SerializeField] private string VR_Scene_Name;

        [Tooltip("TOBII 씬 이름")]
        [SerializeField] private string TOBII_Scene_Name;

        [Tooltip("Recrod 씬 이름")]
        [SerializeField] private string Record_Scene_Name;
        #endregion

        #region 로딩 프로그레스

        [Header("Loading Progress")]
        [SerializeField] private GameObject loading_object;
        #endregion
        #region 프로퍼티
        /// <summary>
        /// 로그인 되었는지 아닌지를 체크해준다.
        /// </summary>
        public bool Is_login
        {
            get;set;
        }
        public bool Is_loading
        {
            get; set;
        }
        #endregion

        #region 싱글 톤

        private static MainScene_Manager _singleton;
        public static MainScene_Manager mainscene_manager
        {
            get { return _singleton; }
        }

        private void Awake()
        {
            if (_singleton == null)
                _singleton = this;
        }
        #endregion

        private void Start()
        {
            Is_loading = false;
            Is_login = false;

               
            Menu_canvas.GetComponent<Canvas>().planeDistance = 60f;
            Loading_canvas.GetComponent<Canvas>().planeDistance = 70f;

            
            loading_object.SetActive(true);
           
            if (save_user_data.Instance.Record_to_Main)
            {
                ID_PW_canvas.SetActive(false);
                Menu_canvas.SetActive(true);
            }
            else
            {
                ID_PW_canvas.GetComponent<Canvas>().planeDistance = 50f;
                Menu_canvas.SetActive(false);
            }
        }
        
       /// <summary>
       /// 메뉴 열어주는 함수
       /// </summary>
        public void Show_Menu()
        {
            Show_Loading_Canvas();
            StartCoroutine(Cor_Show_Menu());
        }
        IEnumerator Cor_Show_Menu()
        {
            yield return new WaitForSeconds(1f);
            Show_Menu_Canvas();
        }

        /// <summary>
        ///  로딩 씬을 보이게 해준다.
        /// </summary>
        private void Show_Loading_Canvas()
        {
            Is_loading = true;
            Loading_canvas.GetComponent<Canvas>().planeDistance = 40f;
        }

        /// <summary>
        /// 로딩 씬을 숨김.
        /// </summary>
        private void Hide_Loading_Canvas()
        {
            Is_loading = false;
            Loading_canvas.GetComponent<Canvas>().planeDistance = 70f;
        }

        private void Show_Menu_Canvas()
        {
            ID_PW_canvas.GetComponent<Canvas>().planeDistance = 100f;
            ID_PW_canvas.SetActive(false);

            Hide_Loading_Canvas();
           loading_object.SetActive(false); // 그저 도는건 꺼주기.

            Menu_canvas.SetActive(true);
            Menu_canvas.GetComponent<Canvas>().planeDistance = 50f;
        }

        /// <summary>
        /// 씬 전환 함수
        /// </summary>
        /// <param name="btn_name"> 버튼 누른것에 맞춰서 맞는 씬으로 이동 </param>
        public void GO_Scene(string btn_name)
        {
            AsyncOperation async;
            if (btn_name.Contains("VR"))
            {
                async = SceneManager.LoadSceneAsync(VR_Scene_Name);
            }
            else if (btn_name.Contains("TOBII"))
            {
                async = SceneManager.LoadSceneAsync(TOBII_Scene_Name);
            }
            else
            {
                SceneManager.LoadScene(Record_Scene_Name);
                return;
            }

            StartCoroutine(loading_SceneChanged(async));
            
        }
        
        IEnumerator loading_SceneChanged(AsyncOperation async)
        {
          Show_Loading_Canvas(); 

          async.allowSceneActivation = false;
          loading_object.SetActive(true);
            while (!async.isDone)
            {
                yield return null;
                if (async.progress >= 0.9f)
                {
                  async.allowSceneActivation = true;
                }
            }
        }
        
    }
}
