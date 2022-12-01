using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatToDo : MonoBehaviour
{
    public GameObject Plow;
    public GameObject Plant;
    public GameObject Water;
    public GameObject Harvest;

    public void UI_Interaction(int State)
    {
        if(State == 0)
        {
            Plow.SetActive(true);
            Plant.SetActive(false);
            Water.SetActive(false);
            Harvest.SetActive(false);
        }
        else if(State == 1)
        {
            Plow.SetActive(false);
            Plant.SetActive(true);
            Water.SetActive(false);
            Harvest.SetActive(false);
        }
        else if(State == 2)
        {
            Plow.SetActive(false);
            Plant.SetActive(false);
            Water.SetActive(true);
            Harvest.SetActive(false);
        }
        else if(State == 3)
        {
            Plow.SetActive(false);
            Plant.SetActive(false);
            Water.SetActive(false);
            Harvest.SetActive(false);
        }
        else if(State == 4)
        {
            Plow.SetActive(false);
            Plant.SetActive(false);
            Water.SetActive(false);
            Harvest.SetActive(true);
        }
    }

    /*
    if (Input.GetKeyDown(KeyCode.Tab))
    {
                gun_now += 1;
                if (gun_now >= guns.Length)
                    gun_now = 0;
                StartCoroutine(SetGunPlay());
    }
    */
}
