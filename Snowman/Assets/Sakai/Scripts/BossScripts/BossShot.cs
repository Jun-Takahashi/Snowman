using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    [SerializeField, Header("ボスの弾")]
    private GameObject Bullet = null;

    [SerializeField]
    public BossManager bossManager;

    //待機時間（リキャスト）に対する現在の経過時間をカウント
    private float timeCnt;
    //弾を撃つ間隔（次の弾を撃つまでの時間をカウント）
    private float timeCnt2;

    [SerializeField, Header("次に弾を撃つまでの待機時間")]
    private float reCast = 0.3f;
    public float pinchReCast = 0.1f;

    private float reCastNum;

    [SerializeField, Header("連射速度（弾の間隔）")]
    private float rapidFire = 0.3f;
    public float pinchRapidFire = 0.1f;

    private float rapidFireNum;

    //[SerializeField, Header("一度に撃つ弾の最大数")]
    //private int bulletNum = 6;

    //現在が弾を撃てる状態かを管理するフラグ
    public bool shotFlag;

    //現在残っている弾の数
    private int bulletCnt = 0;

    [SerializeField, Header("通常弾のパターン")]
    public List<int> pattern1;
    public List<int> pattern2;
    public List<int> pattern3;

    [SerializeField, Header("ピンチ状態のパターン")]
    public List<int> patternA;
    public List<int> patternB;

    public Dictionary<int, List<int>> blletPattern;

    private List<int> Ilist = null;

    private int Pnum;

    public bool rightOrLeft;
    [SerializeField, Header("弾の出現位置を調整用")]
    public Vector3 AdjustmentBullet = new Vector3(0.3f, 0.5f, 0);

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        blletPattern = new Dictionary<int, List<int>>
        {
            { 1,pattern1 },
            { 2,pattern2 },
            { 3,pattern3 },
            { 4,patternA }, //ここからピンチパターン
            { 5,patternB }
        };
        shotFlag = false;
        timeCnt = 0;
        timeCnt2 = 0;
        Pnum = 0;
        Ilist = RandomList();

        reCastNum = reCast;
        rapidFireNum = rapidFire;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(reCastNum);
        timeCnt = TimeCounter(timeCnt);
        if (timeCnt >= reCastNum)
        {
            shotFlag = true;
            timeCnt = 0;
        }

        if (shotFlag)
        {
            timeCnt2 = TimeCounter(timeCnt2);
            if (timeCnt2 >= rapidFireNum)
            {
                if (Pnum < Ilist.Count)
                {
                    shot(Bullet);
                    if (rightOrLeft)
                        rightOrLeft = false;
                    else if (!rightOrLeft)
                        rightOrLeft = true;
                    Pnum++;

                }
                else if (Pnum >= Ilist.Count)
                {
                    shotFlag = false;
                    Pnum = 0;
                    Ilist = RandomList();
                }
                timeCnt2 = 0;
            }
        }
    }

    /// <summary>
    /// 弾の生成メソッド
    /// </summary>
    /// <param name="bullet">弾オブジェクト</param>
    void shot(GameObject bullet)
    {
        
        if (rightOrLeft)
        {
            
            anim.SetBool("direction", rightOrLeft);
            AdjustmentBullet.x = AdjustmentBullet.x * -1;
        }
        else if (!rightOrLeft)
        {
            
            anim.SetBool("direction", rightOrLeft);
            AdjustmentBullet.x = AdjustmentBullet.x * -1;
        }

        GameObject bulletInstance = Instantiate(bullet, transform.position + AdjustmentBullet, transform.rotation);
        Firing script = bulletInstance.GetComponent<Firing>();
        script.SetTag(false, "Enemy");

        script.Charge(Ilist[Pnum]);
        anim.SetTrigger("shot");
    }

    /// <summary>
    /// リストをランダムに返す
    /// </summary>
    /// <returns></returns>
    List<int> RandomList()
    {
        if (bossManager.bossState == BossManager.BossState.Normal)
        { Ilist = blletPattern[Random.Range(1, 4)]; }
        else if (bossManager.bossState == BossManager.BossState.Pinch)
        { Ilist = blletPattern[Random.Range(4, 6)]; }

        return Ilist;
    }

    public void MoveCheck()
    {
        reCastNum = pinchReCast;
        rapidFireNum = pinchRapidFire;
    }


    /// <summary>
    /// 時間計測用
    /// </summary>
    /// <param name="Count">経過時間</param>
    /// <returns></returns>
    float TimeCounter(float Count)
    {
        Count += Time.deltaTime;
        return Count;
    }
}
