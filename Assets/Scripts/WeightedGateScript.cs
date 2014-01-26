using UnityEngine;
using System.Collections;

public class WeightedGateScript : MonoBehaviour 
{
	public CircleCollider2D ColliderA;
	public CircleCollider2D ColliderB;
	public CircleCollider2D ColliderC;
	public CircleCollider2D ColliderD;

	Start()
	{
	}

	void OnCollisionStay(Collider collider)
	{
		if (collider.gameObject.tag == "Player") 
		{
		}
	}


}
