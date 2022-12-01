using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_bat : InteractObject
{

    
    public PlantData myPlant;
    public GameObject Plant_Object;
    public GameObject whatToDo;
    public bool waterEnd = false;
    public bool harvest = false;


    [SerializeField]
    public bool plowling_end = false;

    public float now_water = 0;
    public float max_water;

    public float Now_groth = 0;
    public float Max__groth = 100;
    
    private void Start()
    {
        Whatis = "��";
    }

    private void Update()
    {
        if (waterEnd && !harvest)
        {
            Now_groth += Time.deltaTime;
            float groth_rate = Now_groth / Max__groth;
            if (Plant_Object != null)
            {
                Plant_Object.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, groth_rate);
            }

            if (groth_rate >= 1.0f)
                SetState(4);
        }

    }

    void GetChild()
    {
        
    }
    
    public void SetPlant(PlantData _plant)
    {
        myPlant = _plant;
    }
    public void SetClear()
    {
        State = 0;
    }


    public override void SetState(int num)
    {
        if (num == 0) //�� �ʱ�ȭ
        {
            plowling_end = false;
            now_water = 0;
            Now_groth = 0;
            harvest = false;
            waterEnd = false;

            
        }
        else if (num == 1) //�� ����
        {
            if (myPlant != null)
            {
                plowling_end = true;

            }
        }
        else if (num == 2) //���� ���� ����
        {
            if (myPlant != null)
            {
                now_water = 0;
                max_water = myPlant.Require_water;
                Now_groth = 0;
                Max__groth = myPlant.Duration;
                Debug.Log("��¥�ɾ���!");

                Plant_Object = Instantiate(myPlant.PlantModeling, transform.position, Quaternion.identity);
                Plant_Object.transform.parent = gameObject.transform;

                Plant_Object.transform.localScale = new Vector3(0,0,0);

            }
            else
            {
                Debug.Log("���� ������ �������� ����");
            }

        }
        else if (num == 3) //���� �Ѹ�����(�ڶ����)
        {
            if (myPlant != null)
            {
                waterEnd = true;

            }
            else
            {
                Debug.Log("T������ ����");
            }

        }
        else if (num == 4) //���ڶ�(��Ȯ����)
        {
            if (myPlant != null)
            {
                harvest = true;

            }
            else
            {
                Debug.Log("T������ ����");
            }

        }
        base.SetState(num);
    }
    void DestroyOnBat()
    {
        if (Plant_Object != null)
            Destroy(Plant_Object);

        myPlant = null;
    }
}