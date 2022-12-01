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

    public float move_front { get; private set; } // ������ ������ �Է°�
    public float move_side { get; private set; } // ������ ȸ�� �Է°�
    public bool Action { get; private set; } // ��ȣ�ۿ��� ���� �Է°�

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
                // move�� ���� �Է� ����
                move_front = Input.GetAxis(MovefrontName);
                // rotate�� ���� �Է� ����
                move_side = Input.GetAxis(MovesideName);
                /*
                //��ȣ�ۿ뿡 ���� �Է� ����
                Action = Input.GetButton(ActionName);

                MouseRotX = Input.GetAxis(rotateAxisMouseXName);
                */
            }
        }
    }
}

