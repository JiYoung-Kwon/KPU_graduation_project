using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    /// <summary>
    ///  싱글톤 변수 관리
    /// </summary>
    public class MainScene_Manager : MonoBehaviour
    {
        private static MainScene_Manager _singleton;
        public static MainScene_Manager mainscene_manager
        {
            get { return _singleton; }
        }

        public bool Is_loading
        {
            get;set;
        }
        [SerializeField] private GameObject Loading_canvas;
        [SerializeField] private GameObject ID_PW_canvas;
        [SerializeField] private GameObject Menu_canvas;
        /// <summary>
        /// 로그인 되었는지 아닌지를 체크해준다.
        /// </summary>
        public bool Is_login
        {
            get;set;
        }
        private void Awake()
        {
            _singleton = GetComponent<MainScene_Manager>();
        }
        private void Start()
        {
            Is_loading = false;
            Is_login = false;

            ID_PW_canvas.GetComponent<Canvas>().planeDistance = 50f;
            Menu_canvas.GetComponent<Canvas>().planeDistance = 60f;
            Loading_canvas.GetComponent<Canvas>().planeDistance = 70f;

            
        }
        
       /// <summary>
       /// ID 와 PW 를 DB와 연동해서 체크해준다
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="ID"> 넘겨받은 ID </param>
       /// <param name="PW"> 넘겨받은 PW </param>
        public void check_userinformation<T>(T ID, T PW) where T : class
        {
            Show_Loading_Canvas();
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
            Is_login = false;
            Loading_canvas.GetComponent<Canvas>().planeDistance = 70f;
        }

        private void Show_Menu_Canvas()
        {
            
            ID_PW_canvas.GetComponent<Canvas>().planeDistance = 100f;
            ID_PW_canvas.SetActive(false);

            Hide_Loading_Canvas();

            Menu_canvas.GetComponent<Canvas>().planeDistance = 50f;
        }
        
    }
}
