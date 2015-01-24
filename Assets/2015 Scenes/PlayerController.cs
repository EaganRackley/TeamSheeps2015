using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum FacingDirection {N, S, E, W};

    public float speed = 10f;
    // Default direction is North.
    public FacingDirection currentDirection = FacingDirection.N;

    // Direction keys.
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

        // Check for movement buttons; if none, set velocity to 0.
        // Moving in a direction sets your facing to that direction.
        if (Input.GetButton("Up")) {
            this.body.velocity = new Vector2(0, speed);
            this.currentDirection = FacingDirection.N;
        }
        else if (Input.GetButton("Down")) {
            this.body.velocity = new Vector2(0, -speed);
            this.currentDirection = FacingDirection.S;
        }
        else if (Input.GetButton("Left")) {
            this.body.velocity = new Vector2(-speed, 0);
            this.currentDirection = FacingDirection.W;
        }
        else if (Input.GetButton("Right")) {
            this.body.velocity = new Vector2(speed, 0);
            this.currentDirection = FacingDirection.E;
        }
        else {
            this.body.velocity = Vector2.zero;
        }
    }
}
