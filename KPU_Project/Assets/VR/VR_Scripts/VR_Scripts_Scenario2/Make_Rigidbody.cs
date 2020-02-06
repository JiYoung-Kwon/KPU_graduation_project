using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_Rigidbody : MonoBehaviour
{
    public Rigidbody rb;
    
    private static Make_Rigidbody make_rigidbody;
    public static Make_Rigidbody MAKE_RIGIDBODY
    {
        get { return make_rigidbody; }
    }

    private void Awake()
    {
        make_rigidbody = GetComponent<Make_Rigidbody>();
    }
    public void Rigidbody()
    {
        rb.isKinematic = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
