using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour
{
    public PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = Player.speed / Player.StartingSpeed;
        if (fillAmount > 1f) fillAmount = 1f;
        if (fillAmount < 0f) fillAmount = 0f;
        GetComponent<Image>().fillAmount = fillAmount;
    }
}
