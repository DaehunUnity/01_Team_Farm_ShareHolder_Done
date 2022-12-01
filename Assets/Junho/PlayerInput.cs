using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public InputController IC;
    public Toggle toggle;
    public bool useTouch = false;

    public string MovefrontName = "Horizontal";
    public string MovesideName = "Vertical";
    public string ActionName = "Fire2";

    public string rotateAxisMouseXName = "Mouse X";

    public float move_front { get; private set; } // 감지된 움직임 입력값
    public float move_side { get; private set; } // 감지된 회전 입력값
    public bool Action { get; private set; } // 상호작용을 위한 입력값

    public float MouseRotX { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        IC = FindObjectOfType<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
        {
            useTouch = true;
            if(useTouch && IC != null)
            {
                move_front = IC.t_Input[0].valX;
                move_side = IC.t_Input[0].valY;
            }
        }
        else if (!toggle.isOn)
        {
            useTouch = false;
            if (!useTouch)
            {
                // move에 관한 입력 감지
                move_front = Input.GetAxis(MovefrontName);
                // rotate에 관한 입력 감지
                move_side = Input.GetAxis(MovesideName);
                /*
                //상호작용에 관한 입력 감지
                Action = Input.GetButton(ActionName);

                MouseRotX = Input.GetAxis(rotateAxisMouseXName);
                */
            }
        }
    }
}

