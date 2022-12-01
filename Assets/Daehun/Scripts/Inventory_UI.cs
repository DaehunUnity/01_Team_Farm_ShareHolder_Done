using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory_UI : MonoBehaviour
{
    public GameObject InventoryPanel;
    bool InvenOpen = false;
    Vector3 DownPoint;
    Vector3 UpPoint;
    RaycastHit hit;

    GameObject HitObj;

    //public Slot[] Slots; ������Ʈ�� �迭ȭ�Ͽ� ������ ����
    //public Transform slotHolder;

    private void Start()
    {
        //Slots = slotHolder.GetComponentsInChildren<Slot>(); slotHolder�Ʒ��� �ڽ� ��ü���� ������
        InventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenInven();
            
        }
        if (Input.GetButtonDown("Fire1"))
        {
            ClickDown();
        }
        if(Input.GetButton("Fire1"))
        {
            Drag();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ClickUp();
        }
    }
    void OpenInven()
    {
        if (InvenOpen == false)
        {
            InvenOpen = true;
        }
        else
        {
            InvenOpen = false;
        }        
        InventoryPanel.SetActive(InvenOpen);
    }
    
    void ClickDown()
    {
        DownPoint = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(DownPoint);
        if(Physics.Raycast(ray, out hit)) //������Ʈ�� ����
        {
            HitObj = hit.collider.gameObject;
            Debug.Log(HitObj.name);
        }
    }

    void Drag()
    {
        UpPoint = Input.mousePosition;
        Vector3 Distance = DownPoint - UpPoint;
        //HitObj.transform.position = Distance;
    }

    void ClickUp()
    {
        UpPoint = Input.mousePosition;
    }
}
