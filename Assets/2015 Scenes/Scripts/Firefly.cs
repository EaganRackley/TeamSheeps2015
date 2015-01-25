using UnityEngine;
using System.Collections;

public class Firefly : MonoBehaviour {

    public float onTime;
    public float offTime;
    public float randomVariance;

    float lightCountdown;
    bool lightOn;

    Light fireflyLight;

    void Awake()
    {
        fireflyLight = GetComponentInChildren<Light>();
    }

    void Start()
    {
        if (Random.Range(0, 1) > 0)
        {
            this.lightOn = true;
            lightCountdown = onTime + Random.Range(0f, randomVariance);
        }
        else
        {
            this.lightOn = false;
            lightCountdown = offTime + Random.Range(0f, randomVariance);
        }
    }

    void Update()
    {
        lightCountdown -= Time.deltaTime;
        if (lightCountdown <= 0f)
        {
            if (this.lightOn)
            {
                this.lightOn = false;
                lightCountdown = offTime + Random.Range(0f, randomVariance);
            }
            else
            {
                this.lightOn = true;
                lightCountdown = onTime + Random.Range(0f, randomVariance);
            }
        }
        UpdateLight();
    }

    void UpdateLight()
    {
        if (this.lightOn)
            fireflyLight.enabled = true;
        else
            fireflyLight.enabled = false;
    }
}
