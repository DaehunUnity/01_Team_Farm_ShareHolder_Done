using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChange : MonoBehaviour
{
    // Inspector 영역에 표시할 배경음악 이름
    public string bgmName = "";

    private GameObject CamObject;

    void Start()
    {
        CamObject = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            CamObject.GetComponent<BGMController>().PlayBGM(bgmName);
    }
}
