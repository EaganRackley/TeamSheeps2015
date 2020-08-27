using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class SickPlayer : MonoBehaviour {

    public const float SPEED_DECREASE_DELTA = 0.01f;
    public const float DECREASE_SPEED_EVERY = 1.00f; //in seconds
    private float speedDecreaseTimer = DECREASE_SPEED_EVERY;
    PlayerController playerComponent;
    public bool isVisible;
	// This prefab will spawn randomly around the now dead character.
	public GameObject prefabToSpawnOnDeath;
	// This prefab will spawn where the player was on death (matching player rotation at time of death)
	public GameObject playerDeathShroud;
    private float m_startingSpeed;
    private int m_offScreenCount;
    private float m_debounceOffscreenTimer;


    // Use this for initialization
    void Start () {
        m_offScreenCount = 0;
        m_debounceOffscreenTimer = 0f;
        this.playerComponent = GetComponent<PlayerController>();
        this.playerComponent.speed = this.playerComponent.speed * 0.75f;
        m_startingSpeed = this.playerComponent.speed;
    }

    /// <summary>
    /// Method triggers the prefabToSpawnOnDeath object to be instantiated randomly.
    /// Used when sick player leaves camera space, @see Update() -> if(!InCameraView()).
    /// and handles spawning the player deathshroud based at the player rotation
    /// </summary>
    void SpawnPrefabsOnDeath()
	{
		Instantiate(playerDeathShroud, this.transform.position, this.transform.rotation);

		Vector3 pos = this.transform.position;

		for(int i = 0; i < 10; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(pos.x-0.27f, pos.x+0.27f),
		                                    Random.Range(pos.y-0.27f, pos.y+0.27f),
			                                pos.z - Random.Range(0.8f, 1.25f));
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
            if(playerComponent.speed > 0.05f)
            {
                playerComponent.speed -= SPEED_DECREASE_DELTA;
            }

            // Have our sick player sprite fade as it gets sicker and sicker.
            if (playerComponent.speed <= m_startingSpeed)
            {
                Color col = GetComponent<SpriteRenderer>().color;
                col.a = playerComponent.speed > 0f ? (playerComponent.speed / m_startingSpeed) : 0f;
                GetComponent<SpriteRenderer>().color = col;
            }

            //if(playerComponent.speed <= 2f)
            //{
            //    Color col = GetComponent<SpriteRenderer>().color;
            //    col.a = playerComponent.speed > 0f ? playerComponent.speed : 0f;
            //    GetComponent<SpriteRenderer>().color = col;
            //}
        }
        if (!InCameraView())
        {
            if (!playerComponent.Following && m_offScreenCount < 3)
            {
                playerComponent.GetPowerup(this.PowerupFunction);
                m_offScreenCount++;
            }
            else if(!playerComponent.Following && m_offScreenCount >= 3)
            { 
                SpawnPrefabsOnDeath();
                Destroy(this.gameObject);
            }
        }
	}
    public IEnumerator PowerupFunction(PlayerController player)
    {
        player.Following = true;
        player.speed += 1f;
        yield return new WaitForSeconds(3f);
        player.speed -= 1f;
        player.Following = false;
    }

    bool InCameraView()
    {
        Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(frustum, this.GetComponent<Collider>().bounds);
    }
}
