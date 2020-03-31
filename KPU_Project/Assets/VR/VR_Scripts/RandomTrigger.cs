using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Trigger;
    [SerializeField] private float NewX = 0, NewY = 0, NewZ = 0;
    [SerializeField] private float Max =0, Min = 0;
    [SerializeField] private bool ChangeX, ChangeY , ChangeZ ;

    // Start is called before the first frame update
    void Start()
    {
        if (ChangeX && ChangeY && ChangeZ)
        {
            NewX = Random.Range(Min, Max);
            NewY = Random.Range(Min, Max);
            NewZ = Random.Range(Min, Max);
        }
        else if (ChangeX && ChangeY)
        {
            NewX = Random.Range(Min, Max);
            NewY = Random.Range(Min, Max);
        }
        else if (ChangeX && ChangeZ)
        {
            NewX = Random.Range(Min, Max);
            NewZ = Random.Range(Min, Max);
        }
        else if (ChangeY && ChangeZ)
        {
            NewY = Random.Range(Min, Max);
            NewZ = Random.Range(Min, Max);
        }
        else if(ChangeX)
            NewX = Random.Range(Min, Max);
        else if(ChangeY)
            NewY = Random.Range(Min, Max);
        else
            NewZ = Random.Range(Min, Max);

        Trigger.transform.position = new Vector3(NewX, NewY, NewZ);
    }
}
