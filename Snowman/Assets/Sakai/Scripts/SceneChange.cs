using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private string scene;

    enum SceneState
    {
        title,
        gamePlay,
        clear,
        gameOver,
    };
    private SceneState sceneState;


    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        if (scene == "TitleScene")
            sceneState = SceneState.title;
        else if (scene == "Stage1")
            sceneState = SceneState.gamePlay;
        else if (scene == "EndingScene")
            sceneState = SceneState.gameOver;
        else if (scene == "ClearScene")
            sceneState = SceneState.clear;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(ChangeScene(scene));
        }
    }

    string ChangeScene(string sceneName)
    {
        if (sceneState == SceneState.title)
            sceneName = "Stage1";
        else if (sceneState == SceneState.gameOver)
            sceneName = "TitleScene";
        else if (sceneState == SceneState.clear)
            sceneName = "TitleScene";
        return sceneName;
    }
}
