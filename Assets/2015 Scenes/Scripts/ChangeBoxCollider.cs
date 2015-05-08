using UnityEngine;
using System.Collections;

[ExecuteInEditMode]  
public class ChangeBoxCollider : MonoBehaviour {
	
	/*Change the collider size in the editor.
	
	void Update () {
		BoxCollider b = this.gameObject.collider as BoxCollider;
		if(b != null)
		{
			b.size = new Vector3(0.9f, 0.9f, 0.9f);
		}
	}*/

	/*Change the object scale in the editor.
	void Update () {
		Vector3 comparisonVector = new Vector3(1.0f, 1.0f, 1.0f);
		if(this.gameObject.transform.localScale != comparisonVector)
		{
			this.gameObject.transform.localScale = comparisonVector;
		}

	}*/
}