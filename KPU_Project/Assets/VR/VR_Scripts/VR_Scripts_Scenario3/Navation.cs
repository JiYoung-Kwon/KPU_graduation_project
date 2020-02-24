using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navation : MonoBehaviour
{
    private static Navation navation;
    public static Navation NAVATION
    {
        get { return navation; }
    }
    
    private void Awake()
    {
        navation = GetComponent<Navation>();
    }

    public List<Transform> WayPoint;
    public int Nextidx = 0;
    private NavMeshAgent agent;

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
        //agent.destination = WayPoint[Nextidx].position;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 트리거를 지났을 경우 앞차량이 멈추는 코드
        if (SuddenStopCar.SUDDENSTOPCAR.CarStop)
        {
            SuddenStopCar.SUDDENSTOPCAR.FrontCar.gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        }

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
                //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                //gameObject.GetComponent<NavMeshAgent>().enabled = false;         // 목표지점 도착하면 사라짐(바닥으로 강제로 사라짐 ㅋㅋㅋ)
                //gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;  // 목표지점 도착하면 정지(도착장소에서 부들거리는 단점 있음)
                Destroy(this.gameObject);
            }
        }
    }
}