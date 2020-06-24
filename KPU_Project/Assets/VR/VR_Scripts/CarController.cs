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
    float accel_Rpm_Point_Input;
    float m_SteeringAngle;
    public float break_Input;

    int drivingMode = 0;

    public WheelCollider Front_Right_Wheel, Front_Left_Wheel;
    public WheelCollider Back_Right_Wheel, Back_Left_Wheel;

    public Transform Front_Right_Tr, Front_Left_Tr;
    public Transform Back_Right_Tr, Back_Left_Tr;
    public Transform Handle;
    public Transform Rpm_Pointer;
    public Transform Speed_Pointer;

    public bool[] Gear;

    public float MaxSteerAngle;
    public float motorForce;
    public float breakForce;
    public float motor_torque = 0;

    Vector3 pre;
    float max_torque = 0;
    public float times;
    public float Break_CheckTime = 0;
    public float Rpm_Power = 0;
    public float Break_Power = 0;
    public float addForce_speed = 0;
    public float Now_speed = 0;

    public bool BreakCheck = true;
    public bool GameStart = false;

    Vector3 vec_pre;
    void Update()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(0);
        //if (FOVE2.Fove2.Times > FOVE2.Fove2.RandTime)
        //{
        //    times += Time.deltaTime;
        //    if (recs.lRz < 32767f & BreakCheck)
        //    {
        //        Break_CheckTime = times;
        //        BreakCheck = false;
        //    }
        //}
    }
    public void GetInput()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(0);

        handle_Input = (recs.lX / 32768f);
        accel_Input = (1 - (recs.lY / 32767f)) / 2;
        accel_Rpm_Point_Input = recs.lY;
        break_Input = (1 - (recs.lRz / 32767f)) / 2;

        // 기어를 위로 올리면 전진
        if (recs.rgbButtons[14] == 128)
        {
            drivingMode = -1;
        }

        // 기어를 아래로 내리면 후진
        else if (recs.rgbButtons[15] == 128)
        {
            drivingMode = 1;
        }
    }
    // 핸들의 회전량에 차량 내부의 핸들 변화 & 핸들 회전량에 따른 바퀴의 회전 값 설정
    void Steer()
    {
        m_SteeringAngle = MaxSteerAngle * handle_Input;
        Handle.localRotation = Quaternion.Euler(0, 0, -handle_Input * 420f);
        Front_Left_Wheel.steerAngle = m_SteeringAngle;
        Front_Right_Wheel.steerAngle = m_SteeringAngle;
    }

    void Accel()
    {
        if (Rpm_Power == 0)
        {
            if (drivingMode != 0)
            {
                // 후진 시 0.05f의 속도만큼 후진
                if (motor_torque < -5 * drivingMode)
                {
                    motor_torque -= -0.05f * drivingMode;
                }
                // 아닐경우에 -0.00005f만큼 속도 감속(빠른속도로 주행중인 차량 서서히 정지)
                // 처음에 아무것도 하지 않았을 경우에 차량이 뒤로가는 문제 해결해야함
                else
                    motor_torque = -0.00005f * drivingMode;
            }
            else
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        // 초반 속도 수정은 이 아래에서부터 해야할듯
        else motor_torque = -10 * drivingMode * Rpm_Power;

        Back_Left_Wheel.motorTorque = motor_torque;
        Back_Right_Wheel.motorTorque = motor_torque;

        Vector3 vec_new = this.transform.localPosition;
        //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
        float mluti = motor_torque * -1 * 5f;

        if (this.GetComponent<Rigidbody>().velocity.magnitude < 20f && Rpm_Power > 0)
        {
            this.GetComponent<Rigidbody>().AddForce((vec_new - vec_pre) * 100f * mluti);
        }
        else
        {
            this.GetComponent<Rigidbody>().mass = 1000f;
        }

        if (drivingMode == -1 && Rpm_Power > 0 && this.GetComponent<Rigidbody>().velocity.magnitude < 20f)
        {
            this.GetComponent<Rigidbody>().AddForce((vec_new - vec_pre) * 200f * -mluti);
        }
        vec_pre = this.transform.localPosition;
    }

    // 브레이크 밟았을때 motorTorque를 0으로 변환
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
    //바퀴의 상황 실시간 업데이트
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

        wheelCollider.GetWorldPose(out _pos, out _quat);

        transform_.rotation = _quat;
    }

    // Accel페달을  밟는 세기에 따른 RPM 수치 변화
    void Rpm()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(0);
        if ((recs.lY / 32767f) == 1) Rpm_Power = 0;
        else if ((recs.lY / 32767f) > 0.75)
            Rpm_Power = 1;
        else if ((recs.lY / 32767f) > 0.5)
            Rpm_Power = 1.5f;
        else if ((recs.lY / 32767f) > 0.25)
            Rpm_Power = 2.25f;
        else if ((recs.lY / 32767f) > 0)
            Rpm_Power = 2.25f * 1.5f;
        else if ((recs.lY / 32767f) > -0.25)
            Rpm_Power = 2.25f * 1.5f * 1.5f;
        else if ((recs.lY / 32767f) > -0.5)
            Rpm_Power = 2.25f * 1.5f * 1.5f * 1.5f;
        else if ((recs.lY / 32767f) > -0.75)
            Rpm_Power = 2.25f * 1.5f * 1.5f * 1.5f;
        else
            Rpm_Power = 2.25f * 1.5f * 1.5f * 1.5f;

        // 차량 내부에 있는 RPM Pointer를 Accel의 세기에 따라 변환 
        Rpm_Pointer.localRotation = Quaternion.Euler(0, 0, -accel_Input * 220f);
        //-32768 ~ 32767
    }

    // 브레이크 페달을 밟는 세기에 따른 브레이크 값 변화
    void Break_Sensor()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(0);
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

    // RPM Velocity를 이용하여 현재의 속도 표현 
    void RPM_Velocity()
    {
        Vector3 now = this.transform.localPosition;
        Now_speed = this.GetComponent<Rigidbody>().velocity.magnitude;
        Speed_Pointer.localRotation = Quaternion.Euler(0, 0, -Now_speed * 10f);

        pre = this.transform.localPosition;
    }

    // Logitech Steering Wheel 실시간 업데이트 및 변화에 따라 값 받아오기
    private void FixedUpdate()
    {
        //if (GameStart)
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
            }
    }
}
