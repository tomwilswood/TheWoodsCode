using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvolveGames;

public class NestEntranceSoundEffects : MonoBehaviour
{

    AudioSource whisperingSounds;
    AudioSource RUNWhisper;
    PlayerController playerControllerScript;
    AudioSource heartbeatSound;
    bool playerEntered = false;
    AudioSource secondBackgroundMusic;
    AudioSource finalBackgroundMusic;

    GameObject entranceBarrier;

    bool effectsDone = false;
    bool effectsDone2 = false;

    OptoinsMenuController optionsScript;
    // Start is called before the first frame update
    void Start()
    {
        whisperingSounds = GameObject.Find("Overall susurration (whispering) sound object").GetComponent<AudioSource>();
        RUNWhisper = GameObject.Find("RUN Whisper Object").GetComponent<AudioSource>();

        playerControllerScript = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        heartbeatSound = GameObject.Find("Archive Effects Object").GetComponent<AudioSource>();

        secondBackgroundMusic = GameObject.Find("Second Background Music Object").GetComponent<AudioSource>();
        finalBackgroundMusic = GameObject.Find("Final Background Music Object").GetComponent<AudioSource>();
        finalBackgroundMusic.volume = 0;

        entranceBarrier = GameObject.Find("barrier to start of path");
        entranceBarrier.SetActive(false);

        optionsScript = GameObject.Find("Options Menu").GetComponent<OptoinsMenuController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerEntered)
        {
            if (heartbeatSound.volume < (1 * optionsScript.volumeSlider.value))
            {
                heartbeatSound.volume += 0.5f * Time.deltaTime;
            }

            if (finalBackgroundMusic.volume < (1 * optionsScript.volumeSlider.value))
            {
                secondBackgroundMusic.volume -= 0.5f * Time.deltaTime;
                if (!effectsDone2)
                {
                    finalBackgroundMusic.Play();
                    effectsDone2 = true;
                }
                finalBackgroundMusic.volume += 0.5f * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!effectsDone)
        {
            playerEntered = true;
            RUNWhisper.Play();
            StartCoroutine(WaitThenStopWhisperingAndRun(1.0f));
            entranceBarrier.SetActive(true);
            effectsDone = true;
        }
    }

    IEnumerator WaitThenStopWhisperingAndRun(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        whisperingSounds.enabled = false;
        playerControllerScript.RuningSpeed = 6;
    }
}
