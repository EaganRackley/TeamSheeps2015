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
    public float HideAfter = 0.0f;
    private float m_hideAfterTimer = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        m_hideAfterTimer = 0.0f;
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
        if (TextObject)
        {
            Color rColor = TextObject.GetComponent<TextMeshPro>().color;
            rColor.a = Mathf.MoveTowards(rColor.a, 0.0f, Time.deltaTime / FadeSpeed);
            TextObject.GetComponent<TextMeshPro>().color = rColor;
        }
        if (TargetLight)
        {
            Mathf.MoveTowards(TargetLight.intensity, 0.0f, Time.deltaTime / FadeSpeed);
        }
        if (Target3DObject)
        {
            Color rColor = Target3DObject.GetComponent<MeshRenderer>().material.color;
            rColor.a = Mathf.MoveTowards(rColor.a, 0.0f, Time.deltaTime / FadeSpeed);
            Target3DObject.GetComponent<MeshRenderer>().material.color = rColor;
        }
        if (Target2DObject)
        {
            Color rColor = Target2DObject.GetComponent<SpriteRenderer>().color;
            rColor.a = Mathf.MoveTowards(rColor.a, 0.0f, Time.deltaTime / FadeSpeed);
            Target2DObject.GetComponent<SpriteRenderer>().color = rColor;
        }
    }

    public bool IsFinished()
    {
        bool returnValue = true;
        if (FadeIn)
        {
            if (TextObject)
            {
                if (TextObject.GetComponent<TextMeshPro>().color.a != 1.0f)
                {
                    returnValue = false;
                }
            }
            if (TargetLight)
            {
                if (TargetLight.intensity != TargetLightIntensity)
                {
                    returnValue = false;
                }
            }
            if (Target3DObject)
            {
                if (Target3DObject.GetComponent<MeshRenderer>().material.color.a != 1.0f)
                {
                    returnValue = false;
                }
            }
            if (Target2DObject)
            {
                if (Target2DObject.GetComponent<SpriteRenderer>().color.a != 1.0f)
                {
                    returnValue = false;
                }
            }
        }
        else
        {
            if (TextObject)
            {
                if (TextObject.GetComponent<TextMeshPro>().color.a > 0.0f)
                {
                    returnValue = false;
                }
            }
            if (TargetLight)
            {
                if (TargetLight.intensity > 0.0f)
                {
                    returnValue = false;
                }
            }
            if (Target3DObject)
            {
                if (Target3DObject.GetComponent<MeshRenderer>().material.color.a > 0.0f)
                {
                    returnValue = false;
                }
            }
            if (Target2DObject)
            {
                if (Target2DObject.GetComponent<SpriteRenderer>().color.a > 0.0f)
                {
                    returnValue = false;
                }
            }
        }

        return returnValue;
    }

    public void Finish()
    {
        if (FadeIn)
        {
            if (TextObject)
            {
                Color rColor = TextObject.GetComponent<TextMeshPro>().color;
                rColor.a = 1.0f;
                TextObject.GetComponent<TextMeshPro>().color = rColor;
            }
            if (TargetLight)
            {
                TargetLight.intensity = TargetLightIntensity;
            }
            if (Target3DObject)
            {
                Color rColor = Target3DObject.GetComponent<MeshRenderer>().material.color;
                rColor.a = 1.0f;
                Target3DObject.GetComponent<MeshRenderer>().material.color = rColor;
            }
            if (Target2DObject)
            {
                Color rColor = Target2DObject.GetComponent<SpriteRenderer>().color;
                rColor.a = 1.0f;
                Target2DObject.GetComponent<SpriteRenderer>().color = rColor;
            }
        }
        else
        {
            if (TextObject)
            {
                Color rColor = TextObject.GetComponent<TextMeshPro>().color;
                rColor.a = 0.0f;
                TextObject.GetComponent<TextMeshPro>().color = rColor;
            }
            if (TargetLight)
            {
                TargetLight.intensity = 0.0f;
            }
            if (Target3DObject)
            {
                Color rColor = Target3DObject.GetComponent<MeshRenderer>().material.color;
                rColor.a = 0.0f;
                Target3DObject.GetComponent<MeshRenderer>().material.color = rColor;
            }
            if (Target2DObject)
            {
                Color rColor = Target2DObject.GetComponent<SpriteRenderer>().color;
                rColor.a = 0.0f;
                Target2DObject.GetComponent<SpriteRenderer>().color = rColor;
            }
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

            if (HideAfter > 0f)
            {
                m_hideAfterTimer += Time.deltaTime;
                if (m_hideAfterTimer > HideAfter)
                {
                    FadeIn = false;
                }
            }
        }
    }
}
