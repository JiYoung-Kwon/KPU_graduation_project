using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Build Setting
    /// 0 : Select
    /// 1 : Original
    /// 2 : Dynavision
    /// </summary>

    public bool timerOn = false;
    public bool lightOn = false;
    public float time = 0;
    public float see_time = 0; //시선
    public float resultTime = 0; //space 누른 시간
    public float sum = 0f; //시선 합계
    public float Bsum = 0f; //break 합계 
    public int randomNum;
    public static int type = 0;

    public GameObject Sphere;
    public GameObject Spheres;
    public Text EyeText;
    public Text timeText;
    public Text EarlyText;
    public Text eAllText; //eye (시선+break)
    public Text tAllText; //time (시선+break)
    public GameObject ResultPanel;
    public List<float> SeeData;
    public List<float> BreakData;

    private static GameManager gm;
    public static GameManager GM
    {
        get { return gm; }
    }

    private void Awake()
    {
        gm = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("StopSign");   
    }

    // Update is called once per frame
    void Update()
    {
        //시간 측정  
        if (timerOn)
        {
            time += Time.deltaTime;
        }

        //앞으로가기 (연동 후, 엑셀로 수정 예정, w키 여러번 누르면 여러번 실행되는 단점 Dynavision은 bool로 조정하기)
        if (Input.GetKeyDown("w") && lightOn == false)
        {
            Debug.Log("앞으로가기");
            if (SceneManager.GetActiveScene().name == "Original")
                StartCoroutine("StopSign");
            else if (SceneManager.GetActiveScene().name == "Dynavision") //간단한 코드로 바꿀것
                StartCoroutine("StopSignDy");
            lightOn = true;
        }
    }

    //랜덤시간 이후 정지 오브젝트 활성화
    IEnumerator StopSign()
    {
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        Debug.Log("멈춰!");
        //Sphere.gameObject.SetActive(true);
        timerOn = true;
    }

    IEnumerator StopSignDy()
    {
        //dynavision : 10번 반복
        //랜덤(3초까지)
        if (SeeData.Count < 10 && BreakData.Count < 10)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Debug.Log("Dyna멈춰!");
            randomNum = Random.Range(0, 54);
            Spheres.transform.GetChild(randomNum).gameObject.SetActive(true); //총 33개(0~32)
            timerOn = true;
        }
        else if (SeeData.Count == 10 || BreakData.Count == 10) //둘중 하나 만족하면
        {
            Debug.Log("끝났덩");
            ViewResult();
        }
    }

    public void ViewResult() //결과 보여주는 함수
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1: //Original
                if (timerOn == false) //구 활성화 전 정지
                {
                    timeText.gameObject.SetActive(false);
                    EyeText.gameObject.SetActive(false);
                    EarlyText.gameObject.SetActive(true);
                }
                else if (see_time == 0)
                {
                    EyeText.text = string.Format("정지 신호를 보지 않음");
                    timeText.text = string.Format("반응 속도 : {0:N3}초", resultTime);
                }
                else
                {
                    EyeText.text = string.Format("시선 측정 : {0:N3}초", see_time); /*"시선 측정 : " + see_time + "초"*/
                    timeText.text = string.Format("반응 속도 : {0:N3}초", resultTime); /*"반응 속도 : " + resultTime + "초";*/
                }
                ResultPanel.SetActive(true); //패널 ON
                Time.timeScale = 0f;

                break;
            case 2: //Dynavision
                    //시선 데이터(list : Overlap) 평균시간 출력하기 - 수정하기
                    //1.시선, 2. 반응속도 3. 시선 + 반응속도 둘다
                for (int i = 0; i < SeeData.Count; i++)
                {
                    sum += SeeData[i];
                }
                for (int i = 0; i < BreakData.Count; i++)
                {
                    Bsum += BreakData[i];
                }

                float total = (sum / SeeData.Count);
                float Btotal = (Bsum / BreakData.Count);

                switch (type)
                {
                    case 1: //시선
                        EyeText.text = string.Format("평균 시간(시선) : {0:N3}초", total);
                        EyeText.gameObject.SetActive(true);                      
                        break;
                    case 2: //break
                        timeText.text = string.Format("반응 시간 : {0:N3}초", Btotal);
                        timeText.gameObject.SetActive(true);
                        break;
                    case 3:
                        eAllText.text = string.Format("시선 측정 : {0:N3}초", total);
                        tAllText.text = string.Format("반응 시간 : {0:N3}초", Btotal);
                        eAllText.gameObject.SetActive(true);
                        tAllText.gameObject.SetActive(true);
                        break;
                }

                ResultPanel.SetActive(true); //패널 ON
                Time.timeScale = 0f;

                break;

        }
    }
}
