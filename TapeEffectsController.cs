using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeEffectsController : MonoBehaviour
{
    GameObject doorToRoom2;
    GameObject doorToBackroom;
    public bool[] tapePlayed = new bool[3];
    GameObject mainRoomTape;
    GameObject tape1;
    GameObject targetPoint;
    GameObject[] controlSystems = new GameObject[16];
    AudioSource lockSound;
    AudioSource appearSound;
    AudioSource heartbeatSound;

    bool effectsDone = false;
    bool effectsDone2 = false;
    bool effectsDone3 = false;

    Light[] mainRoomLights = new Light[6];
    Light[] tapeLights = new Light[3];

    bool heartStopped = false;
    // Start is called before the first frame update
    void Start()
    {
        doorToRoom2 = GameObject.Find("door with animations 3 (to Side Room 2 ) ");
        doorToBackroom = GameObject.Find("door with animations 4 (to back room )");
        doorToRoom2.tag = "Locked";
        doorToBackroom.tag = "Locked";

        tape1 = GameObject.Find("tape recorder 1");
        targetPoint = GameObject.Find("target");

        for (int i = 0; i < 16; i++)
        {
            controlSystems[i] = GameObject.Find("Control system " + i);
        }

        lockSound = GameObject.Find("Lock sound object").GetComponent<AudioSource>();
        appearSound = GameObject.Find("Appear sound object").GetComponent<AudioSource>();
        heartbeatSound = GameObject.Find("Heart").GetComponent<AudioSource>();

        for (int i = 0; i < 6; i++)
        {
            mainRoomLights[i] = GameObject.Find("Point Light Main Room " + (i + 1)).GetComponent<Light>();
        }
        for (int i = 0; i < 3; i++)
        {
            tapeLights[i] = GameObject.Find("Tape effects object " + (i + 1)).GetComponent<Light>();
        }

        mainRoomTape = GameObject.Find("tape recorder 3");
        mainRoomTape.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tapePlayed[0])
        { //if first tape played
            if (!effectsDone)
            {
                doorToRoom2.tag = "Openable";
                lockSound.Play();
                for (int i = 0; i < 16; i++)
                {
                    controlSystems[i].transform.LookAt(targetPoint.transform);
                }
                tapeLights[0].enabled = false;
                effectsDone = true;
            }
        }
        if (tapePlayed[1])
        { //if second tape played
            if (!effectsDone2)
            {
                mainRoomTape.SetActive(true);
                appearSound.Play();
                effectsDone2 = true;
                Animator anim = GameObject.Find("Heart").GetComponent<Animator>();
                heartStopped = true;
                anim.SetBool("heartStopped", heartStopped);
                for (int i = 0; i < 6; i++)
                {
                    mainRoomLights[i].intensity = 0.1f;
                }
                tapeLights[1].enabled = false;
            }
        }
        if (tapePlayed[2])
        { //if third tape played
            if (!effectsDone3)
            {
                doorToBackroom.tag = "Openable";
                lockSound.Play();
                tapeLights[2].enabled = false;
                effectsDone3 = true;
            }
        }
    }
}
