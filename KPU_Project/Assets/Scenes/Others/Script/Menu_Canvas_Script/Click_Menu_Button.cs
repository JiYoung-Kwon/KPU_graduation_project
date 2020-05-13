using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Button
{
    /// <summary>
    /// 
    /// 메뉴 버튼 클릭 시, 씬 전환 해주는 함수
    /// 
    /// 씬 이름을 Inspector에 작성해야 함.
    /// 
    /// </summary>
    public class Click_Menu_Button : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Manager.MainScene_Manager.mainscene_manager.GO_Scene(gameObject.name);
            Time.timeScale = 1f;
        }
    }
}