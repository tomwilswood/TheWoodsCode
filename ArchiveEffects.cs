using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArchiveEffects : MonoBehaviour
{

    Light ItemLight;
    GameObject Player;
    ItemInteractController ItemScript;
    StartingPointEffects2 StartingPointEffectsScript;

    Light[] sceneLights;
    AudioSource flickerSound;

    TextMeshProUGUI ObserveText;
    Light LightPath2Light1;
    public bool effectsDone = false;
    bool effectsDone2 = false;

    public GameObject finalLightofPreviousPath;
    AudioSource audio1;
    AudioSource knockingSound;

    // Start is called before the first frame update
    void Start()
    {
        ItemLight = GameObject.Find("Item light 1").GetComponent<Light>();
        ItemLight.enabled = false;

        Player = GameObject.Find("PlayerController");

        ItemScript = Player.GetComponent<ItemInteractController>();
        StartingPointEffectsScript = Player.GetComponent<StartingPointEffects2>();

        sceneLights = new Light[3];
        sceneLights[0] = GameObject.Find("Archive Point Light 1").GetComponent<Light>();
        sceneLights[1] = GameObject.Find("Archive Point Light 2").GetComponent<Light>();
        sceneLights[2] = GameObject.Find("Archive Lamp").GetComponent<Light>();

        flickerSound = GameObject.Find("Archive Lamp").GetComponent<AudioSource>();

        ObserveText = GameObject.Find("Observe Their Folly Tooltip").GetComponent<TextMeshProUGUI>();
        ObserveText.color = new Color(ObserveText.color.r, ObserveText.color.g, ObserveText.color.b, 0.0f);

        LightPath2Light1 = GameObject.Find("LP2 Light 1").GetComponent<Light>();
        LightPath2Light1.enabled = false;

        audio1 = GetComponent<AudioSource>();
        knockingSound = GameObject.Find("Archive Extra Sound Object").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (ItemScript.Item1Gained)
            {
                EndScene();
            }
           
        
    }

    void OnTriggerEnter (Collider other) {
        if (!effectsDone2)
        {
            finalLightofPreviousPath.GetComponent<LightPathProximitySwitch>().enabled = false;
            StartCoroutine(WaitThenItemLightOn(15));
            StartCoroutine(WaitThenKillLightsIfNoItemPickup(30));
            StartCoroutine(Player.GetComponent<ItemInteractController>().TextFadeIn(ObserveText, 2.0f));
            StartCoroutine(Player.GetComponent<ItemInteractController>().WaitThenTextFadeOut(3, ObserveText, 1.0f));
            effectsDone2 = true;
        }
    }

    IEnumerator WaitThenItemLightOn(float seconds) {
        yield return new WaitForSeconds(seconds);
        if (!ItemScript.Item1Gained)
        {
            ItemLight.enabled = true;
        }
    }

    IEnumerator WaitThenKillLightsIfNoItemPickup(float seconds) {
        yield return new WaitForSeconds(seconds);
        if (!ItemScript.Item1Gained)
        {
                EndScene();
            
        }
    }

    void EndScene () {
        if (!effectsDone)
        {
            ItemLight.enabled = false;
            flickerSound.enabled = false;
            for (int i = 0; i < 3; i++)
            {
                sceneLights[i].enabled = false;
            }
            LightPath2Light1.enabled = true;
            StartCoroutine(Player.GetComponent<ItemInteractController>().WaitThenTextFadeIn(4, StartingPointEffectsScript.FollowText, 2.0f));
            StartCoroutine(Player.GetComponent<ItemInteractController>().WaitThenTextFadeOut(7, StartingPointEffectsScript.FollowText, 1.0f));

            audio1.Play();
            knockingSound.Play();
            effectsDone = true;
        }
    }
}
