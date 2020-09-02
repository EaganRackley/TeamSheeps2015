using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForMeFollow : MonoBehaviour
{
    public PlayerController PlayerToFollow;
    public Vector3 Offset;

    private SpriteRenderer m_renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        Color hideAlphaColor = m_renderer.material.color;
        hideAlphaColor.a = 0f;
        m_renderer.material.color = hideAlphaColor;
    }

    Color AdjustColorAlpha(Color color, float fadeSpeed)
    {
        Color newColor = color;
        newColor.a += fadeSpeed * Time.deltaTime;
        if (fadeSpeed > 0 && newColor.a >= 1f)
        {
            newColor.a = 1f;
        }
        else if (fadeSpeed <= 0 && newColor.a <= 0f)
        {
            newColor.a = 0f;
        }

        return newColor;
    }

    // Update is called once per frame
    void Update()
    {
        // If not following, fade graphic out (we will start there)
        if (!PlayerToFollow.Following || !PlayerToFollow.ShowingDialog)
        {
            if (m_renderer.material.color.a > 0f)
            {
                m_renderer.material.color = AdjustColorAlpha(m_renderer.material.color, -0.5f);
            }
        }
        // If following, fade graphic in
        else //if (PlayerToFollow.Following)
        {
            if (m_renderer.material.color.a < 1f)
            {
                m_renderer.material.color = AdjustColorAlpha(m_renderer.material.color, 0.5f);
            }
        }
        if(PlayerToFollow != null)
            this.transform.position = PlayerToFollow.transform.position - Offset;
    }
}
