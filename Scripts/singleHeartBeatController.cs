using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleHeartBeatController : MonoBehaviour
{
    AudioSource heartbeatSound;
    // Start is called before the first frame update
    void Start()
    {
        heartbeatSound = GameObject.Find("Heart").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSingleHeartBeat()
    {
        heartbeatSound.Play();
    }
}
