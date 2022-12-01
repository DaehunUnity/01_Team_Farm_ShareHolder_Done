using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CamController : MonoBehaviour
{
    public CinemachineFreeLook Cam;
    public Toggle toggle;
    InputController IC;
    // Start is called before the first frame update
    void Start()
    {
        IC = FindObjectOfType<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!toggle.isOn)
        {
            if (Input.GetButton("Fire1"))
            {
                Cam.m_XAxis.m_InputAxisName = "Mouse X";
                Cam.m_YAxis.m_InputAxisName = "Mouse Y";
            }
            else
            {
                Cam.m_XAxis.m_InputAxisName = null;
                Cam.m_YAxis.m_InputAxisName = null;
                Cam.m_XAxis.m_InputAxisValue = 0;
                Cam.m_YAxis.m_InputAxisValue = 0;
            }
        }
        else
        {
            Cam.m_XAxis.Value = IC.t_Input[1].valX;
            Cam.m_YAxis.Value += IC.t_Input[1].valY * Time.deltaTime;
            Cam.m_XAxis.m_InputAxisName = null;
            Cam.m_YAxis.m_InputAxisName = null;
            Cam.m_XAxis.m_InputAxisValue = 0;
            Cam.m_YAxis.m_InputAxisValue = 0;
        }
    }
}
