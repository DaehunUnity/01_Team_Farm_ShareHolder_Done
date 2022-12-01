using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.AI;

using System.Linq;

public enum Animalname
{
    Chicken,
    Cow,
    Fox,
    Horse,
    Pig
}
public enum AI_STATE
{
    Walk = 2081823275,
    Idle = 1463555229,
    trigger = 133321
}
public class AnimalAi : MonoBehaviour
{
    public AI_STATE CurrentState = AI_STATE.Walk;

    private Animator ThisAnimator = null;

    private NavMeshAgent ThisAgent = null;

    private Transform[] WayPoints = null;

    public string Watpointname = "chicken";

    [SerializeField]
    private Transform nowWaypoint;

    private float actWait = 3;


    public void Awake()
    {
        GameObject[] Waypoints = GameObject.FindGameObjectsWithTag(Watpointname);
         
        WayPoints = (from GameObject GO in Waypoints select GO.transform).ToArray();

        ThisAnimator = GetComponent<Animator>();

        ThisAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IdleIdle());
    }

    // Update is called once per frame
    void Update()
    {
        ThisAnimator.SetFloat("Walk", ThisAgent.velocity.magnitude / ThisAgent.speed);
    }

    public IEnumerator WalkWalk()
    {
        CurrentState = AI_STATE.Walk;

        nowWaypoint = WayPoints[Random.Range(0, WayPoints.Length)];

        ThisAgent.SetDestination(nowWaypoint.position);

        ThisAgent.isStopped = false;

        while (CurrentState == AI_STATE.Walk)
        {


            if (Vector3.Distance(transform.position, nowWaypoint.position) <= 3)
            {
                StartCoroutine(IdleIdle());
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator IdleIdle()
    {
        CurrentState = AI_STATE.Idle;

        ThisAgent.isStopped = true;

        ThisAnimator.SetTrigger("Idle");
        actWait = 2;


        while (CurrentState == AI_STATE.Idle)
        {
            yield return new WaitForSeconds(8f);
            Re_set();
            yield break;

        }
        yield return null;
    }

    /*
    IEnumerator Hi()
    {
        CurrentState = AI_STATE.trigger;
        ThisAgent.isStopped = true;

        ThisAnimator.SetTrigger("Eat");
        actWait = 3;

        while (CurrentState == AI_STATE.trigger)
        {
            yield return new WaitForSeconds(3f);
            Re_set();
            yield break;

        }
    }
    */

    public void Re_set()
    {
        StopAllCoroutines();

        StartCoroutine(WalkWalk());
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(Hi());
        }
    }
    */
}

