using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMeTheMoney : MonoBehaviour
{
    [Header("UI")]
    public Text ShowMoney;

    [Header("Value")]
    public int Money;    
    public int Radish_SellPrice;
    public int Carrot_SellPrice;
    public int Beat_SellPrice;
    public int AsianCabage_SellPrice;

    void Start()
    {
        Money = 0;
        ShowMoney.text = Money.ToString();
    }
    private void Update()
    {
        ShowMoney.text = Money.ToString();
    }

    public void Sell_Radish()
    {
        Money += Radish_SellPrice;
        ShowMoney.text = Money.ToString();
    }
    public void Sell_Carrot()
    {
        Money += Carrot_SellPrice;
        ShowMoney.text = Money.ToString();
    }
    public void Sell_Beat()
    {
        Money += Beat_SellPrice;
        ShowMoney.text = Money.ToString();
    }
    public void Sell_AsianCabage()
    {
        Money += AsianCabage_SellPrice;
        ShowMoney.text = Money.ToString();
    }
}
