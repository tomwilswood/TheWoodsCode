using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveToLabPreMiddleController : MonoBehaviour
{

    public GameObject TheLab;
    public GameObject OtherLabObjects;
    public GameObject tapeRecorder1;
    // Start is called before the first frame update
    void Start()
    {
        TheLab.SetActive(false);
        OtherLabObjects.SetActive(false);
        tapeRecorder1.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        TheLab.SetActive(true);
        tapeRecorder1.SetActive(true);
        OtherLabObjects.SetActive(true);
    }
}
