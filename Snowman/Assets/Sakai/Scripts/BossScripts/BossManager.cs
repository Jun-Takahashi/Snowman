using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public enum BossState
    {
        Normal,
        Pinch
    };
    public BossState bossState;

    [SerializeField, Header("ボスの体力")]
    public float BossHp = 10;
    private float BossHpNum;

    [SerializeField]
    private BossMove bossMove = null;
    [SerializeField]
    private SparkLiner sparkLiner = null;

    public bool sparkFlag = false;

    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        //transform.gameObject.SetActive(false);
        //BossHp = 10;

        bossState = BossState.Normal;
        //bossMove = GetComponent<BossMove>();
        //sparkLiner = GetComponent<SparkLiner>();
        BossHpNum = BossHp;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(BossHp);
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    BossDamage(1);
        //}



        if (BossHp < (BossHpNum / 2) && !flag)
        {
            bossMove.Crisis(bossMove.interval / 3, bossMove.speed * 3);
            SparkLineFlag();
            sparkLiner.NewCoilFlag = true;
            flag = true;
            bossState = BossState.Pinch;
        }
        if (BossHp <= 0)
        {
            transform.gameObject.SetActive(false);
            SceneManager.LoadScene("ClearScene");
        }

    }

    public float BossDamage(int damage)
    {
        BossHp -= damage;
        return BossHp;
    }

    public bool SparkLineFlag()
    {
        return sparkFlag = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "BulletP")
        {
            BossDamage(col.gameObject.GetComponent<Firing>().DamageCheck());
        }

    }
}
