﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tobii
{
    public class Tobii_SuddenCar : MonoBehaviour
    {
        public GameObject Target;
        public GameObject Car;
        public bool CarGo = false;
        public float times;

        public bool IsFail = false;
        public int FailCheck = 0;

        #region Singleton
        private static Tobii_SuddenCar instance = null;
        public static Tobii_SuddenCar Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            instance = GetComponent<Tobii_SuddenCar>();
        }
        #endregion

        private void Update()
        {
            if (CarGo == true)
            {
                times += Time.deltaTime;
                GazeEvent.Instance.IsEvent = true;

                if (times > 3f && FailCheck == 0)
                {
                    IsFail = true;
                    FailCheck++;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CarGo = true;
                StartCoroutine(Go());
            }
        }

        IEnumerator Go()
        {
            //anim = start.GetComponent<Animator>();
            //anim.SetBool("Run", true);
            while (Car.transform.position != Target.transform.position)
            {
                //if (anim.GetBool("Dead"))
                //{
                //    break;
                //}
                Car.transform.position = Vector3.MoveTowards(Car.transform.position, Target.transform.position, 0.5f);
                yield return new WaitForSeconds(0.01f);
            }
            //anim.SetBool("Run", false);
        }
    }
}