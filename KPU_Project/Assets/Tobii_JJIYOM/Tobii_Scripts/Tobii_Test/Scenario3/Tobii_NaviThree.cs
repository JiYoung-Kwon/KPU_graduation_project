using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tobii
{
    public class Tobii_NaviThree : MonoBehaviour
    {
        #region Singleton
        private static Tobii_NaviThree navi;
        public static Tobii_NaviThree NAVI
        {
            get { return navi; }
        }

        private void Awake()
        {
            navi = GetComponent<Tobii_NaviThree>();
        }
        #endregion

        public List<Transform> WayPoint;
        public Transform WayPoint2; //끼어들기차량
        public Transform WayPoint3; //끼어들기차량

        public int Nextidx = 0;
        private NavMeshAgent agent;
        public float times;
        public bool IsArrive = false;

        public GameObject myCar;
        public GameObject NextCar;

        public bool IsFail = false;
        public int FailCheck = 0;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            //자동차 도착지점에서 브레이크 효과
            //agent.autoBraking = true;

            //var group = GameObject.Find("WayPointGroup");
            //if (group !=null)
            //{
            //    group.GetComponentsInChildren<Transform>(WayPoint);

            //}
            //WayPoint.RemoveAt(0);
            MoveWayPoint();
        }

        private void MoveWayPoint()
        {
            if (agent.isPathStale)
                return;

            agent.SetDestination(WayPoint[Nextidx].position);
            agent.isStopped = false;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(NextCar.gameObject.GetComponent<NavMeshAgent>().speed);
            //60km/h 기준으로
            //70~75km/h, 약 6.6초로 달리기, 약 139m
            // 트리거를 지났을 경우
            TriggerPass();
            //Debug.Log(NextCar.gameObject.GetComponent<NavMeshAgent>().remainingDistance);


            if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
            {
                if (Nextidx < 3)
                {
                    Nextidx = ++Nextidx % WayPoint.Count;
                    MoveWayPoint();
                }
                else
                {
                    Nextidx = 4;

                }
            }

        }

        void TriggerPass()
        {
            if (Tobii_StopCar.STOPCAR.CarStop)
            {
                times += Time.deltaTime;
                //GazeEvent.Instance.IsEvent = true;

                if (times > 3f && FailCheck == 0)
                {
                    IsFail = true;
                    FailCheck++;
                }

                if (IsArrive) //끼어들고나서 감속
                {
                    NextCar.gameObject.GetComponent<NavMeshAgent>().SetDestination(WayPoint3.position); //끝으로 쭉쭉 직진
                    NextCar.gameObject.GetComponent<NavMeshAgent>().speed = 10f; //도착후 감속
                }
                else //끼어드는거
                {
                    Debug.Log("끼어드는중");
                    Vector3 inin = new Vector3(-58.3f, 0, myCar.transform.localPosition.z + 13f);
                    //NextCar.gameObject.GetComponent<NavMeshAgent>().speed = 19.5f;
                    NextCar.gameObject.GetComponent<NavMeshAgent>().SetDestination(inin);
                }

                if (NextCar.gameObject.GetComponent<NavMeshAgent>().velocity.sqrMagnitude >= 0.2f * 0.2f && NextCar.gameObject.GetComponent<NavMeshAgent>().remainingDistance < 3f) //5 거리 안에 들어오면 도착으로 침
                    IsArrive = true;
            }
        }
    }


}