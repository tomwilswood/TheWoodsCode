using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EvolveGames;

public class OptoinsMenuController : MonoBehaviour
{
    public bool inOptions = false; //if player is in the options menu

    GameObject optionsMenuBody;
    Slider brightnessSlider;
    public Slider volumeSlider;
    GameObject player;
    public List<AudioSource> audioToBeUnpaused = new List<AudioSource>();

    public bool easyModeEnabled = false;
    Toggle easyModeToggle;

    AudioSource[] allAudioSources;

    int numberOfAudioSources;

    float[] intialVolumes;

    bool optionsLoaded;

    StartMenuController startMenuScript;
    DeathSceneEffects deathSceneScript;
    InventoryController inventoryScript;


    // Start is called before the first frame update
    void Start()
    {
        brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        optionsMenuBody = GameObject.Find("Options Menu Main Body");
        easyModeToggle = GameObject.Find("Easy Mode Toggle").GetComponent<Toggle>();

        player = GameObject.Find("PlayerController");

        optionsMenuBody.SetActive(false);

        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        numberOfAudioSources = allAudioSources.Length;

        intialVolumes = new float[numberOfAudioSources];

        for (int i = 0; i < numberOfAudioSources; i++)
        {
            intialVolumes[i] = allAudioSources[i].volume;
        }
        startMenuScript = GameObject.Find("Start Menu").GetComponent<StartMenuController>();
        deathSceneScript = GameObject.Find("Death Scene Effects Object").GetComponent<DeathSceneEffects>();
        inventoryScript = player.GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientIntensity = brightnessSlider.value;
        easyModeEnabled = easyModeToggle.isOn;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inOptions)
            { //close options menu
                CloseOptionsMenu();
            }
            else
            { // open options menu 
                if (!startMenuScript.inStartMenu && !deathSceneScript.deathScenePlay && !inventoryScript.inInventory)
                {
                    inOptions = true;
                }
                if (inventoryScript.inInventory)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    inventoryScript.inInventory = false;
                }

            }
        }

        if (inOptions)
        {
            if (!optionsLoaded)
            {
                optionsMenuBody.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (!startMenuScript.inStartMenu)
                {
                    PauseActiveAudioSources();
                }
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponentInChildren<HeadBob>().enabled = false;
                optionsLoaded = true;
            }
        }
    }

    void PauseActiveAudioSources()
    {
        foreach (AudioSource individualAudioSource in allAudioSources)
        {
            if (individualAudioSource.isPlaying)
            {
                individualAudioSource.Pause();
                audioToBeUnpaused.Add(individualAudioSource);
            }
        }
    }

    void PlayPausedAudioSources()
    {
        foreach (AudioSource individualAudioSource in audioToBeUnpaused)
        {
            individualAudioSource.Play();
        }
        audioToBeUnpaused.Clear();
    }

    public void UpdateVolume()
    {
        for (int i = 0; i < numberOfAudioSources; i++)
        {
            allAudioSources[i].volume = intialVolumes[i] * volumeSlider.value;
        }
    }

    public void CloseOptionsMenu()
    {
        optionsMenuBody.SetActive(false);
        inOptions = false;
        optionsLoaded = false;
        if (!startMenuScript.inStartMenu && !startMenuScript.inControls && !startMenuScript.goneOptionsToControls)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayPausedAudioSources();
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponentInChildren<HeadBob>().enabled = true;
        }
        else
        {
            if (startMenuScript.inStartMenu && !startMenuScript.goneOptionsToControls)
            {
                startMenuScript.startMenuMainBody.SetActive(true);
            }
        }

    }
}
