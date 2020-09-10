using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitchCamera : MonoBehaviour
{
    private SmoothCamera2D m_camera;
    private bool m_triggered;
    public Transform NewTarget;
    public float NewFOV;
    public float FovSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = FindObjectOfType<SmoothCamera2D>();
        m_triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_camera.target = NewTarget;
            m_triggered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_triggered)
        {
            if(m_camera.GetComponent<Camera>())
            {
                Camera cam = m_camera.GetComponent<Camera>();
                float currentFOV = cam.fieldOfView;
                //currentFOV = Mathf.Lerp(currentFOV, NewFOV, Time.deltaTime / FovSpeed);
                currentFOV = Mathf.MoveTowards(currentFOV, NewFOV, Time.deltaTime * FovSpeed);
                cam.fieldOfView = currentFOV;
            }
        }
    }

    public bool IsFinished()
    {
        Camera cam = m_camera.GetComponent<Camera>();
        return (cam.fieldOfView == NewFOV);
    }

    public void Finish()
    {
        Camera cam = m_camera.GetComponent<Camera>();
        cam.fieldOfView = NewFOV;
    }
}
