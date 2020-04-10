using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_CarStop : MonoBehaviour
{
    private static VR_CarStop Interruptioncar;
    public static VR_CarStop INTERRUPTIONCAR
    {
        get { return Interruptioncar; }
    }

    private void Awake()
    {
        Interruptioncar = GetComponent<VR_CarStop>();
    }
    public bool CarStop = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CarStop = true;
        }
    }
}
