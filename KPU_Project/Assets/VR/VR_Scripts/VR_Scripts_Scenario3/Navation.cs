﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Navation : MonoBehaviour
{
    public List<Transform> WayPoint;
    public Transform WayPoint2; //끼어들기차량
    public Transform WayPoint3; //끼어들기차량

    public int Nextidx = 0;
    private NavMeshAgent agent;
    public float times;
    public bool IsArrive = false;

    public GameObject NextCar;

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
        if (VR_CarStop.INTERRUPTIONCAR.CarStop)
        {
            times += Time.deltaTime;
            if (IsArrive)
            {
                NextCar.gameObject.GetComponent<NavMeshAgent>().SetDestination(WayPoint3.position); //끝으로 쭉쭉 직진
                NextCar.gameObject.GetComponent<NavMeshAgent>().speed = 4f; //도착후 감속
            }
            else
            {
                NextCar.gameObject.GetComponent<NavMeshAgent>().speed = 19.5f; //도착후 가속
                NextCar.gameObject.GetComponent<NavMeshAgent>().SetDestination(WayPoint2.position);
            }

            if (NextCar.gameObject.GetComponent<NavMeshAgent>().velocity.sqrMagnitude >= 0.2f * 0.2f && NextCar.gameObject.GetComponent<NavMeshAgent>().remainingDistance < 5f) //5 거리 안에 들어오면 도착으로 침
                IsArrive = true;
        }
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
}