using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class SickPlayer : MonoBehaviour {

    public const float SPEED_DECREASE_DELTA = 0.01f;
    public const float DECREASE_SPEED_EVERY = 2.00f; //in seconds
    private float speedDecreaseTimer = DECREASE_SPEED_EVERY;
    PlayerController playerComponent;
    public bool isVisible;


    // Use this for initialization
	void Start () {
        this.playerComponent = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (speedDecreaseTimer > 0)
        {
            // Time.deltaTime returns time elapsed since last frame drawn.
            this.speedDecreaseTimer -= Time.deltaTime;
        }
        else if (speedDecreaseTimer <= 0)
        {
            // Timer's run out, so decrease speed and reset timer.
            this.speedDecreaseTimer = DECREASE_SPEED_EVERY;
            playerComponent.speed -= SPEED_DECREASE_DELTA;
        }
        if (!InCameraView())
        {
            Destroy(this.gameObject);
        }
	}

    bool InCameraView()
    {
        Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(frustum, this.collider.bounds);
    }
}
