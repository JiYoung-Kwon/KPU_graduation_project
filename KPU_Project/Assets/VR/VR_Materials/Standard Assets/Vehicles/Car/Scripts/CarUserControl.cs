using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{

    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        float handle_Input;
        float accel_Input;
        float m_SteeringAngle;
        public float break_Input;

        public float MaxSteerAngle;
        public float motorForce;
        public float breakForce;
        //public List<float> motor_torque = new List<float>();
        //public List<float> brake_torque = new List<float>();
        public Transform Handle;
        public Transform Rpm_Pointer;
        public Transform Speed_Pointer;

        private CarController m_Car; // the car controller we want to use

        int drivingMode = 0;

        #region singleton
        private static CarUserControl instance = null;
        public static CarUserControl Instance
        {
            get { return instance; }
        }
        #endregion


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                return;
            }
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        public void Start()
        {
            //for (int i = 0; i < 4; i++)
            //{
            //    //Debug.Log(CarController.Carcontroller.m_WheelColliders[i].motorTorque);
            //    motor_torque.Add(CarController.Carcontroller.m_WheelColliders[i].motorTorque);
            //    brake_torque.Add(CarController.Carcontroller.m_WheelColliders[i].brakeTorque);

            //}
        }
        private void FixedUpdate()
        {
            // pass the input to the car!
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");
            /*GetInput(); //logitech �� �޾ƿ���
            Steer(); //�ڵ� ȸ��
            RPM(); //�����, rpm
            CarSpeed(); //�ӵ���*/
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            //float h = handle_Input;
            //float v = accel_Input;
            //float b = break_Input;
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            m_Car.Move(h, v, v, handbrake);
            /*if (drivingMode == -1)
                m_Car.Move(h, v, v, handbrake);
            else if (drivingMode == 1)
                m_Car.Move(h, -v, -v, handbrake);//Move(float steering, float accel, float footbrake, float handbrake)
            else
            { }*/
#else
            m_Car.Move(h, v, v, 0f);
            
#endif
            if (break_Input > 0)
            {

                for (int i = 0; i < 4; i++)
                {
                    CarController.Carcontroller.m_WheelColliders[i].motorTorque = 0f;
                    CarController.Carcontroller.m_WheelColliders[i].brakeTorque = break_Input * 100000f;
                    //m_Car.Move(h, v, v, handbrake);
                }
            }
            else
            {
                if (CarController.Carcontroller.CurrentSpeed <= 0.0001f)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        CarController.Carcontroller.m_WheelColliders[i].motorTorque = 1f;
                        CarController.Carcontroller.m_WheelColliders[i].brakeTorque = 0f;
                    }
                }
            }
        }

        public void GetInput()
        {
            LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(1);

            handle_Input = (recs.lX / 32768f);
            accel_Input = (1 - (recs.lY / 32767f)) / 2;
            break_Input = (1 - (recs.lRz / 32767f)) / 2;

            // �� ���� �ø��� ����
            if (recs.rgbButtons[14] == 128)
            {
                drivingMode = -1;
            }

            // �� �Ʒ��� ������ ����
            else if (recs.rgbButtons[15] == 128)
            {
                drivingMode = 1;
            }

            else
            {
                drivingMode = 0;
            }
        }

        // �ڵ��� ȸ������ ���� ������ �ڵ� ��ȭ & �ڵ� ȸ������ ���� ������ ȸ�� �� ����
        void Steer()
        {
            m_SteeringAngle = MaxSteerAngle * handle_Input;
            Handle.localRotation = Quaternion.Euler(0, 0, -handle_Input * 420f);
        }

        void RPM()
        {
            float rpm = 0f;

            for (int i = 0; i < 4; i++)
            {
                rpm += CarController.Carcontroller.m_WheelColliders[i].rpm;
                //Debug.Log(CarController.Carcontroller.m_WheelColliders[i].rpm);
            }

            rpm = rpm / 4;
            //Debug.Log(rpm);
            // ���� ���ο� �ִ� RPM Pointer�� Accel�� ���⿡ ���� ��ȯ 
            if (rpm < 0)
                Rpm_Pointer.localRotation = Quaternion.Euler(0, 0, 0);
            else if (rpm > 900)
                Rpm_Pointer.localRotation = Quaternion.Euler(0, 0, -240f);
            else
                Rpm_Pointer.localRotation = Quaternion.Euler(0, 0, -rpm / 1000f * 240f);
        }

        void CarSpeed()
        {
            float speed = 0f;
            //Debug.Log(CarController.Carcontroller.CurrentSpeed * 1.609344);
            speed = CarController.Carcontroller.CurrentSpeed;

            if (speed < 0)
                Speed_Pointer.localRotation = Quaternion.Euler(0, 0, 0);
            else if (speed > 200)
                Speed_Pointer.localRotation = Quaternion.Euler(0, 0, -240f);
            else
                Speed_Pointer.localRotation = Quaternion.Euler(0, 0, -speed / 200 * 240f);
        }
    }
}