using UnityEngine;
using System.Collections;

public class SpeedPowerup : AbstractPowerup
{
    public float speedIncrease = 500f;
    public float powerupDuration = 5f;

	public float fallFromZ = -10.0f;
	public float stopAtZ = -0.53f;
	public bool startZIsRandom = false;

	private float fallModifier = 30.0f;
	private float timeFalling = 0.0f;

    public override IEnumerator PowerupFunction(PlayerController player)
    {
        player.speed += speedIncrease;
        yield return new WaitForSeconds(powerupDuration);
        player.speed -= speedIncrease;
    }

	void Start()
	{
		fallModifier = Random.Range(20.0f, 40.0f);
		timeFalling = 0.0f;
		Vector3 pos = this.transform.position;
		if(startZIsRandom)
		{
			pos.z = Random.Range(stopAtZ, fallFromZ);
		}
		else
		{
			pos.z = fallFromZ;
		}

		this.transform.position = pos;
	}

	void Awake()
	{
		Start ();
	}
	
	void Update()
	{
		Vector3 pos = this.transform.position;
		Vector3 fallFrom = new Vector3(pos.x, pos.y, fallFromZ);
		Vector3 fallTo = new Vector3(pos.x, pos.y, stopAtZ);
		pos = Vector3.Lerp(fallFrom, fallTo, timeFalling);
		this.transform.position = pos;
		if(timeFalling < 1.0f )
		{
			timeFalling += Time.deltaTime / fallModifier;
			this.transform.Rotate(new Vector3(0.0f, 0.0f, timeFalling * 5.0f));
		}
	}

}
