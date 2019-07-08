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

    [SerializeField, Header("連射速度（弾の間隔）")]
    private float rapidFire = 0.3f;

    [SerializeField, Header("一度に撃つ弾の最大数")]
    private int bulletNum = 6;

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

    // Start is called before the first frame update
    void Start()
    {
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
    }


    // Update is called once per frame
    void Update()
    {
        timeCnt = TimeCounter(timeCnt);
        if (timeCnt >= reCast)
        {
            shotFlag = true;
            timeCnt = 0;
        }

        if (shotFlag)
        {
            timeCnt2 = TimeCounter(timeCnt2);
            if (timeCnt2 >= rapidFire)
            {
                if (Pnum < Ilist.Count)
                {
                    shot(Bullet);
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
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        Firing script = bulletInstance.GetComponent<Firing>();
        script.SetTag(false, "Enemy");

        script.Charge(Ilist[Pnum]);
    }

    /// <summary>
    /// リストをランダムに返す
    /// </summary>
    /// <returns></returns>
    List<int> RandomList()
    {
        if (bossManager.bossState == BossManager.BossState.Normal)
        { Ilist = blletPattern[Random.Range(1, 4)]; }
        else if(bossManager.bossState == BossManager.BossState.Pinch)
        { Ilist = blletPattern[Random.Range(4, 6)]; }

        return Ilist;
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
