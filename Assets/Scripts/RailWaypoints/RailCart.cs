using UnityEngine;
using System.Collections;

public class RailCart : MonoBehaviour 
{

	public StaticWaypoint StartingWaypoint;
	public float MaxSpeed;

	// Private attributes
	private bool m_RailCartActivated = false;
	private bool m_RailCartFinished = false;
	private float m_Velocity;
	private float m_MaxVelocity;
	private Vector2 m_Direction;
	private float m_PlayerInterpolation;

	// Private associations
	private StaticWaypoint m_CurrentWaypoint;
	private GameObject m_PlayerObject;

	// Use this for initialization
	void Start () {
		this.transform.position = StartingWaypoint.transform.position;
		m_CurrentWaypoint = StartingWaypoint;
		m_RailCartActivated = false;
		m_RailCartFinished = false;
		m_MaxVelocity = MaxSpeed;
		m_PlayerInterpolation = 0;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && m_RailCartActivated == false && m_RailCartFinished == false) 
		{
			print ("Trigger Entered: " + other.gameObject.name.ToString ());

			m_RailCartActivated = true;
			m_RailCartFinished = false;
			m_PlayerObject = other.gameObject;
			m_Velocity = other.gameObject.rigidbody2D.velocity.magnitude;
		}
	}

	// Update is called once per frame
	void Update() 
	{
		if (m_RailCartActivated == true && m_RailCartFinished == false) 
		{
			/*
			// Keep the player attached to this object.
			if(m_PlayerObject.transform.position != this.transform.position)
			{
				Vector3 pos = m_PlayerObject.transform.position;
				pos = Vector3.Lerp(m_PlayerObject.transform.position, this.transform.position, m_PlayerInterpolation+=1.0f);
				m_PlayerObject.transform.position = pos;
			}
			*/
			m_PlayerObject.transform.position = this.transform.position;

			if (m_Velocity < m_MaxVelocity) 
			{
				m_Velocity += 10.0f * Time.deltaTime;
			}

			Vector3 position = this.transform.position;
			position = Vector3.MoveTowards(this.transform.position, m_CurrentWaypoint.transform.position, m_Velocity * Time.deltaTime );
			this.transform.position = position;

			if(this.transform.position == m_CurrentWaypoint.transform.position)
			{
				if (m_CurrentWaypoint.IsFinalWaypoint == false)
				{
					m_CurrentWaypoint = m_CurrentWaypoint.ChildWaypoint;
				}
				else if( m_CurrentWaypoint.IsFinalWaypoint == true )
				{

					m_RailCartFinished = true;
					m_CurrentWaypoint = StartingWaypoint;
					this.transform.position = StartingWaypoint.transform.position;
				}
			}
		}

	}
}
