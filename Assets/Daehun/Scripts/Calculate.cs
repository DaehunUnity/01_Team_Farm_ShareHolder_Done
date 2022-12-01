using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    [Header("Link Object")]
    public Count_Button[] Counts;
    public Text[] Prices;
    public Text Total;
    public GameObject message;
    public haveSeed haveSeed;
    private ShowMeTheMoney nowMoney;
    
    [Header("Menu")]
    public int Radish_Price;
    public int Carrot_Price;
    public int Beat_Price;
    public int AsianCabage_Price;

    [Header("Calculate")]
    public int cal;

    private void OnEnable()
    {
        Total.text = "0";
        Prices[0].text = Radish_Price.ToString();
        Prices[1].text = Carrot_Price.ToString();
        Prices[2].text = Beat_Price.ToString();
        Prices[3].text = AsianCabage_Price.ToString();
        //Money = 현재 가지고 있는 돈
    }
    private void Start()
    {
        nowMoney = FindObjectOfType<ShowMeTheMoney>();
    }

    public void Calculation()
    {
        
        cal = (Radish_Price * Counts[0].Count_num) + (Carrot_Price * Counts[1].Count_num) + (Beat_Price * Counts[2].Count_num) + (AsianCabage_Price * Counts[3].Count_num);
        Debug.Log(cal);
        Total.text = cal.ToString();
    }

    public void Buy()
    {
        if(cal > nowMoney.Money)
        {
            StartCoroutine(warning());
        }
        else if(cal < nowMoney.Money)
        {
            nowMoney.Money -= cal;

            haveSeed.Set_Seed(0, Counts[0].Count_num);
            haveSeed.Set_Seed(1, Counts[1].Count_num);
            haveSeed.Set_Seed(2, Counts[2].Count_num);
            haveSeed.Set_Seed(3, Counts[3].Count_num);
        }
    }

    IEnumerator warning()
    {
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        message.gameObject.SetActive(false);
    }
}