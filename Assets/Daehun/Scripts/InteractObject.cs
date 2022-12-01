using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public int State = 0;
    public string Whatis = "";

    public virtual void SetState(int num)
    {
        State = num;
    }
}
