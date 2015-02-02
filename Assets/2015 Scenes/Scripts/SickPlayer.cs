using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class SickPlayer : MonoBehaviour {

    public const float SPEED_DECREASE_DELTA = 0.01f;
    public const float DECREASE_SPEED_EVERY = 4.00f; //in seconds
    private float speedDecreaseTimer = DECREASE_SPEED_EVERY;
    PlayerController playerComponent;
    public bool isVisible;
	public GameObject prefabToSpawnOnDeath;


    // Use this for initialization
	void Start () {
        this.playerComponent = GetComponent<PlayerController>();
	}

	/// <summary>
	/// Method triggers the prefabToSpawnOnDeath object to be instantiated randomly.
	/// Used when sick player leaves camera space, @see Update() -> if(!InCameraView()).
	/// </summary>
	void SpawnPrefabsOnDeath()
	{

		Vector3 pos = this.transform.position;
		for(int i = 0; i < 10; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(pos.x-0.27f, pos.x+0.27f),
		                                    Random.Range(pos.y-0.27f, pos.y+0.27f),
		                                    -1f);
			Instantiate(prefabToSpawnOnDeath, spawnPosition, Quaternion.identity);
		}
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
			SpawnPrefabsOnDeath();
			Destroy(this.gameObject);
        }
	}

    bool InCameraView()
    {
        Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(frustum, this.collider.bounds);
    }
}
