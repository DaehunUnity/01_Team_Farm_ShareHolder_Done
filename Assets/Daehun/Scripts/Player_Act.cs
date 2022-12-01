using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player_Act : MonoBehaviour
{
    #region Object
    [Header("Link Object")]
    public GameObject player;
    public Animator animator;
    public MyPlayerMove playerMove;
    public ShowMeTheMoney smtm;
    public GameObject Parent;
    public haveSeed haveSeed;
    public GameObject Basket;
    public GameObject HarvestSpot;
    
    [Header("Tool")]
    public GameObject Rake;
    public GameObject Sprinkling_Can;
    public GameObject Baguni;
    public GameObject Basket_Can;
    #endregion

    public GameObject slider;
    public GameObject target;
    public GameObject whatToDo;
    public GameObject[] seed;
    public InteractObject targetComponent;
    public SelectSeed SeedNum;
    private int Seeds;
        
    [SerializeField]
    [Range(0f,100f)]
    private float Have_Water;
    
    //Sc_bat target_bat;
    public PlantData[] plantData;
    public float Value;
    public bool On_bat = false;
    public bool On_Water = false;

    [Header("Particle")]
    public ParticleSystem Dirt;
    public ParticleSystem Watering;
    public ParticleSystem Star;
    bool Star_Check = false;

    [Header("Sounds")]
    public AudioSource RakeSound;
    public AudioSource SprinklingSound;
    public AudioSource ReceiveWaterSound;
    public AudioSource HarvestSound;

    private void Start()
    {        
        Dirt.Stop();
        Watering.Stop();
        Star.Stop();
    }

    private void Update()
    {
        if(!Star_Check)
        {
            Star.Stop();
        }

        if(playerMove.Acting == false)
        {
            if (GetInteractCheck())
            {
                if (target != null)
                {
                    if (targetComponent.Whatis == "밭")
                    {
                        Farming_Routine();
                    }
                }
                else if (On_Water == true)
                {
                    StartCoroutine(Receive_Water());
                }
            }
        }
            
        if(On_bat == true)
        {
            whatToDo.SetActive(true);
            if (target != null)
            {
                if (targetComponent.Whatis == "밭")
                {
                    targetComponent = target.GetComponent<InteractObject>();
                    whatToDo.GetComponent<WhatToDo>().UI_Interaction(targetComponent.State);
                }
            }
            else
            {
                whatToDo.GetComponent<WhatToDo>().UI_Interaction(3);
                target = null;
            }
        }
        else
        {
            whatToDo.SetActive(false);
        }
        Have_Water = slider.GetComponent<Slider>().value;
        
    }


    bool GetInteractCheck() 
    {
        bool is_check = false;

        if (Input.GetKeyDown(KeyCode.F))
            is_check = true;

        return is_check;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bat"))
        {
            On_bat = true;
            if (other.gameObject.TryGetComponent<InteractObject>(out targetComponent))
                target = other.gameObject;
        }

        if (other.gameObject.CompareTag("Water"))
        {
            On_Water = true;
            Debug.Log("Water"+On_Water);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("bat"))
        {
            On_bat = false;
            target = null;
            targetComponent = null;
        }

        if(other.gameObject.CompareTag("Water"))
        {
            On_Water = false;
            Debug.Log("WaterOut"+On_Water);
        }
    }    

    void Farming_Routine()
    {
        if (targetComponent.State == 0)
        {
            Debug.Log("농사해!");

            StartCoroutine(Plowing());

            targetComponent.SetState(1);
        }
        else if (targetComponent.State == 1)
        {
            Debug.Log("심어심어!");
            StartCoroutine(Plant());

            if(target.GetComponent<Sc_bat>().myPlant != null)
                targetComponent.SetState(2);
        }
        else if (targetComponent.State == 2)
        {
            float RequireWater = target.GetComponent<Sc_bat>().myPlant.Require_water;
            if (Have_Water >= RequireWater) //물이 부족 - 물주자
            {
                target.GetComponent<Sc_bat>().now_water += RequireWater;
                Have_Water -= 20f;
                Debug.Log("뿌려뿌려!");
                StartCoroutine(Water());
                slider.GetComponent<Slider>().value = Have_Water;
            }
           
        }
        else if (targetComponent.State == 4)
        {
            StartCoroutine(Harvest());

            targetComponent.SetState(0);
            
        }
    }

    #region Act_Setting
    private IEnumerator Plowing()
    {
        Dirt.transform.position = target.transform.position + transform.up * 1;
        playerMove.Acting = true;
        Lookbat(target.transform);
        animator.SetBool("Plowing", true);
        Rake.SetActive(true);
        Dirt.Play();
        RakeSound.Play();
        
        yield return new WaitForSeconds(2.0f);

        RakeSound.Stop();
        animator.SetBool("Plowing", false);
        Rake.SetActive(false);        
        Dirt.Stop();

        yield return new WaitForSeconds(0.5f);
        
        playerMove.Acting = false;
    }

    private IEnumerator Water()
    {
        playerMove.Acting = true;
        Lookbat(target.transform);
        animator.SetBool("Water", true);
        Sprinkling_Can.SetActive(true);
        Watering.gameObject.SetActive(true);
        Watering.Play();
        SprinklingSound.Play();

        yield return new WaitForSeconds(2.0f);

        SprinklingSound.Stop();
        Watering.Stop();
        Watering.gameObject.SetActive(false);
        animator.SetBool("Water", false);
        Sprinkling_Can.SetActive(false);
        Sc_bat nbat = targetComponent.GetComponent<Sc_bat>();

        if (nbat.now_water >= nbat.max_water)
            targetComponent.SetState(3);

        yield return new WaitForSeconds(1f);

        playerMove.Acting = false;
    }
    
    private IEnumerator Receive_Water()
    {
        playerMove.Acting = true;
        
        animator.SetBool("Refill", true);
        Basket_Can.SetActive(true);
        ReceiveWaterSound.Play();
        Basket.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        Basket.SetActive(false);
        ReceiveWaterSound.Stop();
        Have_Water = 100;
        slider.GetComponent<Slider>().value = Have_Water;
        Basket_Can.SetActive(false);
        animator.SetBool("Refill", false);

        yield return new WaitForSeconds(1f);

        playerMove.Acting = false;
    }

    private IEnumerator Plant()
    {        
        if (Check_Seed(SeedNum.num) > 0)
        {
            playerMove.Acting = true;
            Lookbat(target.transform);
            Baguni.SetActive(true);
            animator.SetBool("Plant", true);

            target.GetComponent<Sc_bat>().myPlant = plantData[SeedNum.num];
            target.GetComponent<Sc_bat>().max_water = target.GetComponent<Sc_bat>().myPlant.Require_water;

            target.GetComponent<Sc_bat>().SetPlant(plantData[SeedNum.num]);

            haveSeed.CountSeed(SeedNum.num);
            

            yield return new WaitForSeconds(2.5f);

            animator.SetBool("Plant", false);
            Baguni.SetActive(false);

            playerMove.Acting = false;
        }
    }

    private IEnumerator Harvest()   
    {
        Star_Check = true;
        GameObject Vegetable = target.GetComponent<Sc_bat>().Plant_Object;
        Vegetable.transform.parent = HarvestSpot.transform;
        playerMove.Acting = true;
        Lookbat(target.transform);
        animator.SetBool("Harvest", true);
        target.GetComponent<Sc_bat>().SetClear();
        string GetName = target.GetComponent<Sc_bat>().myPlant.plantname;
        HarvestSound.Play();
        Vegetable.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        HarvestSound.Stop();
        Vegetable.transform.localPosition = Vector3.zero;
        Vegetable.transform.rotation = Quaternion.EulerRotation(0, 0, 0);
        Vegetable.SetActive(true);
        Star.Play();
        

        yield return new WaitForSeconds(0.5f);
        Star.Stop();
        Destroy(Vegetable);
        animator.SetBool("Harvest", false);
        GetPrice(GetName);
        playerMove.Acting = false;
        Star_Check = false;
    }

    #endregion
    void GetPrice(string PlantName)
    {
        if(PlantName != null)
        {            
            if(PlantName == "무")
                smtm.Sell_Radish();
            else if (PlantName == "당근")
                smtm.Sell_Carrot();
            else if (PlantName == "비트")
                smtm.Sell_Beat();
            else if (PlantName == "배추")
                smtm.Sell_AsianCabage();
        }
    }

    void Lookbat(Transform target)
    {
        Vector3 LookPos = target.transform.position;
        LookPos.y = gameObject.transform.position.y;
        Parent.transform.LookAt(LookPos);
    }

    int Check_Seed(int DataNum)
    {
        int data = haveSeed.SeedNums[DataNum];
        
        return data;
    }
    #region trash
    //[Header("Aniamtion")]
    //[SerializeField]
    //private bool inarea = false;
    //[SerializeField]
    //private bool working = false;
    //[SerializeField]
    //private bool farming = false;

    //private void OnAnimatorIK(int layerIndex)//도구에 
    //{
    //    if(Equipment)
    //    {
    //        farmequipment.transform.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);
    //
    //        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
    //        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
    //
    //        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandMount.position);
    //        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandMount.rotation);
    //
    //        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
    //        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
    //
    //        animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandMount.position);
    //        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandMount.rotation);
    //    }
    //}

    //state = State.farming;
    //애니메이션을 진행하는 내용
    //Idle_Animation
    //state = State.idle;
    #endregion
}
