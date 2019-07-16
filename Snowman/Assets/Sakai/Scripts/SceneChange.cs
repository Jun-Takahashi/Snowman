using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneChange : MonoBehaviour
{
    private string scene;

    [SerializeField, Header("プレイヤーリスポーン")]
    public GameObject player = null;
    [SerializeField, Header("ボス")]
    public GameObject boss = null;
    [SerializeField, Header("タイマー")]
    public GameObject timer = null;
    [SerializeField, Header("エネミーファクトリー")]
    public GameObject eneFac = null;

    [SerializeField, Header("ゲームオーバーまでの移行ディレイ")]
    public float gameOverDelay;
    float timeCount;

    AudioSource audioSource;

    [SerializeField, Header("選択SE")]
    public AudioClip choiceSE;

    [SerializeField, Header("SEのタイミング用待機時間")]
    public float SEtime = 1;
    bool changeFlag;


    enum SceneState
    {
        title,
        gamePlay,
        clear,
        gameOver,
    };
    private SceneState sceneState;

    float bossHp;

    //int number;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        scene = SceneManager.GetActiveScene().name;
        if (scene == "TitleScene")
            sceneState = SceneState.title;
        else if (scene == "Stage1" || scene == "Sakai")
            sceneState = SceneState.gamePlay;
        else if (scene == "EndingScene")
            sceneState = SceneState.gameOver;
        else if (scene == "ClearScene")
            sceneState = SceneState.clear;
        //number = 0;
        audioSource = GetComponent<AudioSource>();
        changeFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region ゲームプレイシーンの処理
        if (sceneState == SceneState.gamePlay)
        {
            if (timer.GetComponent<TimeCount>().countDown <= 0)
            {
                SceneManager.LoadScene(ChangeScene(scene, 1));
            }
            else if (player.GetComponent<ReSpawn>().Hp <= 0)
            {
                timeCount = TimeCounter(timeCount);
                if (timeCount >= gameOverDelay)
                {
                    SceneManager.LoadScene(ChangeScene(scene, 1));
                }
            }
            else if (boss == null && eneFac == null)
                boss = GameObject.Find("Nikola Tesla(Clone)");
            if (boss != null)
            {
                bossHp = boss.GetComponent<BossManager>().BossHp;
                if (boss.GetComponent<BossManager>().BossHp <= 0)
                {
                    timeCount = TimeCounter(timeCount);
                    if (timeCount >= gameOverDelay)
                    {
                        SceneManager.LoadScene(ChangeScene(scene, 2));
                    }
                }
            }
        }
        #endregion

        #region それ以外のシーン処理（タイトル、エンディング等）
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                changeFlag = true;
                audioSource.PlayOneShot(choiceSE);
            }
            if (changeFlag)
            {
                timeCount = TimeCounter(timeCount);
                if (timeCount >= SEtime)
                    SceneManager.LoadScene(ChangeScene(scene, 0));
            }
        }
        #endregion
    }

    string ChangeScene(string sceneName, int num)
    {
        if (sceneState == SceneState.title)
        {
            sceneName = "Stage1";
            //sceneName = "Sakai";
        }
        else if (sceneState == SceneState.gameOver)
        {
            sceneName = "TitleScene";

        }
        else if (sceneState == SceneState.clear)
            sceneName = "TitleScene";
        else if (sceneState == SceneState.gamePlay)
        {
            if (num == 1)
                sceneName = "EndingScene";
            else if (num == 2)
                sceneName = "ClearScene";
        }
        return sceneName;
    }

    float TimeCounter(float count)
    {
        count += Time.deltaTime;
        return count;
    }
}
