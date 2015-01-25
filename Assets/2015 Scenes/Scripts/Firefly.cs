using UnityEngine;
using System.Collections;

public class Firefly : MonoBehaviour {

    public float onTime;
    public float offTime;
    public float randomVariance;

    public float moveSpeed;

    public float timePerTurningDirection;
    public float randomTimePerTurningDirection;
    public float turnSpeed;
    public float randomTurnSpeed;
    bool turningRight = false;

    float turningTimer;

    const float UPDOWN_RANGE = 0.5f;
    const float SECONDS_PER_CYCLE = 5f;
    float updownTimer;
    float initialHeight;

    float currentDirection = 0f;

    float lightCountdown;
    bool lightOn;

    Light fireflyLight;

    void Awake()
    {
        fireflyLight = GetComponentInChildren<Light>();
        initialHeight = transform.position.z;
    }

    void Start()
    {
        if (Random.Range(0, 1) > 0)
        {
            this.lightOn = true;
            lightCountdown = onTime + Random.Range(0f, randomVariance);
        }
        else
        {
            this.lightOn = false;
            lightCountdown = offTime + Random.Range(0f, randomVariance);
        }
    }

    void Update()
    {
        lightCountdown -= Time.deltaTime;
        if (lightCountdown <= 0f)
        {
            if (this.lightOn)
            {
                this.lightOn = false;
                lightCountdown = offTime + Random.Range(0f, randomVariance);
            }
            else
            {
                this.lightOn = true;
                lightCountdown = onTime + Random.Range(0f, randomVariance);
            }
        }
        UpdateLight();
        MoveUpDown();
        MoveAround();
    }

    void MoveAround()
    {
        this.turningTimer -= Time.deltaTime;
        if (turningTimer <= 0f)
        {
            this.turningTimer = timePerTurningDirection + Random.Range(0f, randomTimePerTurningDirection);
            this.turningRight = !this.turningRight;
        }
        if (this.turningRight)
        {
            currentDirection -= turnSpeed + Random.Range(0f, randomTurnSpeed);
        }
        else
        {
            currentDirection += turnSpeed + Random.Range(0f, randomTurnSpeed);
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentDirection);
        Vector2 movementVector = new Vector2(Mathf.Cos(currentDirection * Mathf.Deg2Rad), Mathf.Sin(currentDirection * Mathf.Deg2Rad));
        transform.position += new Vector3(movementVector.x, movementVector.y, 0f) * moveSpeed * Time.deltaTime;
    }

    void UpdateLight()
    {
        if (this.lightOn)
            fireflyLight.enabled = true;
        else
            fireflyLight.enabled = false;
    }

    void MoveUpDown()
    {
        updownTimer += Time.deltaTime * (1f / SECONDS_PER_CYCLE);
        if (updownTimer > 1f)
            updownTimer -= 1f;
        transform.position = new Vector3(transform.position.x, transform.position.y, initialHeight + Mathf.Sin(updownTimer * 2 * Mathf.PI) * UPDOWN_RANGE);
    }
}
