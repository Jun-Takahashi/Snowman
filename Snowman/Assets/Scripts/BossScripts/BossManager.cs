using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private CharaAttribute BossAttribute = CharaAttribute.minus;

    private int BossHp;
    [SerializeField]
    private BossMove bossMove;
    
    // Start is called before the first frame update
    void Start()
    {
        BossHp = 10;
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int BossDamage(int damage)
    {
        BossHp -= damage;
        return BossHp;
    }
}
