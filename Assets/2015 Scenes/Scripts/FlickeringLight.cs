using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FlickeringLight : MonoBehaviour {

    Light myLittleLight;
    //float initialIntensity;
    float initialRange;
    public float flickerAmount = 0.1f;

    void Awake()
    {
        this.myLittleLight = GetComponent<Light>();
        //this.initialIntensity = myLittleLight.intensity;
        this.initialRange = myLittleLight.range;
    }

	void Update ()
    {
        // I would recommend varying range over intensity because it doesn't produce a strobe effect
        //light.intensity = initialIntensity + Random.Range(-flickerAmount, flickerAmount;
        myLittleLight.range = initialRange + Random.Range(-flickerAmount, flickerAmount);
	}
}
