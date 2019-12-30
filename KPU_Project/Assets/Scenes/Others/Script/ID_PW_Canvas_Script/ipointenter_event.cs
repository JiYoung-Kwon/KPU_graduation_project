using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ipointenter_event : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public string str
    {
        get;set;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.GetChild(1).gameObject.GetComponent<Text>().text != null)
        {
            str = transform.GetChild(1).gameObject.GetComponent<Text>().text;

            transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.GetChild(2).gameObject.GetComponent<Text>().text.Length == 0)
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().text = str;
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
        }
    }
}
