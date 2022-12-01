using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookPlayer : MonoBehaviour
{
    public Animator NPC_ani;
    public GameObject target;
    public First_Talk F_Talk;
    private Quaternion start_rot;

    public Vector3 lookPos;

    public GameObject Intro;
    public GameObject Interaction;
    public GameObject panelController;
    public GameObject GameUI;
    public GameObject TradeUI;
    public bool meetPlayer = false;
    public bool captainCheck = false;

    private void Start()
    {
        start_rot = gameObject.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            meetPlayer = true;
            Interaction.SetActive(true);
            NPC_ani.SetTrigger("Greeting");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            StopAllCoroutines();

            lookPos = target.transform.position;
            lookPos.y = gameObject.transform.position.y;

            //transform.LookAt(lookPos);
            Vector3 dir = lookPos - gameObject.transform.position;
            dir.Normalize();

            Quaternion look_rot = Quaternion.LookRotation(dir);
            gameObject.transform.rotation =Quaternion.Lerp(gameObject.transform.rotation,look_rot,Time.deltaTime * 3.0f);
        }
    }
    private void Update()
    {
        if(meetPlayer == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interaction.SetActive(false);
                panelController.SetActive(true);
                Intro.SetActive(true);
                GameUI.SetActive(false);
                TradeUI.SetActive(false);

                if (captainCheck)
                {
                    if(!F_Talk.is_Talk)
                        F_Talk.is_Talk = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Goodbye());

            Intro.SetActive(false);
            Interaction.SetActive(false);
            panelController.SetActive(false);
            GameUI.SetActive(true);
            TradeUI.SetActive(true);
            NPC_ani.SetTrigger("Greeting");

            meetPlayer = false;
        }
    }
    IEnumerator Goodbye()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, start_rot, Time.deltaTime * 3.0f);
            yield return null;
        }
    }
}
