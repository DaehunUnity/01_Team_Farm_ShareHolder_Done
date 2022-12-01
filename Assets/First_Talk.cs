using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Talk : MonoBehaviour
{
    public GameObject mailBox;
    public ShowMeTheMoney smtm;
    public bool is_First = true;
    public bool is_Talk = false;

    private void Update()
    {
        if (is_Talk)
        {
            if (is_First)
            {
                if (gameObject.activeSelf)
                {
                    StartCoroutine(give_Money());
                    is_First = false;
                }
            }
        }
    }
    
    IEnumerator give_Money()
    {        
            yield return new WaitForSeconds(1f);
        
            mailBox.SetActive(true);
    }

    public void Read_Letter()
    {
        smtm.Money += 1000;
        smtm.ShowMoney.text = smtm.Money.ToString();
    }
}
