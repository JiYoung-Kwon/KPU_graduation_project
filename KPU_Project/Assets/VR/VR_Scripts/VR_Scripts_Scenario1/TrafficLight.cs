using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject Red;
    public GameObject Yellow;
    public GameObject Green;

    public int green_t;
    public float Yellow_t;
    public int Red_t;

    public int Start_sig;

    public float times;


    // Start is called before the first frame update
    void Start()
    {
        // 색 시간 설정
        green_t = 8;
        Yellow_t = 1;
        Red_t = 4;

        // 처음 시작 후 Red, Yellow 꺼짐
        Red.SetActive(false);
        Yellow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //신호등 설정 시간에 맞추어 색변환 시켜주기
        if (Green.active)
        {
            times += Time.deltaTime;
            if (times > green_t)
            {
                Yellow.SetActive(true);
                Green.SetActive(false);
                times = 0;
            }
        }
        if (Yellow.active)
        {
            times += Time.deltaTime;
            if (times > Yellow_t)
            {
                Red.SetActive(true);
                Yellow.SetActive(false);
                times = 0;
            }
        }
        if (Red.active)
        {
            times += Time.deltaTime;
            if (times > Red_t)
            {
                Green.SetActive(true);
                Red.SetActive(false);
                times = 0;
            }
        }
    }
}
