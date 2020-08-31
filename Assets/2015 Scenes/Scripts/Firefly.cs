using UnityEngine;
using System.Collections;

public class Firefly : MonoBehaviour {

    public float onTime;
    public float offTime;
    public float randomVariance;
    private float m_maxIntensity;

    float lightCountdown;
    bool lightOn;

    Light fireflyLight;

    void Awake()
    {
        fireflyLight = GetComponentInChildren<Light>();
        m_maxIntensity = fireflyLight.intensity;
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
        {
            fireflyLight.intensity = Mathf.Lerp(0f, m_maxIntensity, onTime/2f);
        }
        else
        {
            fireflyLight.intensity = Mathf.Lerp(m_maxIntensity, 0f, onTime / 2f);
        }
            
    }
}
