using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestProperEntranceDetection : MonoBehaviour
{

    public bool playerInNest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        playerInNest = true;
    }
}
