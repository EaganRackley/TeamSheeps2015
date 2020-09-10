using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerFadeObjects : MonoBehaviour
{
    private bool m_triggered;
    public float StartDelay = 0f;
    public bool Flash = false;
    public float FlashTime = 2f;
    public bool FadeIn = true;
    public float FadeSpeed = 0.05f;
    public float TargetLightIntensity = 3.81f;
    public GameObject Target3DObject;
    public GameObject Target2DObject;
    public Light TargetLight;
    public TextMeshPro TextObject;
    public float HideAfter = 0.0f;
    private float m_hideAfterTimer = 0.0f;
    private float m_startTimer = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        m_startTimer = 0f;
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

        if(HideAfter > 0f || Flash == true || StartDelay > 0f)
        {
            return true;
        }

        else if (FadeIn)
        {
            if (TextObject)
            {
                if (TextObject.GetComponent<TextMeshPro>().color.a <= 0.9f)
                {
                    returnValue = false;
                }
            }
            if (TargetLight)
            {
                if (TargetLight.intensity <= TargetLightIntensity - 0.1f)
                {
                    returnValue = false;
                }
            }
            if (Target3DObject)
            {
                if (Target3DObject.GetComponent<MeshRenderer>().material.color.a <= 0.9f)
                {
                    returnValue = false;
                }
            }
            if (Target2DObject)
            {
                if (Target2DObject.GetComponent<SpriteRenderer>().color.a <= 0.9f)
                {
                    returnValue = false;
                }
            }
        }
        else
        {
            if (TextObject)
            {
                if (TextObject.GetComponent<TextMeshPro>().color.a >= 0.1f)
                {
                    returnValue = false;
                }
            }
            if (TargetLight)
            {
                if (TargetLight.intensity >= 0.1f)
                {
                    returnValue = false;
                }
            }
            if (Target3DObject)
            {
                if (Target3DObject.GetComponent<MeshRenderer>().material.color.a >= 0.1f)
                {
                    returnValue = false;
                }
            }
            if (Target2DObject)
            {
                if (Target2DObject.GetComponent<SpriteRenderer>().color.a >= 0.1f)
                {
                    returnValue = false;
                }
            }
        }

        return returnValue;
    }

    public void Finish()
    {
        if (FadeIn && HideAfter <= 0f)
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
        m_triggered = true;
        m_hideAfterTimer = HideAfter;
        m_startTimer = StartDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_triggered)
        {
            if(m_startTimer < StartDelay)
            {
                m_startTimer += Time.deltaTime;
                return;
            }

            if (FadeIn)
                HandledFadeIn();
            else
                HandledFadeOut();

            if (HideAfter > 0f)
            {
                m_hideAfterTimer += Time.deltaTime;
                if (m_hideAfterTimer > HideAfter)
                {
                    if (!Flash)
                        FadeIn = false;
                    else {
                        m_hideAfterTimer = 0f;
                        HideAfter = FlashTime;
                        FadeIn = !FadeIn;
                    }
                }
            }
        }
    }
}
