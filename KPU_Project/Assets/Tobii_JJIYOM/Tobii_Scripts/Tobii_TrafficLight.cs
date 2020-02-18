using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tobii
{
    public class Tobii_TrafficLight : MonoBehaviour
    {
        public GameObject Red;
        public GameObject Green;

        public int green_t;
        public int Red_t;

        public int Start_sig;

        public float times;
        public bool IsEvent = false;

        private static Tobii_TrafficLight tt;
        public static Tobii_TrafficLight TT
        {
            get { return tt; }
        }

        private void Awake()
        {
            tt = GetComponent<Tobii_TrafficLight>();
        }


        // Start is called before the first frame update
        void Start()
        {
            // 색 시간 설정
            green_t = 8;            
            Red_t = 4;

            // 처음 시작 후 Red 꺼짐
            Red.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            //신호등 설정 시간에 맞추어 색변환 시켜주기
            if (Green.activeSelf)
            {
                times += Time.deltaTime;
                if (times > green_t)
                {
                    IsEvent = true;
                    Red.SetActive(true);
                    Green.SetActive(false);
                    times = 0;
                }
            }
            if (Red.activeSelf)
            {
                times += Time.deltaTime;
                if (times > Red_t)
                {
                    IsEvent = false;
                    Green.SetActive(true);                    
                    Red.SetActive(false);
                    times = 0;
                }
            }
        }
    }
}
