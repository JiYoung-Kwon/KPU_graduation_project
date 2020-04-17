using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record_btn_Manager : MonoBehaviour
{
    [Header("Canvas :")]
    [Tooltip("0 : 메뉴 \n 1 : 레코드 \n 2 : 로딩")]
    [SerializeField] GameObject[] Canvas;

    public void Press_backbtn()
    {
        Canvas[0].SetActive(true);

        Hide_Record();
    }
    private void Hide_Record()
    {
        Canvas[1].SetActive(false);
        Canvas[2].SetActive(true);
    }
}
