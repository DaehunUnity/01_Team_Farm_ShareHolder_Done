using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class MyPlayerMove : MonoBehaviour
{
    CharacterController cController;
    PlayerInput pInput;
    public GameObject[] characters;
    public int char_num = 0;
    public Animator myannim;

    public float speed = 8.0f;
    public float rot_Speed = 5.0f;

    float VelY = 0;

    public float Gravity = 10.0f;
    
    public Camera curCam;
    public GameObject panel;
    public bool Acting = false;

    bool istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();
        pInput = GetComponent<PlayerInput>();

        setChar(0);
    }
    // Update is called once per frame
    void Update()
    {
        OnPanel();
        if (!istalk)
            if(!Acting)
                Move();
    }
    public void Move()
    {
        Vector3 Movedir = new Vector3(pInput.move_front, 0, pInput.move_side);

        float Move_mag = Movedir.magnitude;
        myannim.SetFloat("Idle", Move_mag);

        Quaternion v3Rot = Quaternion.Euler(0,curCam.transform.eulerAngles.y, 0);

        Movedir = v3Rot * Movedir;

        if(Move_mag > 0)
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(Movedir), Time.deltaTime * rot_Speed);

        VelY -= Gravity * Time.deltaTime; //주인공에게 중력을 주기위함

        cController.Move(Movedir * speed * Time.deltaTime
              + Vector3.up * VelY * Time.deltaTime);
    }
    void OnPanel()
    {
        if (panel.activeSelf)
            istalk = true;
        else
            istalk = false;
    }

    void setChar(int num)
    {
        char_num = num;

        for (int i = 0; i < characters.Length; i++)
        {
            if (i == char_num)
                characters[i].SetActive(true);
            else
                characters[i].SetActive(false);
        }
        myannim = characters[char_num].GetComponent<Animator>();
    }
    public void Click_One()
    {
        char_num = 0;

        setChar(char_num);
    }

    public void Click_Two()
    {
        char_num = 1;

        setChar(char_num);
    }

    public void Click_Three()
    {
        char_num = 2;
        
        setChar(char_num);
    }
}
