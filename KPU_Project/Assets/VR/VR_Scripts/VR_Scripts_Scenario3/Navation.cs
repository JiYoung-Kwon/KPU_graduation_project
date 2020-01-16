using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navation : MonoBehaviour
{
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

        agent.destination = WayPoint[Nextidx].position;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            Nextidx =  ++Nextidx % WayPoint.Count;
            MoveWayPoint();
        }
    }
}
