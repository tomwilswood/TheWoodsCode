using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInteractController : MonoBehaviour
{
    public Camera camera1;
    public bool Item1Gained = false;
    GameObject item1Model;
    public bool Item2Gained = false;
    GameObject item2Model;
    public bool Item3Gained = false;
    GameObject item3Model;

    TextMeshProUGUI pickupTooltip;
    TextMeshProUGUI openTooltip;
    TextMeshProUGUI inventoryEnterTooltip;
    TextMeshProUGUI lockedTooltip;
    TextMeshProUGUI playTooltip;

    bool doorShouldOpen = false;
    bool doorShouldClose = false;

    DeathSceneEffects deathEffectsScript;
    GameObject Item3MiddleImage;
    AudioSource page3Sound;

    TapeEffectsController tapeEffectsScript;
    GameObject[] tapeTagObjects = new GameObject[3];

    //InventoryController InventoryControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        item1Model = GameObject.Find("Page Item 1");
        item2Model = GameObject.Find("Page Item 2");
        item3Model = GameObject.Find("Page Item 3");
        pickupTooltip = GameObject.Find("Pickup Tooltip").GetComponent<TextMeshProUGUI>();
        pickupTooltip.enabled = false;
        openTooltip = GameObject.Find("Open Tooltip").GetComponent<TextMeshProUGUI>();
        openTooltip.enabled = false;
        lockedTooltip = GameObject.Find("Locked Tooltip").GetComponent<TextMeshProUGUI>();
        lockedTooltip.enabled = false;
        playTooltip = GameObject.Find("Play Tooltip").GetComponent<TextMeshProUGUI>();
        playTooltip.enabled = false;

        inventoryEnterTooltip = GameObject.Find("Inventory Enter Tooltip").GetComponent<TextMeshProUGUI>();

        inventoryEnterTooltip.color = new Color(inventoryEnterTooltip.color.r, inventoryEnterTooltip.color.g, inventoryEnterTooltip.color.b, 0.0f);

        camera1 = GameObject.Find("PlayerCamera").GetComponent<Camera>();

        deathEffectsScript = GameObject.Find("Death Scene Effects Object").GetComponent<DeathSceneEffects>();
        Item3MiddleImage = GameObject.Find("Item 3 Middle Version");
        Item3MiddleImage.SetActive(false);

        page3Sound = GameObject.Find("Page3SoundObject").GetComponent<AudioSource>();

        tapeEffectsScript = GameObject.Find("Tape Recorders").GetComponent<TapeEffectsController>();
        for (int i = 0; i < 3; i++)
        {
            tapeTagObjects[i] = GameObject.Find("tape tag object " + (i + 1));
        }

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit result;
        Ray ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(ray, out result))
        {
            GameObject g = result.collider.gameObject;

            if (g.tag == "Pickup-able")
            {
                pickupTooltip.enabled = true;
            }
            else
            {
                pickupTooltip.enabled = false;
            }

            if (g.tag == "Openable")
            {
                Animator anim = g.GetComponent<Animator>();
                if (Vector3.Distance(transform.position, g.transform.position) < 5 && !anim.GetBool("DoorShouldOpen"))
                {
                    openTooltip.enabled = true;
                }
                else
                {
                    openTooltip.enabled = false;
                }

            }
            else
            {
                openTooltip.enabled = false;
            }

            if (g.tag == "Locked" && Vector3.Distance(transform.position, g.transform.position) < 5)
            {
                lockedTooltip.enabled = true;
            }
            else
            {
                lockedTooltip.enabled = false;
            }

            if (g.tag == "Tape" && Vector3.Distance(transform.position, g.transform.position) < 5 && !tapeTagObjects[0].GetComponent<AudioSource>().isPlaying
            && !tapeTagObjects[1].GetComponent<AudioSource>().isPlaying && !tapeTagObjects[2].GetComponent<AudioSource>().isPlaying) //if the object is a tape and no tapes are playing
            {
                playTooltip.enabled = true;
            }
            else
            {
                playTooltip.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (g.name == "Page Item 1")
                {
                    Item1Gained = true;
                    item1Model.SetActive(false);

                    StartCoroutine(TextFadeIn(inventoryEnterTooltip, 1.0f));
                    StartCoroutine(WaitThenTextFadeOut(3, inventoryEnterTooltip, 1.0f));
                }

                if (g.name == "Page Item 2")
                {
                    Item2Gained = true;
                    item2Model.SetActive(false);

                    StartCoroutine(TextFadeIn(inventoryEnterTooltip, 1.0f));
                    StartCoroutine(WaitThenTextFadeOut(3, inventoryEnterTooltip, 1.0f));
                }

                if (g.name == "Page Item 3")
                {
                    Item3Gained = true;
                    item3Model.SetActive(false);
                    if (!deathEffectsScript.deathScenePlay)
                    {
                        Item3MiddleImage.SetActive(true);
                        page3Sound.Play();

                    }
                }


                if (g.tag == "Openable" && Vector3.Distance(transform.position, g.transform.position) < 5)
                {
                    Animator anim = g.GetComponent<Animator>();
                    doorShouldOpen = true;
                    doorShouldClose = false;
                    anim.SetBool("DoorShouldOpen", doorShouldOpen);
                    doorShouldOpen = false;
                    anim.SetBool("DoorShouldClose", doorShouldClose);
                    StartCoroutine(WaitThenDoorClose(5, anim));
                    if (!g.GetComponent<AudioSource>().isPlaying)
                    {
                        g.GetComponent<AudioSource>().Play();
                    }
                }

                if (g.tag == "Tape" && Vector3.Distance(transform.position, g.transform.position) < 5 && !tapeTagObjects[0].GetComponent<AudioSource>().isPlaying
                && !tapeTagObjects[1].GetComponent<AudioSource>().isPlaying && !tapeTagObjects[2].GetComponent<AudioSource>().isPlaying)
                {
                    g.GetComponent<AudioSource>().Play();
                    for (int i = 0; i < 3; i++)
                    {
                        if (g.name == ("tape tag object " + (i + 1)))
                        {
                            tapeEffectsScript.tapePlayed[i] = true;
                        }
                    }
                }

            } // end of e pressed
        } // end of raycast

        if (deathEffectsScript.deathScenePlay)
        {
            Item3MiddleImage.SetActive(false);
            if (page3Sound.volume > 0)
            {
                page3Sound.volume -= 2.0f * Time.deltaTime;
            }
        }

    }

    public IEnumerator WaitThenDoorClose(float seconds, Animator anim2)
    {
        yield return new WaitForSeconds(seconds);
        doorShouldClose = true;
        doorShouldOpen = false;
        anim2.SetBool("DoorShouldOpen", doorShouldOpen);
        anim2.SetBool("DoorShouldClose", doorShouldClose);
    }

    public IEnumerator TextFadeIn(TextMeshProUGUI text, float modifer)
    { //fade-in and out text taken from https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime / modifer);
            yield return null;

        }

    }

    public IEnumerator WaitThenTextFadeIn(float seconds, TextMeshProUGUI text, float modifer)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(TextFadeIn(text, modifer));
    }

    public IEnumerator TextFadeOut(TextMeshProUGUI text, float modifer)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime / modifer);
            yield return null;
        }
    }

    public IEnumerator WaitThenTextFadeOut(float seconds, TextMeshProUGUI text, float modifer)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(TextFadeOut(text, modifer));
    }
}
