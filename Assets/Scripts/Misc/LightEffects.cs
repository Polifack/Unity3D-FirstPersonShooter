using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffects : MonoBehaviour
{
    public float deltaIntensity;
    private Light l;
    private float initIntensity;

    private void Awake()
    {
        l = GetComponent<Light>();
        initIntensity = l.intensity;
    }

    private void Update()
    {
        float val = Mathf.PingPong(Time.time, deltaIntensity);
        l.intensity = initIntensity + val;
    }
}
