using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffOnTrigger : MonoBehaviour
{

    
    Light ownLight;
    AudioSource ownSound;

    // Start is called before the first frame update
    void Start()
    {
        ownLight = GetComponent<Light>();
        ownSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        ownLight.enabled = false;
        ownSound.enabled = false;
    }
}
