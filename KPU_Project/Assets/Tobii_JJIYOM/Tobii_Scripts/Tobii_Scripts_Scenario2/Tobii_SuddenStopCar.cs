﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tobii
{
    public class Tobii_SuddenStopCar : MonoBehaviour
    {

        #region Singleton
        private static Tobii_SuddenStopCar suddenstopcar;
        public static Tobii_SuddenStopCar SUDDENSTOPCAR
        {
            get { return suddenstopcar; }
        }

        private void Awake()
        {
            suddenstopcar = GetComponent<Tobii_SuddenStopCar>();
        }
        #endregion

        public GameObject FrontCar;
        public GameObject Light;
        public GameObject Light1;
        public GameObject Light2;
        public GameObject Light3;
        public GameObject RealCollider;
        public bool CarStop = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CarStop = true;
                RealCollider.SetActive(true);
                Light.gameObject.SetActive(true);
                Light1.gameObject.SetActive(true);
                Light2.gameObject.SetActive(true);
                Light3.gameObject.SetActive(true);

                // 지정차량 부모자식 해제하여 차 멈추게 하기
                // Make_Rigidbody.MAKE_RIGIDBODY.Rigidbody();
                // FrontCar.transform.parent = FrontCar.transform.parent.parent;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
         
        }
    }
}