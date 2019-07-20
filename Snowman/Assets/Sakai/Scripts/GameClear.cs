using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public Text clearText;
    public Text rankingText1;
    public Text rankingText2;
    public Text rankingText3;
    public Text rankingText4;
    public Text rankingText5;
    private float score;
    private float highScore1;
    private float highScore2;
    private float highScore3;
    private float highScore4;
    private float highScore5;

    bool colorFlag;

    float x;

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        AddScore(TimeCount.countDown);
        highScore1 = PlayerPrefs.GetFloat("highScoreKey1", 0);
        highScore2 = PlayerPrefs.GetFloat("highScoreKey2", 0);
        highScore3 = PlayerPrefs.GetFloat("highScoreKey3", 0);
        highScore4 = PlayerPrefs.GetFloat("highScoreKey4", 0);
        highScore5 = PlayerPrefs.GetFloat("highScoreKey5", 0);
        colorFlag = false;
        x = 0;
    }

    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="s"></param>
    void AddScore(float s)
    {
        score = score + s;
    }

    // Update is called once per frame
    void Update()
    {
        #region テキストの点滅処理
        //if (colorFlag)
        //    x -= Time.deltaTime;
        //else
        //    x += Time.deltaTime;
        //if (x < 0)
        //{
        //    x = 0;
        //    colorFlag = false;
        //}
        //else if (x > 1)
        //{
        //    x = 1;
        //    colorFlag = true;
        //}
        #endregion

        clearText.text = TimeCount.countDown.ToString("F2");

        if (highScore1 < score)
        {
            highScore5 = highScore4;
            highScore4 = highScore3;
            highScore3 = highScore2;
            highScore2 = highScore1;
            highScore1 = score;
            rankingText1.color = new Color(255, 0, 0);
        }
        else if (highScore1 > score &&
            highScore2 < score)
        {
            highScore5 = highScore4;
            highScore4 = highScore3;
            highScore3 = highScore2;
            highScore2 = score;
            rankingText2.color = new Color(255, 0, 0);
        }
        else if (highScore2 > score &&
            highScore3 < score)
        {
            highScore5 = highScore4;
            highScore4 = highScore3;
            highScore3 = score;
            rankingText3.color = new Color(255, 0, 0);
        }
        else if (highScore3 > score &&
            highScore4 < score)
        {
            highScore5 = highScore4;
            highScore4 = score;
            rankingText4.color = new Color(255, 0, 0);
        }
        else if (highScore4 > score &&
            highScore5 < score)
        {
            highScore5 = score;
            rankingText5.color = new Color(255, 0, 0);
        }

        rankingText1.text = "1位:" + highScore1;
        rankingText2.text = "2位:" + highScore2;
        rankingText3.text = "3位:" + highScore3;
        rankingText4.text = "4位:" + highScore4;
        rankingText5.text = "5位:" + highScore5;
    }

    public void Save()
    {
        //メソッドが呼ばれた時のキーと値をセットする
        PlayerPrefs.SetFloat("highScoreKey1", highScore1);
        PlayerPrefs.SetFloat("highScoreKey2", highScore2);
        PlayerPrefs.SetFloat("highScoreKey3", highScore3);
        PlayerPrefs.SetFloat("highScoreKey4", highScore4);
        PlayerPrefs.SetFloat("highScoreKey5", highScore5);
        //キーと値を保存
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        //キーを全て消す
        PlayerPrefs.DeleteAll();
        score = 0;
        highScore1 = 0;
        highScore2 = 0;
        highScore3 = 0;
        highScore4 = 0;
        highScore5 = 0;
    }
}
