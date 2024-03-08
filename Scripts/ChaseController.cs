using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseController : MonoBehaviour
{
    public bool chaseBegins = false;
    int stage = 1;
    float rateOfChase = 5f;
    bool inPos = false;

    WithinRadiusMonsterDetection radiusDetectionScript;
    Image redScreen;
    DeathSceneEffects deathEffectsScript;
    NestProperEntranceDetection nestEntranceDetectionScript;

    OptoinsMenuController optionsMenuScript;
    ItemInteractController itemScript;

    // Start is called before the first frame update
    void Start()
    {
        radiusDetectionScript = GameObject.Find("Monster Radius").GetComponent<WithinRadiusMonsterDetection>();
        redScreen = GameObject.Find("Red Screen").GetComponent<Image>();
        redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 0.0f);
        deathEffectsScript = GameObject.Find("Death Scene Effects Object").GetComponent<DeathSceneEffects>();
        nestEntranceDetectionScript = GameObject.Find("NestEntranceDetectionObject").GetComponent<NestProperEntranceDetection>();

        optionsMenuScript = GameObject.Find("Options Menu").GetComponent<OptoinsMenuController>();
        itemScript = GameObject.Find("PlayerController").GetComponent<ItemInteractController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!deathEffectsScript.deathScenePlay && !optionsMenuScript.inOptions)
        {
            if (chaseBegins)
            { //makes the mosnter stand-in follow the path around
                if (stage == 1)
                {
                    MoveToPostion(379.4433f, 1.500099f, 588.397f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 2)
                {
                    RotateToYPosition(100.757f);
                    MoveToPostion(377.0162f, 1.500099f, 560.354f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 3)
                {
                    RotateToYPosition(129.74f);
                    MoveToPostion(371.4695f, 1.529876f, 553.4333f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 4)
                {
                    RotateToYPosition(19.77f);
                    MoveToPostion(365.8279f, 1.599427f, 553.3591f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 5)
                {
                    RotateToYPosition(70.21f);
                    MoveToPostion(366.9832f, 1.500099f, 584.5143f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 6)
                {
                    RotateToYPosition(38.982f);
                    MoveToPostion(352.1807f, 1.500099f, 595.8986f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 7)
                {
                    RotateToYPosition(2.632f);
                    MoveToPostion(326.382f, 1.500099f, 596.0975f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 8)
                {
                    RotateToYPosition(-43.737f);
                    MoveToPostion(319.8156f, 1.500099f, 591.4091f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 9)
                {
                    RotateToYPosition(-69.993f);
                    MoveToPostion(317.5875f, 1.61888f, 581.9174f);
                    if (inPos)
                    {
                        stage++;
                        inPos = false;
                    }
                }
                else if (stage == 10)
                { //completed journey
                    RotateToYPosition(-109.488f);
                }

                if (!nestEntranceDetectionScript.playerInNest)
                {

                    if (radiusDetectionScript.inMonsterRadius)
                    {
                        if (redScreen.color.a < 0.14f)
                        {
                            redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, redScreen.color.a + (0.02f * Time.deltaTime));
                        }

                    }
                    else
                    {
                        if (redScreen.color.a > 0)
                        {
                            redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, redScreen.color.a - (0.02f * Time.deltaTime));
                        }
                    }
                }
                else
                { // if the player IS in the nest
                    if (redScreen.color.a < 0.14f)
                    {
                        redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, redScreen.color.a + (0.01f * Time.deltaTime));
                    }
                }

                if (redScreen.color.a >= 0.13f && !optionsMenuScript.easyModeEnabled)
                {
                    deathEffectsScript.deathScenePlay = true;
                    redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 0.0f);
                }

                if (optionsMenuScript.easyModeEnabled && redScreen.color.a >= 0.13f && itemScript.Item3Gained)
                {
                    StartCoroutine(WaitThenKillPlayer(1.5f));
                }

            } // end of chase begins
        } //end of if not in death scene or in options
    }

    void MoveToPostion(float newX, float newY, float newZ)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, newY, newZ), rateOfChase * Time.deltaTime);
        if (transform.position == new Vector3(newX, newY, newZ))
        {
            inPos = true;
        }
    }

    void RotateToYPosition(float newY)
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, newY, transform.rotation.z);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Dead from hitting monster");
        if (chaseBegins && !optionsMenuScript.easyModeEnabled)
        {
            deathEffectsScript.deathScenePlay = true;
            redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 0.0f);
        }
    }

    IEnumerator WaitThenKillPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        deathEffectsScript.deathScenePlay = true;
        redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 0.0f);
    }
}
