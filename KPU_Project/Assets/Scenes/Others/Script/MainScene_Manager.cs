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

        [Tooltip("Record canvas")]
        [SerializeField] private GameObject Record_canvas;

        #endregion

        #region Scene
        [Header("Scene Name")]

        [Tooltip("VR 씬 이름")]
        [SerializeField] private string VR_Scene_Name;

        [Tooltip("TOBII 씬 이름")]
        [SerializeField] private string TOBII_Scene_Name;

        [Tooltip("Recrod 씬 이름")]
        [SerializeField] private string Recrod_Scene_Name;
        #endregion

        #region 로딩 프로그레스 ( 숫자 있는 거 )

        [Header("Loading Num Progress")]
        [SerializeField] private Loading_Num_progress num_progress;

        [System.Serializable]
        public struct Loading_Num_progress{
        [Header("Loading Object")]
        [Tooltip("로딩 Progress")]
        public GameObject loading_object;
        [Tooltip("로딩 Progress 숫자 있음")]
         public GameObject loading_withnum_object;

        [Header("Loading Image")]
        public RectTransform rectComponent;
        public Image imageComp;

        [Header("Loading Text")]
        public Text text_num;
        public Text text_string;

        }
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
            _singleton = GetComponent<MainScene_Manager>();
        }
        #endregion

        private void Start()
        {
            Is_loading = false;
            Is_login = false;

            ID_PW_canvas.GetComponent<Canvas>().planeDistance = 50f;
            Menu_canvas.GetComponent<Canvas>().planeDistance = 60f;
            Loading_canvas.GetComponent<Canvas>().planeDistance = 70f;

            Menu_canvas.SetActive(false);


            num_progress.loading_object.SetActive(true);
            num_progress.loading_withnum_object.SetActive(false);

            num_progress.text_num.text = 0.ToString() + "%";

            num_progress.text_string.text = "DownLoading";
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
            num_progress.loading_object.SetActive(false); // 그저 도는건 꺼주기.
            num_progress.loading_withnum_object.SetActive(true); // 숫자 로딩 켜준다. ( 메뉴부터는 씬이 바뀌는 거기 때문 )

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
                return;
            }

            StartCoroutine(loading_SceneChanged(async));
            
        }
        /// <summary>
        /// 레코드 캔버스 보여주기.
        /// </summary>
        public void Show_Record()
        {
            Loading_canvas.SetActive(false);

            Record_canvas.SetActive(true);

            Menu_canvas.SetActive(false);
        }
        IEnumerator loading_SceneChanged(AsyncOperation async)
        {
            Show_Loading_Canvas();
          float timer = 0;

          num_progress.imageComp = num_progress.rectComponent.GetComponent<Image>(); // 이미지 세팅
          num_progress.imageComp.fillAmount = 0.0f; // 초기화 시켜준다.

          async.allowSceneActivation = false;

            while (!async.isDone)
            {
                yield return null;

                timer += Time.deltaTime;

                if (async.progress >= 0.9f)
                {
                    num_progress.text_string.text = "Please wait...";
                    num_progress.imageComp.fillAmount = Mathf.Lerp(num_progress.imageComp.fillAmount, 1f, timer);
                    num_progress.text_num.text = (Math.Truncate(num_progress.imageComp.fillAmount * 100)).ToString() + "%";

                    if (num_progress.imageComp.fillAmount == 1f)
                    {
                        async.allowSceneActivation = true;
                    }
                }
                else
                {
                    if(num_progress.imageComp.fillAmount < 0.5f)
                    {
                        num_progress.text_string.text = "DownLoading";
                    }
                    else if(num_progress.imageComp.fillAmount< 0.9f)
                    {
                        num_progress.text_string.text = "Loading";
                    }

                    num_progress.imageComp.fillAmount = Mathf.Lerp(num_progress.imageComp.fillAmount, async.progress, timer);
                    num_progress.text_num.text = (Math.Truncate(num_progress.imageComp.fillAmount * 100)).ToString() + "%";

                    if (num_progress.imageComp.fillAmount>= async.progress)
                    {
                        timer = 0f;
                    }
                }
            }
        }
        
    }
}
