using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField, Header("ボスの体力")]
    public float BossHp;
    [SerializeField]
    private BossMove bossMove;
    [SerializeField]
    private SparkLiner sparkLiner;

    public bool sparkFlag = false;

    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        BossHp = 10;
        bossMove = GetComponent<BossMove>();
        sparkLiner = GetComponent<SparkLiner>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(BossHp);
        if (Input.GetKeyDown(KeyCode.P))
        {
            BossDamage(1);
        }
        if (BossHp < 5 && !flag)
        {
            bossMove.Crisis(bossMove.interval / 3, bossMove.speed * 3);
            SparkLineFlag();
            sparkLiner.NewCoilFlag = true;
            flag = true;
        }
        if (BossHp <= 0)
        {
            transform.gameObject.SetActive(false);
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
}
