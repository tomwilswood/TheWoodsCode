using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    Light Lamplight;
    float randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        Lamplight = GetComponent<Light>();
        randomOffset = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        Lamplight.intensity = Mathf.PerlinNoise(0, 10.0f * Time.timeSinceLevelLoad + randomOffset);
    }
}
