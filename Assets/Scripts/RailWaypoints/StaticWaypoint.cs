/**
* @file StaticWaypoint.cs
* @brief Handles all behavior associated with a waypoint ISprite implementation
* @details
* Sample usage:<br>
* Example usage
* @code
* // TODO: Create sample documentation
* @endcode  
* @since    1.0.0
* @version  1.0.0
* @package  Oroboros
*/
using UnityEngine;
using System.Collections;

/**
 * @class   WaypointScript
 * @brief   Handles all behavior associated with a waypoint ISprite implementation
 * @author  Eagan
 * @date    1/28/2012
 */
public class StaticWaypoint : MonoBehaviour
{
    public bool IsActive = false;
	public bool IsStartingWaypoint = false;
	public bool IsFinalWaypoint = false;
    public StaticWaypoint ChildWaypoint;
    private double myTimeSinceLastUpdate = 0.0f;

    /**
     * @fn  void OnDrawGizmos()
     * @brief   Draws a gizmo representing a waypoint in the application.
     * @author  Eagan
     * @date    1/28/2012
     */
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, CommonValues.WAYPOINT_IMAGE);
        Gizmos.DrawLine(transform.position, ChildWaypoint.transform.position);
		Vector3 position = transform.position;
		position.z = 0.0f;
		transform.position = position;		
    }

    /**
     * @fn  void Awake()
     * @brief   Triggered when the instance is first activated.
     * @author  Eagan
     * @date    1/28/2012
     */
    void Awake()
    {        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Triggered! Dead is " + mySpriteData.Dead.ToString() + " tag is " + other.gameObject.tag);
	}

    /**
     * @fn  void Update()
     * @brief   Updates the instances of this waypoint to handle movement etc. if it's going to change position
     * @author  Eagan
     * @date    1/28/2012
     */
    void Update()
    {
    }

}
