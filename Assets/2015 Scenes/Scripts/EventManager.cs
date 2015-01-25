using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
    Transform topLeft;
    Transform bottomRight;

    public GameObject fireflyPrefab;

    void Awake()
    {
        this.topLeft = GameObject.FindGameObjectWithTag("TopLeft").transform;
        this.bottomRight = GameObject.FindGameObjectWithTag("BottomRight").transform;
    }

    IEnumerator SpawnFireFlies(int numberOfFireFlies)
    {
            for (int i  = 0; i < numberOfFireFlies; i++)
            {
            Random.seed = i;
            Vector3 spawnPosition = new Vector3(Random.Range(topLeft.transform.position.x, bottomRight.transform.position.x),
                                                Random.Range(bottomRight.transform.position.y, topLeft.transform.position.y),
                                                -1f);
            Debug.Log("Spawning at " + spawnPosition);
            // Don't spawn if the firefly would spawn in view of the camera
            Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            if(!GeometryUtility.TestPlanesAABB(frustum, new Bounds(spawnPosition, Vector3.one)))
            {
                Instantiate(fireflyPrefab, spawnPosition, Quaternion.identity);
            }
            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SpawnFireFlies(100));
        }
    }
}
