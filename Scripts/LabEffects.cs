using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabEffects : MonoBehaviour
{

    GameObject Player;
    ItemInteractController ItemScript;
    StartingPointEffects2 StartingPointEffectsScript;
    LabFinalDoorPlayerLeftDetection PlayerLeftScript;
    Light ItemLight;

    GameObject LightsOnObjectGroup;
    GameObject LightsOffObjectGroup;

    bool effectsDone = false;
    bool effectsDone2 = false;
    bool effectsDone3 = false;
    bool effectsDone4 = false;

    GameObject LightPath3Light1;
    GameObject finalDoor;

    bool doorShouldOpen = false;
    bool doorShouldClose = false;

    AudioSource intialBackgroundMusic;
    AudioSource secondBackgroundMusic;
    AudioSource monsterScreachSound;
    AudioSource heartbeatSound;

    AudioSource doorCloseSound;

    OptoinsMenuController optionsScript;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerController");
        ItemScript = Player.GetComponent<ItemInteractController>();
        StartingPointEffectsScript = Player.GetComponent<StartingPointEffects2>();
        PlayerLeftScript = GameObject.Find("Door Close Script Object").GetComponent<LabFinalDoorPlayerLeftDetection>();

        ItemLight = GameObject.Find("Item light 2").GetComponent<Light>();

        LightsOnObjectGroup = GameObject.Find("Lab Lights - On");
        LightsOffObjectGroup = GameObject.Find("Lab Lights - Off");
        LightsOnObjectGroup.SetActive(true);
        LightsOffObjectGroup.SetActive(false);

        LightPath3Light1 = GameObject.Find("LP3 Light 1");
        LightPath3Light1.SetActive(false);

        finalDoor = GameObject.Find("door with animations 5 (to exit)");

        intialBackgroundMusic = GameObject.Find("PlayerController").GetComponent<AudioSource>();
        secondBackgroundMusic = GameObject.Find("Second Background Music Object").GetComponent<AudioSource>();
        monsterScreachSound = GameObject.Find("Monster Scream Object").GetComponent<AudioSource>();
        heartbeatSound = GameObject.Find("Archive Effects Object").GetComponent<AudioSource>();
        doorCloseSound = GameObject.Find("door close sound object").GetComponent<AudioSource>();
        optionsScript = GameObject.Find("Options Menu").GetComponent<OptoinsMenuController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ItemScript.Item2Gained)
        {
            if (!effectsDone4)
            {
                secondBackgroundMusic.volume = 0;
                effectsDone4 = true;
            }
            if (!effectsDone3)
            {
                if (secondBackgroundMusic.volume < (1 * optionsScript.volumeSlider.value))
                {
                    intialBackgroundMusic.volume -= 0.5f * Time.deltaTime;
                    secondBackgroundMusic.Play();
                    secondBackgroundMusic.volume += 0.5f * Time.deltaTime;
                }

                if (heartbeatSound.volume < (0.8 * optionsScript.volumeSlider.value))
                {
                    heartbeatSound.volume += 0.5f * Time.deltaTime;
                }

                if ((heartbeatSound.volume >= (0.8 * optionsScript.volumeSlider.value)) && (secondBackgroundMusic.volume >= (1 * optionsScript.volumeSlider.value)))
                {
                    effectsDone3 = true;
                }
            }

            if (!effectsDone)
            {
                ItemLight.enabled = false;
                LightsOnObjectGroup.SetActive(false);
                LightsOffObjectGroup.SetActive(true);

                LightPath3Light1.SetActive(true);
                StartCoroutine(Player.GetComponent<ItemInteractController>().WaitThenTextFadeIn(6, StartingPointEffectsScript.FollowText, 2.0f));
                StartCoroutine(Player.GetComponent<ItemInteractController>().WaitThenTextFadeOut(9, StartingPointEffectsScript.FollowText, 1.0f));

                Animator anim = finalDoor.GetComponent<Animator>();
                doorShouldOpen = true;
                doorShouldClose = false;
                anim.SetBool("DoorShouldOpen", doorShouldOpen);
                doorShouldOpen = false;
                anim.SetBool("DoorShouldClose", doorShouldClose);
                finalDoor.GetComponent<AudioSource>().Play();

                effectsDone = true;
            }

            if (PlayerLeftScript.playerHasLeft)
            {
                if (!effectsDone2)
                {
                    Animator anim = finalDoor.GetComponent<Animator>();
                    doorShouldClose = true;
                    anim.SetBool("DoorShouldClose", doorShouldClose);
                    doorShouldOpen = false;
                    anim.SetBool("DoorShouldOpen", doorShouldOpen);
                    monsterScreachSound.Play();
                    doorCloseSound.Play();
                    effectsDone2 = true;
                }
            }

        }
    }
}
