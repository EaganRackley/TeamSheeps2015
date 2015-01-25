using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour 
{
    Transform topLeft;
    Transform bottomRight;


    float gameStartTime;

    public float gameTime
    {
        get
        {
            return Time.time - this.gameStartTime;
        }
    }

    public GameObject fireflyPrefab;

    public delegate void EventFunction();
    public bool DEBUG = false;

    public struct TimedEvent
    {
        public EventFunction eventFunction;
        public float triggerTime;
    }

    public List<TimedEvent> eventList;

    void Awake()
    {
        this.eventList = new List<TimedEvent>();
        this.topLeft = GameObject.FindGameObjectWithTag("TopLeft").transform;
        this.bottomRight = GameObject.FindGameObjectWithTag("BottomRight").transform;

        //Add your own game events here!
        AddEvent(SpawnTwentyFireflies, 60);
        AddEvent(SpawnFiftyFireflies, 115);

        //TODO: Make this called when user enters game from start menu.
        StartGame();
    }

    public void StartGame()
    {
        gameStartTime = Time.time;
    }

    void AddEvent(EventFunction eventFunction, float eventTime)
    {
        TimedEvent newEvent = new TimedEvent();
        newEvent.eventFunction = eventFunction;
        newEvent.triggerTime = eventTime;
        this.eventList.Add(newEvent);
    }

    void SpawnTwentyFireflies()
    {
        _SpawnFireFlies(20);
    }

    void SpawnFiftyFireflies()
    {
        _SpawnFireFlies(50);
    }

    void _SpawnFireFlies(int numberOfFireFlies)
    {
            for (int i  = 0; i < numberOfFireFlies; i++)
            {
            Vector3 spawnPosition = new Vector3(Random.Range(topLeft.transform.position.x, bottomRight.transform.position.x),
                                                Random.Range(bottomRight.transform.position.y, topLeft.transform.position.y),
                                                -1f);
            // Don't spawn if the firefly would spawn in view of the camera
            Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            if(!GeometryUtility.TestPlanesAABB(frustum, new Bounds(spawnPosition, Vector3.one)))
            {
                Instantiate(fireflyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        for (int i = eventList.Count - 1; i >= 0; i--)
        {
            if (eventList[i].triggerTime <= this.gameTime)
            {
                eventList[i].eventFunction();
                eventList.RemoveAt(i);
            }
        }
    }

    void OnGUI()
    {
        if (DEBUG)
        {
            GUILayout.BeginArea(new Rect(0, 0, 100, 100));
            GUILayout.Label(gameTime.ToString());
            GUILayout.EndArea();
        }
    }

}
