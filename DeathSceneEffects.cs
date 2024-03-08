using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EvolveGames;

public class DeathSceneEffects : MonoBehaviour
{

    public bool deathScenePlay = false;
    Image blackScreen;

    bool noFadesDoneYet = true;
    bool fadedToBlack1 = false;
    bool fadedBackToColour1 = false;
    bool fadedToBlack2 = false;
    bool fadedBackToColour2 = false;
    bool fadedToBlack3 = false;
    bool fadedBackToColour3 = false;

    GameObject playerController;
    PlayerController playerControllerScript;

    GameObject deathCamera;
    GameObject playerCamera;
    GameObject monster;

    Light[] deathScenePointLightsIntial = new Light[2];
    Light monsterLight;

    public AudioSource[] nestLampFlickerSounds = new AudioSource[13];

    AudioSource chaseBackgroundMusic;
    AudioSource screamSound;
    AudioSource biteSound;
    AudioSource horrorSounds;

    bool horrorSoundsPlayed = false;
    bool accDeathSoundsPlayed = false;

    GameObject deathScreen;

    ItemInteractController ItemScript;

    GameObject hospitalCamera;

    bool allItemsGaimed;
    int survivalSceneStage = 1;

    bool effectsDone = false;
    bool effectsDone2 = false;
    bool effectsDone3 = false;

    GameObject survivalScene;
    GameObject survivalInGameScene;

