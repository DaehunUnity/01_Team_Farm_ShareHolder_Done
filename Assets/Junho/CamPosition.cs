using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum camName { 
player,
captain,
store,
place,
inside,
start
}
public class CamPosition : MonoBehaviour
{
    public GameObject[] cams;
    public GameObject horse;
    public GameObject cow;
    public GameObject pig;
    public GameObject store;

    public GameObject CaptainPanel;
    public GameObject StorePanel;
    public GameObject player;
    public bool First_Time = true;

    private void Start()
    {
        StartCoroutine(startView());
    }

    // Update is called once per frame
    void Update()
    {
        if(!First_Time)
        onPlace();
    }

    void onPlace()
    {
        if(horse.GetComponent<CamLocation>().isplayer || cow.GetComponent<CamLocation>().isplayer || pig.GetComponent<CamLocation>().isplayer)
        {
            Oncamara((int)camName.place);
        }
        else if (store.GetComponent<CamLocation>().isplayer)
        {
            if (StorePanel.activeSelf)
            {
                Oncamara((int)camName.store);
                player.SetActive(false);
            }
            else
            {
                player.SetActive(true);
                Oncamara((int)camName.inside);
            }
        }
        else if (CaptainPanel.activeSelf)
        {
            Oncamara((int)camName.captain);
            player.SetActive(false);
        }
        else
        {
            player.SetActive(true);
            Oncamara((int)camName.player);
        }
       
    }

    void Oncamara(int num)
    {
        for(int i =0; i < cams.Length; i++)
        {

            if (num == i)
                cams[i].SetActive(true);
            else
                cams[i].SetActive(false);
        }
    }
    IEnumerator startView()
    {
        Oncamara((int)camName.start);
        yield return new WaitForSeconds(3f);
        Oncamara((int)camName.player);
        First_Time = false;
    }
}
