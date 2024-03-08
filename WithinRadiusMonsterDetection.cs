using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinRadiusMonsterDetection : MonoBehaviour
{
    public bool inMonsterRadius = false;
    ChaseController chaseScript;
    // Start is called before the first frame update
    void Start()
    {
        chaseScript = GameObject.Find("Monster Stand-In Object").GetComponent<ChaseController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay (Collider other) {
        inMonsterRadius = true;
    }

    void OnTriggerExit (Collider other) {
        inMonsterRadius = false;
        if (!chaseScript.chaseBegins) {
            chaseScript.chaseBegins = true;
        }
    }
}
