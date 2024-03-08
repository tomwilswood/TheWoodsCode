using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPathProximitySwitch : MonoBehaviour
{

    public GameObject nextLightSource;
    Light ownLight;
    AudioSource ownSound;
    Light nextLight;
    AudioSource nextSound;

    public bool lastLightInPath_Archive = false;
    public bool lastLightInPath_Lab = false;

    public GameObject nextSceneLightSource1;
    public GameObject nextSceneLightSource2;
    Light[] nextSceneLights;

    // Start is called before the first frame update
    void Start()
    {
        ownLight = GetComponent<Light>();
        ownSound = GetComponent<AudioSource>();

        if (!lastLightInPath_Lab)
        {
            nextLight = nextLightSource.GetComponent<Light>();
            nextLight.enabled = false;
            nextSound = nextLightSource.GetComponent<AudioSource>();
            nextSound.enabled = false;
        }

        nextSceneLights = new Light[2];

        if (lastLightInPath_Archive)
        {
            nextSceneLights[0] = nextSceneLightSource1.GetComponent<Light>();
            nextSceneLights[1] = nextSceneLightSource2.GetComponent<Light>();
            for (int i = 0; i < 2; i++)
            {
                nextSceneLights[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        ownLight.enabled = false;
        ownSound.enabled = false;

        if (!lastLightInPath_Lab)
        {
            nextLight.enabled = true;
            nextSound.enabled = true;
        }

        if (lastLightInPath_Archive)
        {
            for (int i = 0; i < 2; i++)
            {
                nextSceneLights[i].enabled = true;
            }
        }
    }
}
