using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fove.Unity;

public class FOVE : MonoBehaviour
{
    [SerializeField] private GameObject red_Signal;

    private GameObject traffic;
    public bool FindHuman = false;
    public enum EYE_enum
    {
        Left_EYE, Right_EYE
    }

    public EYE_enum instanceEye;
    // public FoveInterface foveInterface;
    
    private static FoveInterface foveInterfaces;
    public static FoveInterface FoveInterface
    {
        get
        {
            if (foveInterfaces == null)
            {
                // returns the first FoveInterface found here but you should adapt this code to your game
                // especially in the case where you could have no or several FoveInterface in your game
                foveInterfaces = FindObjectOfType<FoveInterface>();
            }

            return foveInterfaces;
        }
    }

    private static FOVE fv;
    public static FOVE Fove
    {
        get { return fv; }
    }

    private void Awake()
    {
        fv = GetComponent<FOVE>();
    }

    private void Start()
    {
        
    }
    void Update()
    {
        var rays = FoveInterface.GetGazeRays();
     

        Ray r = instanceEye == EYE_enum.Left_EYE ? rays.left : rays.right;
        RaycastHit hit;
        
    }
}
