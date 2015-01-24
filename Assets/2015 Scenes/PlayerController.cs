using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    enum FacingDirection {N, S, E, W};

    // Face North to start with.
    public FacingDirection currentDirection = FacingDirection.N;
    public float speed = 10f;

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
            this.body.transform.velocity = new Vector2(0, speed);
            this.currentDirection = FacingDirection.N;
        }
        else if (Input.GetButton("Down")) {
            this.body.transform.velocity = new Vector2(0, -speed);
            this.currentDirection = FacingDirection.S;
        }
        else if (Input.GetButton("Left")) {
            this.body.transform.velocity = new Vector2(-speed, 0);
            this.currentDirection = FacingDirection.W;
        }
        else if (Input.GetButton("Right")) {
            this.body.transform.velocity = new Vector2(speed, 0);
            this.currentDirection = FacingDirection.E;
        }
        else {
            this.body.transform.velocity = Vector2.zero;
        }
    }
}
