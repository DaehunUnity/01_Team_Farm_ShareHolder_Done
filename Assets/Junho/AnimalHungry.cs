using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animal{
    Chiken,
    Pig,
    Cow,
    Horse
}
public class AnimalHungry : MonoBehaviour
{
    public int health = 100;
    public float deley;
    
    // Update is called once per frame
    void Update()
    {
        deley += Time.deltaTime;
        Hungry();
    }
    void Hungry()
    {
       if(deley >= 3.0f)
        {
            health -= 10;
            deley = 0;
        }
    }
}
