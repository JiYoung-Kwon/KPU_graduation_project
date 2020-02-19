using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tobii
{
    public class SceneChange : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NextScenario() //다음 시나리오 (OK)
        {
            GazeEvent.Instance.InitEvent();
            int curScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = curScene + 1;
            SceneManager.LoadScene(nextScene);
            Time.timeScale = 1f;
        }

        public void ReturnMenu() //메뉴로 돌아가기 (현재 로그인 화면으로 돌아감)
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}