using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EventManager : MonoBehaviour 
{
    public float LifeSpentOffset = 0f;
    public TextMeshProUGUI ScoreBoard;
    
    Transform topLeft;
    Transform bottomRight;
	
    float gameStartTime;

    float m_lifeSpent;
    private int m_score;
    public int Score
    {
        get { return m_score; }
        set { m_score = value; }
    }
    public float lifeSpent
    {
        get
        {
            return m_lifeSpent;
        }
    }

    public float gameTime
    {
        get
        {
            return Time.time - this.gameStartTime;
        }
    }

    public GameObject fireflyPrefab;
	public GameObject butterflyPrefab;
	public GameObject speedPrefab;
	public GameObject lanternPrefab;

    public delegate void EventFunction();
    public bool DEBUG = false;

    public struct TimedEvent
    {
        public EventFunction eventFunction;
        public float triggerTime;
    }

    public List<TimedEvent> eventList;

    void Start()
    {
        // Turn off the cursor for the entire application
        Cursor.visible = false;
        m_lifeSpent = LifeSpentOffset;
        eventList = new List<TimedEvent>();
    }

    void Awake()
    {
        this.topLeft = GameObject.FindGameObjectWithTag("TopLeft").transform;
        this.bottomRight = GameObject.FindGameObjectWithTag("BottomRight").transform;

        //Add your own game events here!
		//AddEvent(SpawnTwentyButterflies,20);
		//AddEvent(SpawnSpeedPowerUps,    30);
		//AddEvent(SpawnLanternPowerUps,  30);
        //AddEvent(SpawnTwentyFireflies,  60);
        //AddEvent(SpawnFiftyFireflies,  115);
		//AddEvent(SpawnSpeedPowerUps,   120);
		//AddEvent(SpawnLanternPowerUps, 120);

        //TODO: Make this called when user enters game from start menu.
        StartGame();
    }

    public void StartGame()
    {
        gameStartTime = Time.time;
    }

    void AddEvent(EventFunction eventFunction, float eventTime)
    {
        if (eventList == null)
        {
            //print("EVENT LIST IS NULL OMGZ!");
            return;
        }
        TimedEvent newEvent = new TimedEvent();
        newEvent.eventFunction = eventFunction;
        newEvent.triggerTime = eventTime;
        eventList.Add(newEvent);
    }

    void SpawnTwentyFireflies()
    {
		_SpawnPrefab(this.fireflyPrefab, 20);
    }

    void SpawnFiftyFireflies()
    {
        _SpawnPrefab(this.fireflyPrefab, 50);
    }

	void SpawnTwentyButterflies()
	{
		_SpawnPrefab(this.butterflyPrefab, 20);
	}

	void SpawnSpeedPowerUps()
	{
		_SpawnPrefab(this.speedPrefab, 100);
	}

	void SpawnLanternPowerUps()
	{
		_SpawnPrefab(this.lanternPrefab, 40);
	}

	void _SpawnPrefab(GameObject prefab, int number)
	{
		for (int i  = 0; i < number; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(topLeft.transform.position.x, bottomRight.transform.position.x),
			                                    Random.Range(bottomRight.transform.position.y, topLeft.transform.position.y),
			                                    -1f);
			// Don't spawn if the prefab would spawn in view of the camera
			Plane[] frustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
			if(!GeometryUtility.TestPlanesAABB(frustum, new Bounds(spawnPosition, Vector3.one)))
			{
				Instantiate(prefab, spawnPosition, Quaternion.identity);
			}
		}
	}

    void Update()
    {
        m_lifeSpent += Time.deltaTime;

        if(ScoreBoard)
            ScoreBoard.text = Score.ToString();

        if (eventList == null)
        {
            //print("EVENT LIST IS NULL OMGZ!");
            return;
        }

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
