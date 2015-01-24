using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    // Enum for directions the player is facing.
    // 
    //       N
    //    NW | NE
    //  W -- * -- E
    //    SW | SE
    //       S
    // 
    public enum FacingDirection {N, NE, E, SE, S, SW, W, NW};
    public delegate IEnumerator PlayerPowerup(PlayerController player);
    // Movement amount (for all movement directions).
    public float speed;
    // Default direction is North.
    public FacingDirection currentDirection = FacingDirection.N;
    
    public const SPEED_DECREASE_DELTA = 0.01f;
    public const DECREASE_SPEED_EVERY = 0.5f; //in seconds
    private float speedDecreaseTimer = DECREASE_SPEED_EVERY;
    public bool isSick;

    // Direction keycodes; to be set in the scene editor.
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    private Rigidbody body;
    public Light playerLight;

    // Called on load.
    void Awake() {
        this.body = GetComponent<Rigidbody>();
        this.playerLight = transform.GetComponentInChildren<Light>();
    }

    // Called once per timestep.
    void FixedUpdate() {

    }

    // Called once each frame.
    void Update(){
        if (isSick){
            if (speedDecreaseTimer > 0) {
                // Time.deltaTime returns time elapsed since last frame drawn.
                this.speedDecreaseTimer -= Time.deltaTime;
            }
            else if (speedDecreaseTimer <= 0) {
                // Timer's run out, so decrease speed and reset timer.
                this.speedDecreaseTimer = DECREASE_SPEED_EVERY;
                this.speed -= SPEED_DECREASE_DELTA;
            }
        }

        // If no buttons are pressed, velocity is 0.
        float velocity_x = 0f;
        float velocity_y = 0f;

        // Set x component of velocity.
        if (Input.GetKey(upKey)) {
            velocity_y += speed;
            this.currentDirection = FacingDirection.N;
        }
        else if (Input.GetKey(downKey)) {
            velocity_y -= speed;
            this.currentDirection = FacingDirection.S;
        }

        // Set x component of velocity.
        if (Input.GetKey(leftKey)) {
            velocity_x -= speed;
            this.currentDirection = FacingDirection.W;
        }
        else if (Input.GetKey(rightKey)) {
            velocity_x += speed;
            this.currentDirection = FacingDirection.E;
        }

        // Set the new velocity.
        this.body.velocity = new Vector3(velocity_x, velocity_y, 0f);
    }

    public void GetPowerup(PlayerPowerup powerupFunction)
    {
        StartCoroutine(powerupFunction(this));
    }
}
