using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count_Button : MonoBehaviour
{
    public Text Count;
    public int Count_num;
    public Calculate cal;

    private void OnEnable()
    {
        Count.text = "0";
        Count_num = 0;
    }
    public void Minus()
    {
        if(Count_num > 0)
        Count_num--;

        Count.text = Count_num.ToString();
        cal.Calculation();
    }
    public void Plus()
    {
        if(Count_num < 99)
        Count_num++;

        Count.text = Count_num.ToString();
        cal.Calculation();
    }
}
