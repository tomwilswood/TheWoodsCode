using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvolveGames;

public class AntiWanderingSystemController : MonoBehaviour
{
    GameObject player;

    AntiWanderingSystemController FirstAntiWanderScript;
    AntiWanderingSystemController NextAntiWanderScript;

    public int currentZoneNumber = 0;

    GameObject nextZone;
    GameObject previousZone;

    public bool lastZone;
    public bool firstZone;

    public bool inThisZone = false;

    bool hasEnteredForFirstTime = false;

    public bool lightNeedsBeOff = false;
    public GameObject lightOfZone;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController");

        FirstAntiWanderScript = GameObject.Find("Anti-Wandering Object 1").GetComponent<AntiWanderingSystemController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        inThisZone = true;
        if (!hasEnteredForFirstTime)
        {
            FirstAntiWanderScript.currentZoneNumber += 1;
            currentZoneNumber = FirstAntiWanderScript.currentZoneNumber;

            if (!lastZone)
            {
                nextZone = GameObject.Find("Anti-Wandering Object " + (currentZoneNumber + 1));
                NextAntiWanderScript = nextZone.GetComponent<AntiWanderingSystemController>();
            }
            if (!firstZone)
            {
                previousZone = GameObject.Find("Anti-Wandering Object " + (currentZoneNumber - 1));
            }

            hasEnteredForFirstTime = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (!lastZone)
        {
            if ((!NextAntiWanderScript.inThisZone && ((!previousZone.activeSelf) || firstZone)) || (lightNeedsBeOff && lightOfZone.GetComponent<Light>().enabled))
            { //if (you're not in the next zone, AND (either the previous zone is not active OR this is the first zone)) OR (the lights in this zone should be off AND the lights are on)
                player.GetComponent<PlayerController>().enabled = false;
                other.gameObject.transform.position = transform.position;
                StartCoroutine(WaitThenEnablePlayer(0.01f));

            }
            inThisZone = false;
            if (NextAntiWanderScript.inThisZone && !(lightNeedsBeOff && lightOfZone.GetComponent<Light>().enabled))
            { //if you're in the next zone AND (either the lights need to be off AND are off, or the lights don't need to be off)
                gameObject.SetActive(false);
            }
        }
        else
        { //if it is the last zone
            gameObject.SetActive(false);
        }


    }

    public IEnumerator WaitThenEnablePlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        player.GetComponent<PlayerController>().enabled = true;

    }
}
