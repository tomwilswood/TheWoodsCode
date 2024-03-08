using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabToNestMiddleController : MonoBehaviour
{

    public GameObject TheLab;
    public GameObject TheNest;

    // Start is called before the first frame update
    void Start()
    {
        TheNest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        TheLab.SetActive(false);
        TheNest.SetActive(true);
    }
}
