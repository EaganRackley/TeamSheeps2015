using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromWhite : MonoBehaviour
{

    private bool mShowWhiteBg = false;

    private void FadeAlphaToTarget(float fadeSpeed, float targetAlpha)
    {
        Color currentColor = new Color();
        if (GetComponent<Image>()) currentColor = GetComponent<Image>().color;
        else if (GetComponent<Text>()) currentColor = GetComponent<Text>().color;
        if (currentColor.a < targetAlpha)
        {
            currentColor.a += fadeSpeed * Time.deltaTime;
            if (currentColor.a > targetAlpha) currentColor.a = targetAlpha;
        }
        else if (currentColor.a > targetAlpha)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            if (currentColor.a < targetAlpha) currentColor.a = targetAlpha;
        }
        if (GetComponent<Image>()) GetComponent<Image>().color = currentColor;
        else if (GetComponent<Text>()) GetComponent<Text>().color = currentColor;
    }

    public void Show()
    {
        mShowWhiteBg = true;
    }

    public void Hide()
    {
        mShowWhiteBg = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!mShowWhiteBg)
            FadeAlphaToTarget(0.5f, 0.0f);
        else
            FadeAlphaToTarget(0.5f, 1.0f);
    }
}
