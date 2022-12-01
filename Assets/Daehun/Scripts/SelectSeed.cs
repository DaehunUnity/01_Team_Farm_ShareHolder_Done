using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSeed : MonoBehaviour
{
    public GameObject[] SelectSeeds;
    public int num = 0;

    public int count;
    public Text[] numberseedTxt;


    private void Start()
    {
        SelectSeeds[0].SetActive(true);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Selection_Before();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Selection_Next();
        }
        maxSeed();
    }
    void Selection_Before()
    {
        SelectSeeds[num].gameObject.SetActive(false);
        if(num == 0)
        {
            num = SelectSeeds.Length - 1;
            SelectSeeds[num].gameObject.SetActive(true);
        }
        else if(num > 0)
        {
            num--;
            SelectSeeds[num].gameObject.SetActive(true);
        }
    }

    void Selection_Next()
    {
        SelectSeeds[num].gameObject.SetActive(false);
        if(num == SelectSeeds.Length - 1)
        {
            num = 0;
            SelectSeeds[num].gameObject.SetActive(true);
        }
        else if(num < SelectSeeds.Length -1)
        {
            num++;
            SelectSeeds[num].gameObject.SetActive(true);
        }        
    }
    void maxSeed()
    {
        if (count > 99)
        {
            count = 99;
        }
        else if (count <= 0)
        {
            count = 0;
        }
    }
}
