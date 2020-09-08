using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerFadeObjects : MonoBehaviour
{
    private bool m_triggered;
    public bool FadeIn = true;
    public float FadeSpeed = 0.05f;
    public float TargetLightIntensity = 3.81f;
    public GameObject Target3DObject;
    public GameObject Target2DObject;
    public Light TargetLight;
    public TextMeshPro TextObject;
    

    // Start is called before the first frame update
    void Start()
    {
        m_triggered = false;
        if(FadeIn)
        {
            if (TextObject)
            {
                Color rColor = TextObject.GetComponent<TextMeshPro>().color;
                rColor.a = 0f;
                TextObject.GetComponent<TextMeshPro>().color = rColor;
            }
            if (TargetLight)
            {
                TargetLight.intensity = 0f;
            }
            if(Target3DObject)
            {
                Color rColor = Target3DObject.GetComponent<MeshRenderer>().material.color;
                rColor.a = 0f;
                Target3DObject.GetComponent<MeshRenderer>().material.color = rColor;
            }
            if (Target2DObject)
            {
                Color rColor = Target2DObject.GetComponent<SpriteRenderer>().color;
                rColor.a = 0f;
                Target2DObject.GetComponent<SpriteRenderer>().color = rColor;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit player, fading now!");
            m_triggered = true;
        }
    }

    void HandledFadeIn()
    {
        if(TextObject)
        {
            Color rColor = TextObject.GetComponent<TextMeshPro>().color;
            rColor.a = Mathf.Lerp(rColor.a, 1.0f, Time.deltaTime / FadeSpeed);
            TextObject.GetComponent<TextMeshPro>().color = rColor;
        }
        if (TargetLight)
        {
            TargetLight.intensity = Mathf.Lerp(TargetLight.intensity, TargetLightIntensity, Time.deltaTime / FadeSpeed);
        }
        if (Target3DObject)
        {
            Color rColor = Target3DObject.GetComponent<MeshRenderer>().material.color;
            rColor.a = Mathf.Lerp(rColor.a, 1.0f, Time.deltaTime / FadeSpeed);
            Target3DObject.GetComponent<MeshRenderer>().material.color = rColor;
        }
        if (Target2DObject)
        {
            Color rColor = Target2DObject.GetComponent<SpriteRenderer>().color;
            rColor.a = Mathf.Lerp(rColor.a, 1.0f, Time.deltaTime / FadeSpeed);
            Target2DObject.GetComponent<SpriteRenderer>().color = rColor;
        }
        
    }

    void HandledFadeOut()
    {
        
        if (TargetLight)
        {
            Mathf.Lerp(TargetLight.intensity, 0.0f, Time.deltaTime / FadeSpeed);
        }
        if (Target3DObject)
        {
            Color rColor = Target3DObject.GetComponent<MeshRenderer>().material.color;
            rColor.a = Mathf.Lerp(rColor.a, 0.0f, Time.deltaTime / FadeSpeed);
            Target3DObject.GetComponent<MeshRenderer>().material.color = rColor;
        }
        if (Target2DObject)
        {
            Color rColor = Target2DObject.GetComponent<SpriteRenderer>().color;
            rColor.a = Mathf.Lerp(rColor.a, 0.0f, Time.deltaTime / FadeSpeed);
            Target2DObject.GetComponent<SpriteRenderer>().color = rColor;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_triggered)
        {
            if (FadeIn)
                HandledFadeIn();
            else
                HandledFadeOut();
        }
    }
}
