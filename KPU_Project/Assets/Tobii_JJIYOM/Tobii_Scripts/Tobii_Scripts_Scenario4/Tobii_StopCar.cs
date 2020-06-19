using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tobii
{
    public class Tobii_StopCar : MonoBehaviour
    {
        #region Singleton
        private static Tobii_StopCar stopcar;
        public static Tobii_StopCar STOPCAR
        {
            get { return stopcar; }
        }

        private void Awake()
        {
            stopcar = GetComponent<Tobii_StopCar>();
        }
        #endregion

        public GameObject FrontCar;

        public bool CarStop = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CarStop = true;
            }
        }
    }
}