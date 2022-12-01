using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class haveSeed : MonoBehaviour
{
    public int[] SeedNums;

    public Text[] SeedNums_Texts;

    private void Start()
    {
        SeedNums_Texts[0].text = SeedNums[0].ToString();
        SeedNums_Texts[1].text = SeedNums[1].ToString();
        SeedNums_Texts[2].text = SeedNums[2].ToString();
        SeedNums_Texts[3].text = SeedNums[3].ToString();
    }

    public void Set_Seed(int VegetableNum, int num)
    {
        SeedNums[VegetableNum] += num;
        SeedNums_Texts[VegetableNum].text = SeedNums[VegetableNum].ToString();        
    }
    public void CountSeed(int VegetableNum)
    {
        SeedNums[VegetableNum]--;
        SeedNums_Texts[VegetableNum].text = SeedNums[VegetableNum].ToString();
    }
}
