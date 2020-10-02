using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGame : MonoBehaviour {

    public sound music;
    public SpriteRenderer WhiteQuad;
    public float TriggerLoadAfter = 120f;
    private bool isLoadingNewScene = false;
    private AsyncOperation async;
    private float myLoadingTimer = 0f;
    private float myWaitForFinishedTimer = 0f;
    private float myTriggerLoadTimer = 0f;
    private bool m_fireWasPressed = false;

    public TriggerSwitchCamera TriggerCamera;
    public TriggerFadeObjects[] TriggerFades;

    void Start()
    {
        // Set both materials to transparent so we can play the game.
        Color newColor = WhiteQuad.material.color;
        newColor.a = 1;
        WhiteQuad.material.color = newColor;

        myLoadingTimer = 0f;
        myTriggerLoadTimer = 0f;
        music.increaseVolume(0.5f);
        m_fireWasPressed = false;
    }

    void FinishAllTransitions()
    {
        TriggerCamera.Finish();
        foreach (TriggerFadeObjects obj in TriggerFades)
        {
            // For each object that has one or more of these trigger scripts, make sure we call finish on each.
            TriggerFadeObjects[] objects = obj.gameObject.GetComponents<TriggerFadeObjects>();
            foreach (TriggerFadeObjects fo in objects)
            {
                fo.Finish();
            }
        }
    }

    bool TransitionsAreFinished()
    {
        bool returnValue = true;

        if (!TriggerCamera.IsFinished())
        {
            returnValue = false;
        }
        
        foreach (TriggerFadeObjects obj in TriggerFades)
        {
            TriggerFadeObjects[] objects = obj.gameObject.GetComponents<TriggerFadeObjects>();
            foreach (TriggerFadeObjects fo in objects)
            {
                if (!fo.IsFinished())
                    returnValue = false;
            }
        }

        return returnValue;
    }

    //private IEnumerator LoadLevelAsync()
    //{
    //    var async = SceneManager.LoadSceneAsync("Demo2017");
    //    async.allowSceneActivation = false;
    //    while(!async.isDone)
    //    {
    //        Debug.Log("Progress:" + async.progress.ToString());
    //        if (async.progress == 0.9f)
    //        {
    //            Debug.Log("Loading done - ACTIVATE SCENE");
    //            yield return new WaitForSeconds(3.0f);
    //            async.allowSceneActivation = true;
    //        }
    //        yield return async;
    //    }
    //    yield return async;
    //}

    void HandledWhiteFadeOut()
    {
        if (WhiteQuad.material.color.a > 0f)
        {
            // Set both materials to transparent so we can play the game.
            Color newColor = WhiteQuad.material.color;
            newColor.a -= 0.25f * Time.deltaTime;
            WhiteQuad.material.color = newColor;
        }
    }

    // Update is called once per frame
    void Update() {

        myTriggerLoadTimer += Time.deltaTime;
        bool FirePressed = (Input.GetButton("Fire1") || Input.anyKey);

        if (!isLoadingNewScene && (myTriggerLoadTimer > TriggerLoadAfter || FirePressed) )
        {
            if (TransitionsAreFinished())
            {
                myWaitForFinishedTimer += Time.deltaTime;
                if(myWaitForFinishedTimer > 0.10f && !isLoadingNewScene)
                {
                    m_fireWasPressed = FirePressed;
                    isLoadingNewScene = true;
                    var fadeObjects = FindObjectsOfType<FadeFromWhite>();
                    foreach (FadeFromWhite fade in fadeObjects)
                    {
                        fade.Show();
                    }
                }
            }
            else
            {
                myWaitForFinishedTimer = 0f;
                FinishAllTransitions();
            }
            //StartCoroutine("LoadLevelAsync");
        }
        else if(isLoadingNewScene)
        {
            music.fadeMusic();
            myLoadingTimer += Time.deltaTime;
            if (myLoadingTimer > 3f && myTriggerLoadTimer >= TriggerLoadAfter)
            {
                SceneManager.LoadScene("Cabinet2020_Demo");
            }
            else if(m_fireWasPressed && myLoadingTimer > 3f)
            {
                SceneManager.LoadScene("Cabinet2020");
            }
        }
        else
        {
            HandledWhiteFadeOut();
        }
    }

}