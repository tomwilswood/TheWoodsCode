using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartingPointEffects2 : MonoBehaviour
{
    public TextMeshProUGUI FollowText;
    TextMeshProUGUI runAndJumpText;

    Light lampLight;
    AudioSource flickerSound;

    Light LightPath1Light1;

    StartMenuController startMenuScript;

    bool effectsDone = false;

    // Start is called before the first frame update
    void Start()
    {
        startMenuScript = GameObject.Find("Start Menu").GetComponent<StartMenuController>();

        FollowText = GameObject.Find("Follow the Light Tooltip").GetComponent<TextMeshProUGUI>();
        FollowText.color = new Color(FollowText.color.r, FollowText.color.g, FollowText.color.b, 0.0f);

        runAndJumpText = GameObject.Find("Sprint and Jump Tooltip").GetComponent<TextMeshProUGUI>();
        runAndJumpText.color = new Color(runAndJumpText.color.r, runAndJumpText.color.g, runAndJumpText.color.b, 0.0f);

        lampLight = GameObject.Find("Street Lamp Point Light").GetComponent<Light>();
        flickerSound = GameObject.Find("Street Lamp Point Light").GetComponent<AudioSource>();

        LightPath1Light1 = GameObject.Find("LP1 Light 1").GetComponent<Light>();
        LightPath1Light1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!effectsDone && !startMenuScript.inStartMenu)
        {
            StartCoroutine(WaitThenStartingPointEffects(5));
            effectsDone = true;
        }
    }

    IEnumerator WaitThenStartingPointEffects(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        lampLight.enabled = false;
        flickerSound.enabled = false;
        StartCoroutine(GetComponent<ItemInteractController>().TextFadeIn(FollowText, 2.0f));
        StartCoroutine(GetComponent<ItemInteractController>().WaitThenTextFadeOut(3, FollowText, 1.0f));
        StartCoroutine(GetComponent<ItemInteractController>().TextFadeIn(runAndJumpText, 2.0f));
        StartCoroutine(GetComponent<ItemInteractController>().WaitThenTextFadeOut(3, runAndJumpText, 1.0f));

        LightPath1Light1.enabled = true;


    }
}