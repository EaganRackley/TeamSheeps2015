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
using Sprites;
using Common.Math;
using Common.Observer;

/**
 * @class   WaypointScript
 * @brief   Handles all behavior associated with a waypoint ISprite implementation
 * @author  Eagan
 * @date    1/28/2012
 */
public class StaticWaypoint : MonoBehaviour, ISprite, ISubject
{
    public bool IsActive = false;
    public StaticWaypoint ChildWaypoint;
    private SpriteData mySpriteData;
    private Subject mySubject;
    private double myTimeSinceLastUpdate = 0.0f;
    private bool myWaypointIsActive = false;

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
     * @fn  public TestBaddie()
     * @brief   Initializes a new instance of the <see cref="TestBaddie"/> class.
     * @author  Eagan
     * @date    1/28/2012
     */
    public StaticWaypoint()
    {
        mySubject = new Subject(this);
    }

    /**
     * @fn  void Awake()
     * @brief   Triggered when the instance is first activated.
     * @author  Eagan
     * @date    1/28/2012
     */
    void Awake()
    {
        // Set up our sprite properties
        mySpriteData = SpriteMethods.InitSpriteData(mySpriteData);
        SetVelocities(0.0f, 0.0f, 0.0f, 0.0f);
        mySpriteData.SpriteType = SpriteType.StaticWaypoint;

        // Based on whether active is selected or not provide these guys with health. If the waypoint is active
        // then it will register with the targeting computer and the snake will navigate to the nearest one...
        myWaypointIsActive = IsActive;
        if (myWaypointIsActive == false)
        {
            //Debug.Log("Waypoint inactive!");
            this.Health = CommonValues.ACTIVE_WAYPOINT_HEALTH;
        }
        else
        {
            //Debug.Log("Waypoint active!");
            this.Health = CommonValues.ACTIVE_WAYPOINT_HEALTH;
        }
    }

    /**
     * @fn  void OnTriggerEnter(Collider other )
     * @brief   On trigger a waypoint de-activates and unregisters with the targeting computer,
     *          then activate method on its child waypoint.
     * @author  Eagan
     * @date    1/28/2012
     * @param   Collider    The collision.
     */
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Triggered! Dead is " + mySpriteData.Dead.ToString() + " tag is " + other.gameObject.tag);

