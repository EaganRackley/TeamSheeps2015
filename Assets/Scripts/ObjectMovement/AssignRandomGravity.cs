using UnityEngine;
using System.Collections;

public class AssignRandomGravity : MonoBehaviour {

	public void SetRandomGravity()
	{
		float randomNumber = Random.Range (0.1f, 1.0f);
		this.rigidbody2D.gravityScale = randomNumber;
	}
}
