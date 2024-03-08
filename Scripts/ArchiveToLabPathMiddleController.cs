using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveToLabPathMiddleController : MonoBehaviour
{

    public GameObject TheArchive;
    AudioSource growlSound;
    // Start is called before the first frame update
    void Start()
    {
        growlSound = GameObject.Find("Growl Sound Object").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        TheArchive.SetActive(false);
        growlSound.Play();
    }
}
