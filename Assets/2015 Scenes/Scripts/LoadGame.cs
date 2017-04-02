using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGame : MonoBehaviour {

    public sound music;
    private bool isLoadingNewScene = false;
    private AsyncOperation async;
    private float myLoadingTimer = 0f;

    void Start()
    {
        myLoadingTimer = 0f;
        music.increaseVolume(0.5f);
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
        if (Input.anyKeyDown && !isLoadingNewScene)
        {
            isLoadingNewScene = true;
            var fadeObjects = FindObjectsOfType<FadeFromWhite>();
            foreach(FadeFromWhite fade in fadeObjects)
            {
                fade.Show();
            }
            //StartCoroutine("LoadLevelAsync");
        }
        if(isLoadingNewScene)
        {
            myLoadingTimer += Time.deltaTime;
            if(myLoadingTimer > 3f)
            {
                SceneManager.LoadScene("Demo2017");
            }
            music.fadeMusic();

        }
    }

}