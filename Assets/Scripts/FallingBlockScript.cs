using UnityEngine;
using System.Collections;

public class FallingBlockScript : MonoBehaviour
{
    public bool FadeInsteadOfFall = false;
    public bool FadeAndFall = false;
    public float FadeOffset = 0f; // Determines how much time before other objects fall that this object will fade.
    public bool AllowExtraLife = false;
    public float ExtraLifeSpan = 20f;

    private bool m_ExtraLifeGiven = false;

    private float m_lifetime = 300.0f;
    private float m_destroyAfter = 310.0f;
    public float ShakeTime = 2.0f;
    public float MinShakeMagnitude = 0.01f;
    public float MaxShakeMagnitude = 0.1f;
    private float m_maxFallOffset = 5.0f;
    private float m_fallTimeStart = 80.0f; //180f

    private float m_LifeSpent = 0.0f;
    private float m_CurrentShakeMag = 0.0f;
    private Vector3 m_StartPosition;

    private EventManager m_eventManager;

    private const float FallTimeMultiplier = 1.9f;

    void Start()
    {
        m_LifeSpent = 0f;

        m_eventManager = FindObjectOfType<EventManager>();

        // Set Lifetime and Destroy After based on X position
        Vector3 worldPosition = this.transform.position;

        if (worldPosition.x <= 0)
        {
            m_lifetime = m_fallTimeStart - (-worldPosition.x * FallTimeMultiplier);
            m_destroyAfter = m_lifetime + 10;
        }
        else if (worldPosition.x > 0)
        {
            m_lifetime = m_fallTimeStart - (worldPosition.x * FallTimeMultiplier);
            m_destroyAfter = m_lifetime + 10;
        }

        m_maxFallOffset = 0.5f;
        float rangeAdjustment = Random.Range(0.01f, m_maxFallOffset) - m_maxFallOffset / 2.0f;
        m_CurrentShakeMag = MinShakeMagnitude;
        m_lifetime += rangeAdjustment;
        m_destroyAfter += rangeAdjustment;
        m_StartPosition = this.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (AllowExtraLife && collision.gameObject.tag == "Player2")
        {
            if(m_ExtraLifeGiven == false)
            {
                m_ExtraLifeGiven = true;
                //m_LifeSpent -= ExtraLifeSpan;
            }
        }
    }

    // Creates a rumbling/shaking effect
    void Shake()
    {
        Vector3 shakePosition = this.transform.position;
        shakePosition.x += Random.Range(0.0f, m_CurrentShakeMag) - m_CurrentShakeMag / 2.0f;
        shakePosition.y += Random.Range(0.0f, m_CurrentShakeMag) - m_CurrentShakeMag / 2.0f;

        if (m_CurrentShakeMag < MaxShakeMagnitude) m_CurrentShakeMag += 0.01f * Time.deltaTime;

        Vector3 pos = this.transform.position;
        pos.x = shakePosition.x;
        pos.y = shakePosition.y;
        this.transform.position = pos;
    }

    
    void FadeAlpha()
    {
        // Fade our sprite
        if(GetComponent<SpriteRenderer>() != null)
        {
            Color current = GetComponent<SpriteRenderer>().color;
            if (current.a > 0f)
                current.a -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = current;
        }
        // Fade all child sprites too
        SpriteRenderer[] srList = GetComponentsInChildren<SpriteRenderer>();
        if(srList.Length > 0)
        { 
            foreach (SpriteRenderer sr in srList)
            {
                if (sr != null)
                {
                    Color current = sr.color;
                    if (current.a > 0f)
                        current.a -= Time.deltaTime / 4;
                    sr.color = current;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_LifeSpent = m_eventManager.lifeSpent;
        if(m_ExtraLifeGiven)
        {
            m_LifeSpent -= ExtraLifeSpan;
        }

        //// Esnure physics doesn't toss our tiles into the air
        //if (this.transform.position.z < 0f)
        //{
        //    Vector3 pos = this.transform.position;
        //    //pos.z = 0f;
        //    this.transform.position = pos;
        //}

        // Trigger a particle effect if this tile was given extra life
        if (AllowExtraLife && m_ExtraLifeGiven)
        {
            if (m_LifeSpent + ExtraLifeSpan > m_lifetime - FadeOffset && GetComponent<Rigidbody>().isKinematic && GetComponentInChildren<ParticleSystem>() != null)
            {
                if (!GetComponentInChildren<ParticleSystem>().isPlaying)
                    GetComponentInChildren<ParticleSystem>().Play();
            }
            else if(!GetComponent<Rigidbody>().isKinematic)
            {
                if (GetComponentInChildren<ParticleSystem>().isPlaying)
                    GetComponentInChildren<ParticleSystem>().Stop();
            }
        }

        if(FadeInsteadOfFall )
        {
            if(m_LifeSpent > m_lifetime - FadeOffset)
            {
                FadeAlpha();
            }
        }
        else
        {
            if (FadeAndFall && m_LifeSpent > m_lifetime - FadeOffset)
            {
                FadeAlpha();
            }

            if (m_LifeSpent > m_lifetime - ShakeTime)
            {
                Shake();
            }

            if (m_LifeSpent > m_lifetime)
            {
                if(this.GetComponent<Rigidbody>() != null)
                { 
                    this.GetComponent<Rigidbody>().isKinematic = false;
                    this.GetComponent<Rigidbody>().WakeUp();
                }
            }
            if (m_LifeSpent > m_destroyAfter)
            {
                Destroy(this.gameObject);
            }
        }

        
    }
}
