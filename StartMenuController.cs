using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvolveGames;

public class StartMenuController : MonoBehaviour
{

    public GameObject startMenuMainBody;
    GameObject startMenuCamera;
    GameObject player;
    public Camera playerCamera;
    public AudioListener playerListener;

    public bool inStartMenu = true;

    GameObject controlsMainBody;

    OptoinsMenuController optionsScript;

    public bool inControls = false;

    public bool goneOptionsToControls = false;

    GameObject warningScreenMainBody;

    // Start is called before the first frame update
    void Start()
    {
        startMenuMainBody = GameObject.Find("Start Menu Main Body");
        startMenuCamera = GameObject.Find("Start Menu Camera");
        startMenuMainBody.SetActive(false);

        player = GameObject.Find("PlayerController");
        player.GetComponent<PlayerController>().enabled = false;
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        playerCamera.enabled = false;
        playerListener = GameObject.Find("PlayerCamera").GetComponent<AudioListener>();
        playerListener.enabled = false;

        optionsScript = GameObject.Find("Options Menu").GetComponent<OptoinsMenuController>();
        controlsMainBody = GameObject.Find("Controls Page Main Body");
        controlsMainBody.SetActive(false);

        warningScreenMainBody = GameObject.Find("Warning Screen Main Body");

    }

    // Update is called once per frame
    void Update()
    {
        if (inStartMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (inControls)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonPressed();
            }
        }


    }

    public void StartButtonPressed()
    {
        startMenuMainBody.SetActive(false);
        startMenuCamera.SetActive(false);
        inStartMenu = false;

        playerCamera.enabled = true;
        playerListener.enabled = true;
        player.GetComponent<PlayerController>().enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OptionsButtonPressed()
    {
        startMenuMainBody.SetActive(false);
        optionsScript.inOptions = true;
    }
    public void ControlsButtonPressed()
    {
        if (inStartMenu)
        {
            startMenuMainBody.SetActive(false);
        }
        if (optionsScript.inOptions)
        {
            goneOptionsToControls = true;
            optionsScript.CloseOptionsMenu();
        }
        controlsMainBody.SetActive(true);
        inControls = true;
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }
    public void BackButtonPressed()
    {
        if (inControls)
        {
            controlsMainBody.SetActive(false);
            inControls = false;
        }
        if (!goneOptionsToControls)
        {
            if (inStartMenu)
            {
                startMenuMainBody.SetActive(true);
            }

            if (optionsScript.inOptions)
            {
                optionsScript.CloseOptionsMenu();
            }
        }
        else
        {
            optionsScript.inOptions = true;
            goneOptionsToControls = false;
        }
    }

    public void ContinueButtonPressed()
    {
        warningScreenMainBody.SetActive(false);
        startMenuMainBody.SetActive(true);
    }
}
