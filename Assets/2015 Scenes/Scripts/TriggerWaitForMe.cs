using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWaitForMe : MonoBehaviour
{
    public PlayerController player2;
    private float m_totalFollowTime = 4f;
    private bool m_triggered = false;
    public float FollowTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        m_triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !m_triggered)
        {
            m_triggered = true;
            HavePlayer2FollowPlayer1();
        }
    }

    public void HavePlayer2FollowPlayer1()
    {
            player2.Following = true;
            m_totalFollowTime = FollowTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(player2.Following)
        {
            m_totalFollowTime -= Time.deltaTime;
            if(m_totalFollowTime <= 0f)
            {
                player2.Following = false;
                GameObject.Destroy(this);
            }
        }
    }
}
