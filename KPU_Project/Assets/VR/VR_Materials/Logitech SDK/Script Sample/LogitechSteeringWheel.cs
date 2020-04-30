using UnityEngine;
using System.Collections;
using System.Text;

public class LogitechSteeringWheel : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData properties;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));
        LogitechGSDK.LogiPlaySpringForce(0, 0, 20, -40);
        LogitechGSDK.LogiPlaySpringForce(1, 0, 20, 40);
        //LogitechGSDK.LogiPlayConstantForce(0, -100);
        //LogitechGSDK.LogiPlayConstantForce(1, -100);
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(LogitechGSDK.LogiPlaySpringForce(0, 0, 0, -100) + "  " + LogitechGSDK.LogiPlaySpringForce(1,0,0,-100) + "   " + LogitechGSDK.LogiPlayConstantForce(0, -100)+"   "+ LogitechGSDK.LogiPlayConstantForce(1, -100));
       
        //All the test functions are called on the first device plugged in(index = 0)
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(1))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES recs = LogitechGSDK.LogiGetStateUnity(1);

            // ù��°�� ��� , �ι�°�� ������ �߽ɰ�, ����°�� ������ ����, �׹�°�� ����
            LogitechGSDK.LogiPlaySpringForce(0, 0, 20, -40);
            LogitechGSDK.LogiPlaySpringForce(1, 0, 20, 40);

            //Debug.Log("�ڵ� :" + recs.lX);
            //Debug.Log("�׼� :" + recs.lY);
            //Debug.Log("�극��ũ :" + recs.lRz);
            //Debug.Log("�д� Ŭ��ġ :" + recs.rglSlider[0]);


            //if (recs.rgbButtons[4] == 128) Debug.Log("���� �ڵ� ���ӱ� ����");
            //else if (recs.rgbButtons[5] == 128) Debug.Log("���� �ڵ� ���ӱ� ����");
            //else if (recs.rgbButtons[12] == 128) Debug.Log("�»� ���ӱ� ����");
            //else if (recs.rgbButtons[13] == 128) Debug.Log("���� ���ӱ� ����");
            //else if (recs.rgbButtons[14] == 128) Debug.Log("�߻� ���ӱ� ����");
            //else if (recs.rgbButtons[15] == 128) Debug.Log("���� ���ӱ� ����");
            //else if (recs.rgbButtons[16] == 128) Debug.Log("��� ���ӱ� ����");
            //else if (recs.rgbButtons[17] == 128) Debug.Log("���� ���ӱ� ����");

        }
    }



}
