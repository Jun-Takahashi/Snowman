using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField, Header("ボスの動きが変わるHP（ピンチ）")]
    private float BossHpPinch = 5;

    [SerializeField]
    private BossMove bossMove = null;
    [SerializeField]
    private SparkLiner sparkLiner = null;
    [SerializeField]
    private BossShot bossShot = null;

    [SerializeField, Header("ダメージエフェクト")]
    private ParticleSystem damageEffect = null;
    [SerializeField]
    private InstanceEffect instanceEffect;

    

    public bool sparkFlag = false;

    public bool pinchFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //instanceEffect = GetComponent<InstanceEffect>();
        bossState = BossState.Normal;
        BossHpNum = BossHp;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(BossHp);
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    BossDamage(20);
        //}

        if (BossHp < BossHpPinch && !pinchFlag)
        {
            bossMove.Crisis(bossMove.pinchInterval, bossMove.pinchSpeed);
            SparkLineFlag();
            sparkLiner.NewCoilFlag = true;
            bossMove.Pinch();
            pinchFlag = true;
            bossState = BossState.Pinch;
            bossShot.MoveCheck();
        }
        if (BossHp <= 0)
        {
            instanceEffect.EffectInstance(transform.position);
            transform.gameObject.SetActive(false);
        }

    }

    public float BossDamage(int damage)
    {
        BossHp -= damage;
        damageEffect.Play();
        damageEffect.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
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
            Destroy(col.gameObject);
        }
    }
}