    AudioSource[] allAudioSources;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen = GameObject.Find("Black Screen").GetComponent<Image>();
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 0.0f);

        playerController = GameObject.Find("PlayerController");
        playerControllerScript = playerController.GetComponent<PlayerController>();

        deathCamera = GameObject.Find("DeathCam");
        playerCamera = GameObject.Find("PlayerCamera");

        deathCamera.GetComponent<Camera>().enabled = false;
        deathCamera.GetComponent<AudioListener>().enabled = false;

        monster = GameObject.Find("Wendigo2");
        monster.SetActive(false);

        for (int i = 0; i < 2; i++)
        {
            deathScenePointLightsIntial[i] = GameObject.Find("DeathScenePointLight " + i).GetComponent<Light>();
        }
        monsterLight = GameObject.Find("Monster Light").GetComponent<Light>();
        monsterLight.enabled = false;

        for (int i = 0; i < 13; i++)
        {
            nestLampFlickerSounds[i] = GameObject.Find("Nest Lamp 1 (" + i + ")").GetComponent<AudioSource>();
        }

        chaseBackgroundMusic = GameObject.Find("Final Background Music Object").GetComponent<AudioSource>();
        screamSound = GameObject.Find("ScreamSoundObject").GetComponent<AudioSource>();
        biteSound = GameObject.Find("MonsterBiteSoundObject").GetComponent<AudioSource>();
        horrorSounds = GameObject.Find("HorrorNoiseSoundObject").GetComponent<AudioSource>();

        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);

        ItemScript = playerController.GetComponent<ItemInteractController>();
        hospitalCamera = GameObject.Find("Hospital Camera");
        hospitalCamera.SetActive(false);

        survivalScene = GameObject.Find("SurvivalScene");
        survivalScene.SetActive(false);
        survivalInGameScene = GameObject.Find("Surival Epilogue Scene");
        survivalInGameScene.SetActive(false);


        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    // Update is called once per frame
    void Update()
    {
        allItemsGaimed = (ItemScript.Item1Gained && ItemScript.Item2Gained && ItemScript.Item3Gained);
        if (deathScenePlay)
        {
            if (noFadesDoneYet)
            {
                if (blackScreen.color.a < 255 && !fadedToBlack1)
                {
                    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a + 100.0f);
                }
                if (blackScreen.color.a >= 255)
                {
                    fadedToBlack1 = true;
                    for (int i = 0; i < 13; i++)
                    {
                        nestLampFlickerSounds[i].enabled = false;
                    }
                }
                if (fadedToBlack1) //transport player to nest
                {
                    playerController.GetComponent<PlayerController>().enabled = false;
                    playerCamera.GetComponent<Camera>().enabled = false;
                    playerCamera.GetComponent<AudioListener>().enabled = false;
                    deathCamera.GetComponent<Camera>().enabled = true;
                    deathCamera.GetComponent<AudioListener>().enabled = true;
                    survivalInGameScene.SetActive(true);

                    if (!horrorSoundsPlayed)
                    {
                        horrorSounds.Play();
                        horrorSoundsPlayed = true;
                    }

                    playerController.GetComponent<AudioSource>().enabled = false;
                    chaseBackgroundMusic.enabled = false;
                    if (blackScreen.color.a > 0)
                    {
                        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a - 100.0f);
                    }

                    if (blackScreen.color.a <= 0)
                    { //if we've faded to black and come back
                        StartCoroutine(WaitThenEndNoFadesDone(0.5f));
                    }
                }
            }

            if (fadedBackToColour1) //let player see nest
            {
                if (blackScreen.color.a < 255 && !fadedToBlack2)
                {
                    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a + 100.0f);
                }
                if (blackScreen.color.a >= 255)
                {
                    fadedToBlack2 = true;
                }
                if (fadedToBlack2)
                {
                    if (blackScreen.color.a > 0)
                    {
                        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a - 100.0f);
                    }

                    if (blackScreen.color.a <= 0)
                    { //if we've faded to black and come back
                        fadedBackToColour2 = true;
                        fadedBackToColour1 = false;
                    }
                }
            }

            if (fadedBackToColour2) //then play acc dies.
            {
                if (!fadedToBlack3)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        deathScenePointLightsIntial[i].enabled = false;
                    }

                    if (!accDeathSoundsPlayed)
                    {
                        screamSound.Play();
                        StartCoroutine(WaitThenPlayBite(0.1f));
                        accDeathSoundsPlayed = true;
                    }

                    monsterLight.enabled = true;
                    monster.SetActive(true);
                    Animator anim = monster.GetComponent<Animator>();
                    anim.SetBool("shouldBite", true);

                    if (blackScreen.color.a < 255 && !fadedToBlack3)
                    {
                        if (!effectsDone)
                        {
                            horrorSounds.Stop();
                            effectsDone = true;
                        }
                        StartCoroutine(WaitThenFadeToBlack(0.3f));
                    }
                    if (blackScreen.color.a >= 1.0f && !fadedToBlack3)
                    {
                        if (!allItemsGaimed) // if the player doesn't have all the items, they simply die.
                        {
                            if (!effectsDone2)
                            {
                                StartCoroutine(WaitThenShowDeathScreen(0.2f));
                                effectsDone2 = true;
                            }
                        }
                        else
                        {
                            if (!effectsDone2)
                            {
                                StartCoroutine(WaitThenPlayHeartMonitorSound(2.0f));
                                StartCoroutine(WaitThenStartEpilogue(3.5f));
                                effectsDone2 = true;
                            }
                        }
                    }
                }
            } // fade back to colour 2 ends


            if (fadedToBlack3 && allItemsGaimed)
            {
                if (blackScreen.color.a > 0)
                {
                    if (!effectsDone3)
                    {
                        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 1.0f);
                        effectsDone3 = true;
                    }
                    setNewGradualAlphaValue(blackScreen, -0.2f);
                }

                if (blackScreen.color.a <= 0.5)
                { //if we've faded to black and come back
                    fadedBackToColour3 = true;
                    fadedBackToColour2 = false;
                }
            }
        } // end of if death scene play

        if (fadedBackToColour3)
        {
            if (survivalSceneStage == 1)
            {
                setNewGradualAlphaValue(blackScreen, 0.4f); // black screen comes back
                if (blackScreen.color.a > 1.0f)
                {
                    survivalSceneStage = 2;
                }
            }
            else if (survivalSceneStage == 2)
            {
                setNewGradualAlphaValue(blackScreen, -0.4f); // black screen goes away
                if (blackScreen.color.a <= 0.5f)
                {
                    survivalSceneStage = 3;
                }
            }
            else if (survivalSceneStage == 3)
            {
                setNewGradualAlphaValue(blackScreen, 0.4f); // black screen comes back
                if (blackScreen.color.a > 1.0f)
                {
                    survivalSceneStage = 4;
                }
            }
            else if (survivalSceneStage == 4)
            {
                if (blackScreen.color.a < 1.0f)
                {
                    setNewGradualAlphaValue(blackScreen, 1.0f);
                }

                survivalScene.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        } // end of if fadedBackToColour3

        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     allItemsGaimed = true;
        // }

    }

    IEnumerator WaitThenEndNoFadesDone(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        fadedBackToColour1 = true;
        noFadesDoneYet = false;
    }

    IEnumerator WaitThenFadeToBlack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 1.0f);
    }

    IEnumerator WaitThenPlayBite(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        biteSound.Play();
    }

    IEnumerator WaitThenStartEpilogue(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        for (int i = 0; i < allAudioSources.Length; i++)
        {
            allAudioSources[i].Pause();
        }
        fadedToBlack3 = true;
    }

    IEnumerator WaitThenPlayHeartMonitorSound(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        deathCamera.GetComponent<Camera>().enabled = false;
        deathCamera.GetComponent<AudioListener>().enabled = false;
        hospitalCamera.SetActive(true);
        hospitalCamera.GetComponent<AudioSource>().Play();
    }

    IEnumerator WaitThenShowDeathScreen(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        deathScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void setNewGradualAlphaValue(Image image, float rate)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (rate * Time.deltaTime));
    }
}
