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
    private float m_destroyAfter = 310.0f;
    public float ShakeTime = 2.0f;
    public float MinShakeMagnitude = 0.01f;
    public float MaxShakeMagnitude = 0.1f;
    private float m_maxFallOffset = 5.0f;
    private float m_lifetime = 300.0f; // m_fallTimeStart + 120f
    private float m_fallTimeStart = 180.0f; //180f
    private float m_LifeSpent = 0.0f;
    private float m_CurrentShakeMag = 0.0f;
    private Vector3 m_StartPosition;
    private EventManager m_eventManager;
    private const float FallTimeMultiplier = 1.9f;
    private Rigidbody m_rigidbody;
    private ParticleSystem m_particleSystem;
    private SpriteRenderer m_spriteRenderer;
    SpriteRenderer[] m_srList;


    void Start()
    {
        m_LifeSpent = 0f;
        m_eventManager = FindObjectOfType<EventManager>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_particleSystem = GetComponent<ParticleSystem>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_srList = GetComponentsInChildren<SpriteRenderer>();

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
        if(m_spriteRenderer != null)
        {
            Color current = m_spriteRenderer.color;
            if (current.a > 0f)
                current.a -= Time.deltaTime;
            m_spriteRenderer.color = current;
        }
        // Fade all child sprites too
        if(m_srList.Length > 0)
        { 
            foreach (SpriteRenderer sr in m_srList)
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
    void FixedUpdate()
    {
        m_LifeSpent = m_eventManager.lifeSpent;
        if(m_ExtraLifeGiven)
        {
            m_LifeSpent -= ExtraLifeSpan;
        }

        //// Ensure physics doesn't toss our tiles into the air
        //if (this.transform.position.z < 0f)
        //{
        //    Vector3 pos = this.transform.position;
        //    //pos.z = 0f;
        //    this.transform.position = pos;
        //}
        
        // Trigger a particle effect if this tile was given extra life
        if (AllowExtraLife && m_ExtraLifeGiven)
        {
            if (m_rigidbody && m_LifeSpent + ExtraLifeSpan > m_lifetime - FadeOffset && m_rigidbody.isKinematic && m_particleSystem != null)
            {
                if (m_particleSystem && !m_particleSystem.isPlaying)
                    m_particleSystem.Play();
            }
            else if(m_rigidbody && !m_rigidbody.isKinematic)
            {
                if (m_particleSystem.isPlaying)
                    m_particleSystem.Stop();
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
                if(m_rigidbody != null)
                { 
                    m_rigidbody.isKinematic = false;
                    m_rigidbody.WakeUp();
                }
            }
            if (m_LifeSpent > m_destroyAfter)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
