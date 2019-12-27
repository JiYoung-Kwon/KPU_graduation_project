using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fove.Unity;

public class CarController : MonoBehaviour
{
    private static CarController cc;
    public static CarController Carcontroller
    {
        get { return cc; }
    }

    private void Awake()
    {
        cc = GetComponent<CarController>();
    }

    LogitechGSDK.LogiControllerPropertiesData properties;

    float handle_Input;
    float accel_Input;
    float m_SteeringAngle;
    public float break_Input;

    int drivingMode = 0;

    public WheelCollider Front_Right_Wheel, Front_Left_Wheel;
    public WheelCollider Back_Right_Wheel, Back_Left_Wheel;

    public Transform Front_Right_Tr, Front_Left_Tr;
    public Transform Back_Right_Tr, Back_Left_Tr;
    public Transform Handle;

    public Text Velocity_text;
    public Text RPM_text;
    public Text Gear_text;

    public bool[] Gear;

    public float MaxSteerAngle;
    public float motorForce;
    public float breakForce;
    public float motor_torque = 0;
    
    Vector3 pre;
    float max_torque = 0;
    public float times;
    public float Rpm_Power=0;
    public float Break_Power = 0;
    public float addForce_speed= 0;
    
    Vector3 vec_pre;
    private void Start()
    {
        vec_pre = Vector3.zero;
        FoveManager.TareOrientation();
    }
    private void Update()
    {
        
    }
    public void GetInput()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(1);
        
        handle_Input = (recs.lX / 32768f);
        accel_Input = (1 - (recs.lY / 32767f)) / 2;
        break_Input = (1 - (recs.lRz / 32767f)) / 2;


        if ((recs.lY / 32767f) != 1)
        {
            drivingMode = -1;
        }
        if((recs.lRz / 32767f) != 1)
        {
            drivingMode = 0;
        }
    }

    void Steer()
    {
        m_SteeringAngle = MaxSteerAngle * handle_Input;
        Handle.localRotation = Quaternion.Euler(0, 0, handle_Input * 420f);
        Front_Left_Wheel.steerAngle = m_SteeringAngle;
        Front_Right_Wheel.steerAngle = m_SteeringAngle;
    }

    void Accel()
    {
        if (Rpm_Power == 0)
        {
            if (drivingMode != 0)
            {
               if(motor_torque< -5 * drivingMode)
                {
                    motor_torque -= -0.05f * drivingMode;
                }
                else
                motor_torque = -0.00005f * drivingMode;
            }
            //else
            //    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else motor_torque = -10 * drivingMode * Rpm_Power;

        Back_Left_Wheel.motorTorque = motor_torque;
        Back_Right_Wheel.motorTorque = motor_torque;

        Vector3 vec_new = this.transform.localPosition;
        //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
        float mluti = motor_torque * -1 * 5f;

        if (this.GetComponent<Rigidbody>().velocity.magnitude < 20f && Rpm_Power>0)
        {
           
            this.GetComponent<Rigidbody>().AddForce((vec_new - vec_pre) * 100f * mluti);
        }
        else
        {
            this.GetComponent<Rigidbody>().mass = 2000f;
        }
        
        if(drivingMode == -1&& Rpm_Power > 0 && this.GetComponent<Rigidbody>().velocity.magnitude < 20f)
        {
            this.GetComponent<Rigidbody>().AddForce((vec_new - vec_pre) * 200f * -mluti);
        }

        vec_pre = this.transform.localPosition;

    }

    void Break()
    {
        Back_Left_Wheel.brakeTorque = breakForce * break_Input;
        Back_Right_Wheel.brakeTorque = breakForce * break_Input;
        Front_Right_Wheel.brakeTorque = breakForce * break_Input;
        Front_Left_Wheel.brakeTorque = breakForce * break_Input;

        if (break_Input > 0)
        {
            Back_Left_Wheel.motorTorque = 0;
            Back_Right_Wheel.motorTorque = 0;
        }
    }
    void UpdateWheel()
    {
        UpdateWheel(Front_Right_Wheel, Front_Right_Tr);
        UpdateWheel(Front_Left_Wheel, Front_Left_Tr);
        UpdateWheel(Back_Right_Wheel, Back_Right_Tr);
        UpdateWheel(Back_Left_Wheel, Back_Left_Tr);
    }
    void UpdateWheel(WheelCollider wheelCollider, Transform transform_)
    {
        Vector3 _pos = Vector3.zero;
        Quaternion _quat = Quaternion.identity;

        wheelCollider.GetWorldPose(out _pos,out _quat);


        transform_.rotation = _quat;
    }

    void Rpm()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(1);
        if ((recs.lY / 32767f) == 1) Rpm_Power = 0;
        else if ((recs.lY / 32767f) > 0.75)
            Rpm_Power = 1 ;
        else if ((recs.lY / 32767f) > 0.5)
            Rpm_Power = 1.5f;
        else if ((recs.lY / 32767f) > 0.25)
            Rpm_Power = 2.25f ;
        else if ((recs.lY / 32767f) > 0)
            Rpm_Power = 2.25f * 1.5f ;
        else if ((recs.lY / 32767f) > -0.25)
            Rpm_Power = 2.25f * 1.5f * 1.5f ;
        else if ((recs.lY / 32767f) > -0.5)
            Rpm_Power = 2.25f * 1.5f * 1.5f * 1.5f ;
        else if ((recs.lY / 32767f) > -0.75)
            Rpm_Power = 2.25f * 1.5f * 1.5f * 1.5f ;
        else
            Rpm_Power = 2.25f * 1.5f * 1.5f * 1.5f ;
        //-32768 ~ 32767
    }

    void Break_Sensor()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(1);
        if ((recs.lRz / 32767f) == 1) Break_Power = 0;
        else if ((recs.lRz / 32767f) > 0.75)
            Break_Power = 0.3f;
        else if ((recs.lRz / 32767f) > 0.5)
            Break_Power = 0.3f;
        else if ((recs.lRz / 32767f) > 0.25)
            Break_Power = 50;
        else if ((recs.lRz / 32767f) > 0.125)
            Break_Power = 500;
        else if ((recs.lRz / 32767f) > 0.075)
            Break_Power = 800;
        else if ((recs.lRz / 32767f) > 0.03)
            Break_Power = 1000;
        else if ((recs.lRz / 32767f) > -0.25)
            Break_Power = 1000;
        else if ((recs.lRz / 32767f) > -0.5)
            Break_Power = 1000;
        else
            Break_Power = 1000;
        //1000~32767
    }


    void RPM_Velocity()
    {
        Vector3 now = this.transform.localPosition;
        
        string vel = Mathf.Abs((float)((now - pre).magnitude * 60*4.51)).ToString("F0");
        
        //Velocity_text.text = vel + "Km/h";

        pre = this.transform.localPosition;
    }

    
    private void FixedUpdate()
    {
        LogitechGSDK.LogiSteeringInitialize(true);
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.LogiPlayDamperForce(1, 40);
            //LogitechGSDK.LogiPlaySpringForce(1,0,50,40);
            GetInput();
            Steer();
            Accel();
            Break();
            UpdateWheel();
            Rpm();
            Break_Sensor();
            RPM_Velocity();
        }
    }
}
