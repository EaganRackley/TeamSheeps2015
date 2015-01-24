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

    public float speed = 10f;
    // Default direction is North.
    public FacingDirection currentDirection = FacingDirection.N;

    // Direction keycodes; to be set in the scene editor.
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    private Rigidbody2D body;

    // Called on load.
    void Awake() {
        this.body = GetComponent<Rigidbody2D>();
    }

    // Called once each frame.
    void Update(){
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

        // Set the FacingDirection according to buttons pressed.
        if (velocity_x < 0f) {
            if (velocity_y > 0f) {
                this.currentDirection = FacingDirection.NW;
            }
            else if (velocity_y == 0f) {
                this.currentDirection = FacingDirection.W;
            }
            else if (velocity_y < 0f) {
                this.currentDirection = FacingDirection.SW;
            }
        }
        else if (velocity_x == 0f) {
            if (velocity_y > 0f) {
                this.currentDirection = FacingDirection.W;
            }
            else if (velocity_y == 0f) {
                this.currentDirection = 0f;
            }
            else if (velocity_y < 0f) {
                this.currentDirection = FacingDirection.S;
            }
        }
        else if (velocity_x > 0f) {
            if (velocity_y > 0f) {
                this.currentDirection = FacingDirection.NE;
            }
            else if (velocity_y == 0f) {
                this.currentDirection = FacingDirection.E;
            }
            else if (velocity_y < 0f) {
                this.currentDirection = FacingDirection.SE;
            }
        }

        body.velocity = new Vector2(velocity_x, velocity_y);
    }
}
