using UnityEngine;
using System.Collections;

public class FallingBlockScript : MonoBehaviour {

    public float Lifetime = 300.0f;
    public float DestroyAfter = 310.0f;
    public float ShakeTime = 2.0f;
    public float MinShakeMagnitude = 0.01f;
    public float MaxShakeMagnitude = 0.1f;
    public float MaxFallOffset = 5.0f;

    private float m_LifeSpent = 0.0f;
    private float m_CurrentShakeMag = 0.0f;
    private Vector3 m_StartPosition;

    void Start()
    {
        float rangeAdjustment = Random.Range(0.01f, MaxFallOffset) - MaxFallOffset/2.0f;
        m_CurrentShakeMag = MinShakeMagnitude;
        Lifetime += rangeAdjustment;
        DestroyAfter += rangeAdjustment;
        m_StartPosition = this.transform.position;
    }

    // Creates a rumbling/shaking effect
    void Shake ()
    {
        Vector3 shakePosition = m_StartPosition;
        shakePosition.x += Random.Range(0.0f, m_CurrentShakeMag) - m_CurrentShakeMag/2.0f;
        shakePosition.y += Random.Range(0.0f, m_CurrentShakeMag) - m_CurrentShakeMag/2.0f;

        if (m_CurrentShakeMag < MaxShakeMagnitude) m_CurrentShakeMag += 0.01f * Time.deltaTime;
        
        Vector3 pos = this.transform.position;
        pos.x = shakePosition.x;
        pos.y = shakePosition.y;
        this.transform.position = pos;
    }

	// Update is called once per frame
	void Update () 
    {
        m_LifeSpent += Time.deltaTime;

        if (m_LifeSpent > Lifetime - ShakeTime)
        {
            Shake();
        }

        if (m_LifeSpent > Lifetime)
        {
            this.rigidbody.isKinematic = false;
            this.rigidbody.WakeUp();
        }
        if (m_LifeSpent > DestroyAfter)
        {
            Destroy(this.gameObject);
        }
	}
}
