﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private static UI_Manager instance = null;
    public static UI_Manager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }

    public GameObject ResultPanel;
    public Camera ResultCamera;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ViewResult() //결과 보여주는 함수
    {
        ResultPanel.SetActive(true); //패널 ON
        ResultCamera.gameObject.SetActive(true);
        //카메라 전환
        ResultCamera.enabled = true;
        mainCamera.enabled = false;
        //Time.timeScale = 0.1f;
    }
}
