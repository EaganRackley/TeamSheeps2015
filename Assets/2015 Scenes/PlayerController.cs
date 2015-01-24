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

    // Movement amount (for all movement directions).
    public float speed = 10f;
    // Default direction is North.
    public FacingDirection currentDirection = FacingDirection.N;
    
    public const speedDecreaseDelta = 0.01f;
    public bool isSick;
    public 

    // Direction keycodes; to be set in the scene editor.
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    private Rigidbody3D body;

    // Called on load.
    void Awake() {
        this.body = GetComponent<Rigidbody2D>();
    }

    // Called once per timestep.
    void FixedUpdate() {

    }

    // Called once each frame.
    void Update(){
        Time.deltaTime

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
        body.velocity = new Vector3(velocity_x, velocity_y, 0f);
    }
}
