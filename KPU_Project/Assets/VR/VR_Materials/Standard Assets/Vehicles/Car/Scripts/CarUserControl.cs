using System;
using UnityEngine;
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
        public float motor_torque = 0;
        public Transform Handle;


        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");
            GetInput();
            Steer();

            float h = handle_Input;
            float v = accel_Input - break_Input;
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");           
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

        public void GetInput()
        {
            LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(1);

            handle_Input = (recs.lX / 32768f);
            accel_Input = (1 - (recs.lY / 32767f)) / 2;
            break_Input = (1 - (recs.lRz / 32767f)) / 2;

            //// 기어를 위로 올리면 전진
            //if (recs.rgbButtons[14] == 128)
            //{
            //    drivingMode = -1;
            //}

            //// 기어를 아래로 내리면 후진
            //else if (recs.rgbButtons[15] == 128)
            //{
            //    drivingMode = 1;
            //}
        }

        // 핸들의 회전량에 차량 내부의 핸들 변화 & 핸들 회전량에 따른 바퀴의 회전 값 설정
        void Steer()
        {
            m_SteeringAngle = MaxSteerAngle * handle_Input;
            Handle.localRotation = Quaternion.Euler(0, 0, -handle_Input * 420f);
        }
    }



}
