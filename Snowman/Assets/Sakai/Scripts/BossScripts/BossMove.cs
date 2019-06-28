using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    /// <summary>
    /// 現在の移動状況
    /// </summary>
    enum MoveState
    {
        Move,
        Stop,
    }
    private MoveState BossMoveState; //ボスの移動状況

    [SerializeField, Header("ボスの移動地点")]
    public GameObject BossMovePoint;
    [SerializeField, Header("移動する目的地のリスト")]
    private List<Transform> MovePoints = null;

    private Vector3 destination; //次の目的地

    //private Vector3 direction; //向かう方向

    [SerializeField, Header("移動速度")]
    public float speed = 3.0f;

    private Rigidbody bossRb; //ボスのRigidbody

    [SerializeField, Header("目的地に着いているかどうか")]
    public bool arrived;

    [SerializeField, Header("移動までの待機時間")]
    public float interval = 6f;
    [SerializeField, Header("経過時間")]
    private float tmpTime = 0;

    //目的地の数
    private int maxNum = 5;

    //目的地を通過したら記録
    private int count;

    private BossManager bossManager;

    // Start is called before the first frame update
    void Start()
    {
        maxNum = BossMovePoint.transform.childCount;
        AddMovePoints(maxNum);
        bossRb = GetComponent<Rigidbody>();
        arrived = true;
        BossMoveState = MoveState.Stop;
        bossManager = GetComponent<BossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= maxNum) //最大値を超えたらリセット
            count = 0;

        switch (BossMoveState)
        {
            case MoveState.Move: //Moveの時だけ移動
                GoToPoint(destination);
                break;
            case MoveState.Stop: //Stopなら経過時間が過ぎるまで待機
                tmpTime += Time.deltaTime; //経過時間計測
                if (tmpTime >= interval) //経過時間が待ち時間を超えたか
                {
                    if (bossManager.bossState == BossManager.BossState.Normal)
                        NextPoint(count);
                    else if (bossManager.bossState == BossManager.BossState.Pinch)
                        RandomNextPoint();
                    BossMoveState = MoveState.Move;
                    arrived = false;
                    tmpTime = 0;
                }
                break;
            default:
                break;
        }

        if (Vector3.Distance(transform.position, destination) < 0.5f) //目的地に着いたか
        {
            arrived = true; //着いた
            BossMoveState = MoveState.Stop; //待機に移行
        }
    }

    /// <summary>
    /// リストに巡回場所を入れる
    /// </summary>
    /// <param name="MaxNum">巡回場所の最大値</param>
    void AddMovePoints(int MaxNum)
    {
        for (int i = 0; i < MaxNum; i++)
        {
            MovePoints.Add(BossMovePoint.transform.GetChild(i));
        }
    }

    /// <summary>
    /// 目的地に向かう
    /// </summary>
    /// <param name="destination">目的地</param>
    void GoToPoint(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
    }

    /// <summary>
    /// 次に向かう場所
    /// </summary>
    /// <param name="count">周回場所の数字をカウント</param>
    void NextPoint(int count)
    {
        destination = MovePoints[count].transform.position;
        count++;
        this.count = count;
    }

    public void Crisis(float PInterval, float PSpeed)
    {
        interval = PInterval;
        speed = PSpeed;
    }

    void RandomNextPoint()
    {
        destination = MovePoints[Random.Range(0, maxNum)].transform.position;
    }

}
