using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkLiner : MonoBehaviour
{
    [SerializeField]
    public GameObject coilPositions = null;

    [SerializeField]
    public BossManager bossManager = null;

    [SerializeField, Header("経過時間")]
    private float timeElapsed;
    [SerializeField, Header("経過時間2")]
    private float timeElapsed2;

    [SerializeField, Header("テスラコイル")]
    public GameObject coil;

    private float NewTime;

    //public List<GameObject> coilInstanceS;
    GameObject coilInstance = null;
    [SerializeField, Header("コイルを向かわせる速度")]
    public float coilSpeed = 0.2f;

    [SerializeField, Header("サンダーパワー")]
    public int ThunderPower = 1;

    [SerializeField, Header("パーティクルの大きさ")]
    public float ParticlePower = 1;

    [SerializeField, Header("放電開始までの時間")]
    public float SparkTime = 0.2f;

    [SerializeField, Header("放電を続ける時間")]
    public float SparkingTime = 0.2f;

    public bool NewCoilFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //coilPositions = null;
        //bossManager = GetComponent<BossManager>();
        NewCoilFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (coilPositions == null && bossManager.sparkFlag)
        //{
        //    coilPositions = FindObjectTag(coilPositions, "CoilPos");
        //}

        if (NewCoilFlag)
        {
            NewTime = TimeCount(NewTime);
            if (NewTime > SparkTime)
            {
                NewCoil();
                NewCoilFlag = false;
                NewTime = 0;
                //Debug.Log(223);
            }
        }

        if (coilInstance != null && coilInstance.transform.GetChild(0).GetComponent<CoilMove>().SparkFlag)
        {
            timeElapsed = TimeCount(timeElapsed);
            if (timeElapsed > SparkTime)
            {
                timeElapsed2 = TimeCount(timeElapsed2);
                Debug.Log("放電せよ！");
                Sparking(coilInstance, ThunderPower);
                if (timeElapsed2 > SparkingTime)
                {
                    Destroy(coilInstance);
                    timeElapsed = 0.0f;
                    timeElapsed2 = 0.0f;
                    NewCoilFlag = true;
                }
            }
        }
    }

    /// <summary>
    /// コイルを発射する
    /// </summary>
    /// <param name="coil">コイルの親オブジェ</param>
    void Shot(GameObject coil, float speed)
    {
        coil.transform.GetChild(0).GetComponent<CoilMove>()
            .Move(coilPositions.transform.GetChild(Random.Range(0, (coilPositions.transform.childCount + 1)))
            .transform.position, speed);
    }

    /// <summary>
    /// コイルを放電させる
    /// </summary>
    /// <param name="coil">コイルの親オブジェクト</param>
    /// <param name="ThunderPower">放電の当たり判定の大きさ</param>
    void Sparking(GameObject coil, int ThunderPower)
    {
        coil.transform.GetChild(0).GetComponent<CoilMove>().Spark(ThunderPower, ParticlePower);
    }

    /// <summary>
    /// コイルを生成
    /// </summary>
    void NewCoil()
    {
        coilInstance = Instantiate(coil);
        coilInstance.transform.position = transform.position;
        Shot(coilInstance, coilSpeed);
    }

    /// <summary>
    /// タグと一致するオブジェクトを返す
    /// </summary>
    /// <param name="gameObject">返すオブジェクト</param>
    /// <param name="tag">判別するタグ</param>
    /// <returns></returns>
    GameObject FindObjectTag(GameObject gameObject, string tag)
    {
        gameObject = GameObject.FindGameObjectWithTag(tag);
        return gameObject;
    }

    /// <summary>
    /// 時間計測用
    /// </summary>
    /// <param name="cnt">経過した時間</param>
    /// <returns></returns>
    float TimeCount(float cnt)
    {
        cnt += Time.deltaTime;
        return cnt;
    }

}