        if (mySpriteData.Dead == false)
        {
            // If a snake object collides with our Waypoint then set the health to 0 (killing it) which will cause it to be unregistered
            // with the targeting computer until a parent Waypoint enables it again...
            if (other.gameObject.tag == CommonValues.OROBOROS_TAG)
            {
                //Debug.Log("Collision was Oroboros!");
                this.Health = CommonValues.ACTIVE_WAYPOINT_HEALTH;
                ChildWaypoint.Health = CommonValues.ACTIVE_WAYPOINT_HEALTH;
            }
        }
    }

    /**
     * @fn  void Update()
     * @brief   Updates the instances of this waypoint to handle movement etc. if it's going to change position
     * @author  Eagan
     * @date    1/28/2012
     */
    void Update()
    {
        if (mySpriteData.Dead == false)
        {
            // Load the targeting computer and find the angle to the nearest target
            //TargetingComputer targetComputer = TargetingComputer.getSingleInstance();
            //mySpriteData.Angle = targetComputer.GetAngleToNearestTarget(transform.position, transform.forward);

            // Rotate according to the angle specified to the nearest object (rotating around the Y angle)
            //this.transform.Rotate(new Vector3(0.0f, 270.0f, 0.0f));
            //transform.Rotate(Vector3.right, mySpriteData.Angle, Space.World);

            // Perodically update our observers in the case of a location change...
            myTimeSinceLastUpdate += Time.deltaTime;
            if ((myTimeSinceLastUpdate >= CommonValues.UPDATE_POSITION_DELAY))
            {
                myTimeSinceLastUpdate = 0.0f;
                notifyObservers(this.Location);
            }

            // Handle any velocity changes and update our position to reflect them
            //mySpriteData = SpriteMethods.ProcessVelocity(mySpriteData);
            transform.position = SpriteMethods.UpdateLocationByVelocity(mySpriteData, transform.position, Time.deltaTime);
        }
    }

    /**
     * @fn  public void SetVelocities(float minVelocity, float maxNormalVelocity, float dragVelocity,
     *      float accelerationVelocity)
     * @brief   Sets common velocity values for the sprite.
     * @author  Eagan
     * @date    1/28/2012
     * @param   minVelocity             The minimum velocity.
     * @param   maxNormalVelocity       The maximum units per second velocity a sprite can achieve
     *                                  during normal movement.
     * @param   dragVelocity            The amount of units per second drag on the sprite when
     *                                  accelerating and decelerating.
     * @param   accelerationVelocity    The units per second at which acceleration is increased.
     */
    public void SetVelocities(float minVelocity, float maxNormalVelocity, float dragVelocity, float accelerationVelocity)
    {
        mySpriteData = SpriteMethods.SetVelocities(mySpriteData, minVelocity, maxNormalVelocity, dragVelocity, accelerationVelocity);
    }

    /**
     * @property    public int ID
     * @brief   Gets or sets the ID used to reference the sprite.
     * @return  The identifier.
     * @value    Integer value representing sprite ID.
     */
    public int ID
    {
        get
        {
            return mySpriteData.ID;
        }
        set
        {
            mySpriteData.ID = value;
        }
    }

    /**
     * @property    public SpriteType Type
     * @brief   Gets the type of the sprite.
     * @return  The type.
     * ### value    The type of the sprite.
     */
    public SpriteType Type
    {
        get
        {
            return mySpriteData.SpriteType;
        }
    }

    /**
     * @property    public bool IsDead
     * @brief   Gets a value indicating whether the sprite is dead.
     * @return  true if this object is dead, false if not.
     * ### value    <c>true</c> if this instance is dead; otherwise, <c>false</c>.
     */
    public bool IsDead
    {
        get
        {
            return mySpriteData.Dead;
        }
    }

    /**
     * @property    public SpriteDirection Direction
     * @brief   Gets or sets the direction.
     * @return  The direction.
     * ### value    The direction the sprite is currently facing.
     */
    public SpriteDirection Direction
    {
        get
        {
            return mySpriteData.Direction;
        }
        set
        {
            mySpriteData.Direction = value;
        }
    }

    /**
     * @property    public float ControlMagnitude
     * @brief   Gets or sets the Control Magnitude which can affect the maximum speed of the sprite.
     *          This can be used with analog joysticks or maybe adjusted if a sprite is damaged in
     *          some way (e.g. can only move at .75 of it's maximum velocity)
     *          another example might be traveling through a stick substance can reduce it's control
     *          magnitude.
     * @return  The control magnitude.
     * ### value    The control magnitude.
     */
    public float ControlMagnitude
    {
        get
        {
            return mySpriteData.ControlMagnitude;
        }
        set
        {
            mySpriteData.ControlMagnitude = value;
        }
    }

    /**
     * @property    public float ControlAngle
     * @brief   Gets or sets the control angle which affects the intended movement of the sprite
     *          (e.g. ControlAngle = 90 and sprite is at 40, sprite will change angles until it
     *          reaches 90)
     * @return  The control angle.
     * ### value    The control angle.
     */
    public float ControlAngle
    {
        get
        {
            return mySpriteData.ControlAngle;
        }
        set
        {
            mySpriteData.ControlAngle = value;
        }
    }

    /**
     * @property    public float Health
     * @brief   Gets or sets the health of the Sprite.
     * @return  The health.
     * ### value    A float representing the health of the sprite.
     */
    public float Health
    {
        get
        {
            mySpriteData = SpriteMethods.EvaluateSpriteHealth(mySpriteData);
            if (mySpriteData.Dead == true)
            {
                this.notifyObservers(this.Location);
                this.removeAllObservers();
            }
            return mySpriteData.Health;
        }

        set
        {
            mySpriteData.Health = value;
            mySpriteData = SpriteMethods.EvaluateSpriteHealth(mySpriteData);
            //Debug.Log("Activating Waypoint: " + mySpriteData.Dead.ToString());
            if (mySpriteData.Dead == false)
            {
                // Load the targeting computer and register it as an observer of this object (since we're now alive again, yay!)
                TargetingComputer targetComputer = TargetingComputer.getSingleInstance();
                mySpriteData.ID = targetComputer.GetUniqueSpriteID();
                this.registerObserver(targetComputer);
            }
            else
            {
                this.notifyObservers(this.Location);
                this.removeAllObservers();

            }
        }
    }

    /**
     * @property    public float Angle
     * @brief   Gets or sets the current angle indicating the direction of the sprite (e.g. 90 or 270
     *          for a Player, any angle for a bullet)
     * @return  The angle.
     * ### value    The angle.
     */
    public float Angle
    {
        get
        {
            return mySpriteData.Angle;
        }
        set
        {
            mySpriteData.Angle = value;
        }
    }

    /**
     * @property    public float MaxHealth
     * @brief   Gets or sets the maximum amount of health a sprite can have.
     * @return  The maximum health.
     * ### value    A float representing maximum amount of health a sprite can have.
     */
    public float MaxHealth
    {
        get
        {
            return mySpriteData.MaxHealth;
        }
        set
        {
            mySpriteData.MaxHealth = value;
        }
    }

    /**
     * @property    public int PointValue
     * @brief   Gets or sets the point value for the Sprite (e.g. The points you gain when you
     *          destroy it),.
     * @return  The point value.
     * ### value    The point value for the Sprite (e.g. The points you gain when you destroy it).
     */
    public int PointValue
    {
        get
        {
            return mySpriteData.PointValue;
        }
        set
        {
            mySpriteData.PointValue = value;
        }
    }

    /**
     * @property    public Vector3 Location
     * @brief   Gets or sets the current location of the sprite;
     * @return  The location.
     */
    public Vector3 Location
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.Location = value;
        }
    }

    /**
     * @property    public Vector3 CurrentVelocity
     * @brief   Gets or sets the current velocity of the Sprite.
     * @return  The current velocity.
     * ### value    The current velocity of the Sprite.
     */
    public Vector3 CurrentVelocity
    {
        get
        {
            return mySpriteData.Velocity;
        }
        set
        {
            mySpriteData.Velocity = value;
        }
    }

    /**
     * @fn  public void HandleJump(float jumpForace)
     * @brief   Handle jump.
     * @author  Eagan
     * @date    1/28/2012
     * @exception   NotImplementedException Thrown when the requested operation is unimplemented.
     * @param   jumpForace  The jump forace.
     */
    public void HandleJump(float jumpForace)
    {
        throw new System.NotImplementedException();
    }

    /**
     * @fn  public void IncreaseAcceleartion()
     * @brief   Increase acceleartion.
     * @author  Eagan
     * @date    1/28/2012
     * @exception   NotImplementedException Thrown when the requested operation is unimplemented.
     */
    public void IncreaseAcceleartion()
    {
        throw new System.NotImplementedException();
    }

    /**
     * @fn  public void ProcessVelocity()
     * @brief   Handles processing of velocity information.
     * @author  Eagan
     * @date    1/28/2012
     */
    public void ProcessVelocity()
    {
        // adjust our angle in radians so that we can change our movement...
        mySpriteData = SpriteMethods.ProcessVelocity(mySpriteData);
    }

    /**
     * @fn  public void UpdateLocationByVelocity()
     * @brief   Updates the location of the Sprite by the current velocity.
     * @author  Eagan
     * @date    1/28/2012
     */
    public void UpdateLocationByVelocity()
    {
        // adjust our angle in radians so that we can change our movement...
        Vector3 newLocation = SpriteMethods.UpdateLocationByVelocity(mySpriteData, this.Location, Time.deltaTime);
        this.Location = newLocation;
    }

    /**
     * @fn  public void registerObserver(IObserver observer)
     * @brief   Registers the observer.
     * @author  Eagan
     * @date    1/28/2012
     * @param   observer    The observer.
     */
    public void registerObserver(IObserver observer)
    {
        mySubject.registerObserver(observer);
    }

    /**
     * @fn  public void removeObserver(IObserver observer)
     * @brief   Removes the observer.
     * @author  Eagan
     * @date    1/28/2012
     * @param   observer    The observer.
     */
    public void removeObserver(IObserver observer)
    {
        mySubject.removeObserver(observer);
    }

    /**
     * @fn  public bool containsObserver(IObserver observer)
     * @brief   Determines whether the specified subject contains observer.
     * @author  Eagan
     * @date    1/28/2012
     * @param   observer    The observer.
     * @return  <c>true</c> if the specified subject contains observer; otherwise, <c>false</c>.
     */
    public bool containsObserver(IObserver observer)
    {
        return mySubject.containsObserver(observer);
    }

    /**
     * @fn  public void notifyObservers(object arg)
     * @brief   Notifies the observers.
     * @author  Eagan
     * @date    1/28/2012
     * @param   arg .
     */
    public void notifyObservers(object arg)
    {
        mySubject.notifyObservers(arg);
    }

    /**
     * @fn  public void removeAllObservers()
     * @brief   Unregisters all observer objects to verify that there are no references to other
     *          objects...
     * @author  Eagan
     * @date    1/28/2012
     */
    public void removeAllObservers()
    {
        mySubject.removeAllObservers();
    }

}
