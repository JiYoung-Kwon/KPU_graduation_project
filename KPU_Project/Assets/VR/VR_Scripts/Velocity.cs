using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public Vector3 oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        var distance = (currentPosition - oldPosition);
        var Velocity = distance / Time.deltaTime;
        oldPosition = currentPosition;

        //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
        Debug.Log(Velocity);
    }
}
