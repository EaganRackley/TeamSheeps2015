using UnityEngine;
using System.Collections;

/// <summary>
///  Script that can be applied to a child object that will forcibly update to a fixed angle (identity), ignoring the parent objects rotation.
///  e.g. This might be used for emote bubbles, or player tags that have the a player object as a parent.
/// </summary>
[RequireComponent( typeof(TextMesh) )]
public class PlayerLabel : MonoBehaviour 
{
    public Transform parentTransform;
    public Vector3 offsetFromParent = new Vector3(-0.70f, 0.18f, 0f);

	// Use this for initialization
	void Start () {
	
	}

    // Used to set the text of the name label.
    public void setText(string text)
    {
        TextMesh label = GetComponent<TextMesh>();
        label.text = text;
    }

	public TextMesh getTextMesh()
	{
		return this.GetComponent<TextMesh> ();
	}

    void Update()
    {
		if (parentTransform != null)
        {
            this.transform.position = parentTransform.position;
            this.transform.position += offsetFromParent;
        } 
        else
        {
            Destroy(this.gameObject);
        }
    }
 }
