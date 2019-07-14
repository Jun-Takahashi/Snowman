using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReSpawn : MonoBehaviour
{
    public GameObject Player;
    [SerializeField, Header("プレイヤーの残機")]
    public int Hp;
    [SerializeField, Header("無敵時間")]
    public float InvinciblyTime;

    private Vector3 RespawnPlace;
    private float time;
    private GameObject[] tagObjects;//特定タグの数用
    
    // Update is called once per frame
    void Update()
    {
        if(Check("Player") == 0)
        {
            time += Time.deltaTime;
        }
        if(time >=0.1f)
        {
            GameObject instanceB = Instantiate(Player, RespawnPlace, Quaternion.identity);
            PlayerStatus script = instanceB.GetComponent<PlayerStatus>();
            script.ReSpawn(InvinciblyTime);

            time = 0;
        }
        
        #region 残機管理
        if (Hp <= 0)
        {
            SceneManager.LoadScene("EndingScene");
        }
        #endregion
    }

    /// <summary>
    /// リスポーン地点を設定
    /// </summary>
    /// <param name="RespawnPlace"></param>
    public void SetPlace(Vector3 RespawnPlace)
    {
        this.RespawnPlace = RespawnPlace;
    }

    public void Death()
    {
        Hp--;
    }

    #region 特定タグの個数検索
    int Check(string tagName)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagName);

        return tagObjects.Length;
    }
    #endregion
}
