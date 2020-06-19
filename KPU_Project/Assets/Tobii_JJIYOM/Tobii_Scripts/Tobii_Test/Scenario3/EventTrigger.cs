using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tobii
{
    public class EventTrigger : MonoBehaviour
    {
        #region Singleton
        private static EventTrigger et;
        public static EventTrigger ET
        {
            get { return et; }
        }

        private void Awake()
        {
            et = GetComponent<EventTrigger>();
        }
        #endregion

        public bool firstTrigger = false;
        public bool secondTrigger = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && this.name == "FirstTrigger")
            {
                Debug.Log("첫 트리거");
                firstTrigger = true;
            }
            else if (other.gameObject.CompareTag("Player") && this.name == "SecondTrigger")
            {
                secondTrigger = true;
            }
        }
    }
}