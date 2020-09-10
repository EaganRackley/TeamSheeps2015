using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGame : MonoBehaviour {

    public sound music;
    private bool isLoadingNewScene = false;
    private AsyncOperation async;
    private float myLoadingTimer = 0f;
    private float myWaitForFinishedTimer = 0f;

    public TriggerSwitchCamera TriggerCamera;
    public TriggerFadeObjects[] TriggerFades;

    void Start()
    {
        myLoadingTimer = 0f;
        music.increaseVolume(0.5f);
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

    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Fire1") || Input.anyKey && !isLoadingNewScene)
        {
            if (TransitionsAreFinished())
            {
                myWaitForFinishedTimer += Time.deltaTime;
                if(myWaitForFinishedTimer > 2f)
                {
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
        if(isLoadingNewScene)
        {
            music.fadeMusic();
            myLoadingTimer += Time.deltaTime;
            if(myLoadingTimer > 3f)
            {
                SceneManager.LoadScene("Cabinet2020");
            }
        }
    }

}