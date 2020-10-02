using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFireflies : MonoBehaviour
{
    private ZippyMover[] m_fireflies;
    private List<ZippyMover> m_taggedZippies;
    private PlayerController m_Controller;
    public string MatchingTag = "Firefly";
    private float m_countDown;
    // Start is called before the first frame update
    void Start()
    {
        m_countDown = 5f;
        m_taggedZippies = new List<ZippyMover>();
        m_fireflies = FindObjectsOfType<ZippyMover>();
        m_Controller = GetComponent<PlayerController>();
        foreach (ZippyMover mover in m_fireflies)
        {
            if(mover.gameObject.tag == MatchingTag)
            {
                m_taggedZippies.Add(mover);
            }
        }
        m_Controller.TransformToFollow = m_taggedZippies[0].transform;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == MatchingTag)
    //    {
    //        m_Controller.TransformToFollow = m_taggedZippies.Dequeue().transform;
    //        GameObject.Destroy(other.gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        m_countDown -= Time.deltaTime;
        if(m_countDown <= 0f)
        {
            m_countDown = 5f;
            if(m_taggedZippies.Count > 0)
            { 
                int index = Random.Range(0, m_taggedZippies.Count - 1);
                this.GetComponent<PlayerController>().TransformToFollow = m_taggedZippies[index].transform;
            }
        }
    }
}
