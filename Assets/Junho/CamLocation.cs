using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLocation : MonoBehaviour
{
    public bool isplayer = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isplayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isplayer = false;
        }
    }
}
