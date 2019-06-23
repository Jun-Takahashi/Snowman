using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private int BossHp;
    [SerializeField]
    private BossMove bossMove;
    [SerializeField]
    private SparkLiner sparkLiner;

    public bool sparkFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        BossHp = 10;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            SparkLineFlag();
        }

    }

    public int BossDamage(int damage)
    {
        BossHp -= damage;
        return BossHp;
    }

    public bool SparkLineFlag()
    {
        return sparkFlag = true;
    }
}
