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

    //public Slot[] Slots; 오브젝트를 배열화하여 저장한 공간
    //public Transform slotHolder;

    private void Start()
    {
        //Slots = slotHolder.GetComponentsInChildren<Slot>(); slotHolder아래의 자식 개체들을 가져옴
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
        if(Physics.Raycast(ray, out hit)) //오브젝트가 찍힘
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
