using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightLerper : MonoBehaviour {

    [System.Serializable]
    public struct TimeWithColor
    {
        public Color color;
        public float time;
    }

    public List<TimeWithColor> colorsList;
    Color currentColor;
    Color nextColor;
    int currentColorIndex;
    float timeTillNextColor;

    void Awake()
    {
        if (colorsList.Count == 0)
        {
            Debug.Log("Please load your colors list with some colors");
        }
        this.currentColorIndex = 0;
        this.timeTillNextColor = colorsList[0].time;
        this.currentColor = colorsList[currentColorIndex].color;
        this.nextColor = colorsList[currentColorIndex + 1].color;
    }

    void Update()
    {
        if (currentColorIndex >= colorsList.Count)
        {
            currentColorIndex = colorsList.Count - 1;
        }

        timeTillNextColor -= Time.deltaTime;
        if (timeTillNextColor <= 0f)
        {
            currentColorIndex += 1;
            if (currentColorIndex == colorsList.Count)
                return;
            // If we're at the end of the list, fade to black
            if (currentColorIndex == colorsList.Count - 1)
            {
                timeTillNextColor = colorsList[currentColorIndex].time;
                this.currentColor = colorsList[currentColorIndex].color;
                this.nextColor = Color.black;
            }
            else
            {
                timeTillNextColor = colorsList[currentColorIndex].time;
                currentColor = colorsList[currentColorIndex].color;
                nextColor = colorsList[currentColorIndex + 1].color;
            }
        }
        RenderSettings.ambientLight = Color.Lerp(currentColor, nextColor, 1f - timeTillNextColor / colorsList[currentColorIndex].time);
    }
}
