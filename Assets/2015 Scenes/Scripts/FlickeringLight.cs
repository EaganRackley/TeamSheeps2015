using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour {

    Light light;
    float initialIntensity;
    float initialRange;
    public float flickerAmount = 0.1f;

    void Awake()
    {
        this.light = GetComponent<Light>();
        this.initialIntensity = light.intensity;
        this.initialRange = light.range;
    }

	void Update ()
    {
        // I would recommend varying range over intensity because it doesn't produce a strobe effect
        //light.intensity = initialIntensity + Random.Range(-flickerAmount, flickerAmount;
        light.range = initialRange + Random.Range(-flickerAmount, flickerAmount);
	}
}
