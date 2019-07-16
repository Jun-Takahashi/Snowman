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
    public GameObject PinchMovePoint;

    [SerializeField, Header("移動する目的地のリスト")]
    private List<Transform> MovePoints = null;
    public List<Transform> PMovePoints = null;

    private Vector3 destination; //次の目的地

    [SerializeField, Header("移動速度")]
    public float speed = 3.0f;
    public float pinchSpeed = 4.5f;

    private Rigidbody bossRb; //ボスのRigidbody

    //目的地に着いているかどうか
    //private bool arrived;

    [SerializeField, Header("移動までの待機時間")]
    public float interval = 6f;
    public float pinchInterval = 3f;

    //経過時間
    private float tmpTime = 0;

    //目的地の数
    private int maxNum = 1;

    //目的地を通過したら記録
    private int count;

    private BossManager bossManager;

    // Start is called before the first frame update
    void Start()
    {
        maxNum = BossMovePoint.transform.childCount;
        AddMovePoints(maxNum, BossMovePoint, MovePoints);

        bossRb = GetComponent<Rigidbody>();
        //arrived = true;
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
                        NextPoint(count, MovePoints);
                    else if (bossManager.bossState == BossManager.BossState.Pinch)
                        NextPoint(count, PMovePoints);
                    //RandomNextPoint(); //ランダムはダメなので修正する 2019/07/03
                    BossMoveState = MoveState.Move;
                    //arrived = false;
                    tmpTime = 0;
                }
                break;
            default:
                break;
        }

        if (Vector3.Distance(transform.position, destination) < 0.5f) //目的地に着いたか
        {
            //arrived = true; //着いた
            BossMoveState = MoveState.Stop; //待機に移行
        }
    }

    /// <summary>
    /// リストに巡回場所を入れる
    /// </summary>
    /// <param name="MaxNum">巡回場所の最大値</param>
    void AddMovePoints(int MaxNum, GameObject BossMovePoint, List<Transform> MovePoints)
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
        transform.position = Vector3.Lerp(transform.position, destination,
            Vector3.Distance(transform.position, destination) / speed * Time.deltaTime);
    }

    /// <summary>
    /// 次に向かう場所
    /// </summary>
    /// <param name="count">周回場所の数字をカウント</param>
    void NextPoint(int count, List<Transform> MovePoints)
    {
        destination = MovePoints[count].transform.position;
        count++;
        this.count = count;
    }

    /// <summary>
    /// ボスがピンチの時に呼ばれる
    /// </summary>
    /// <param name="PInterval">待機時間（ピンチ時）</param>
    /// <param name="PSpeed">移動速度（ピンチ時）</param>
    public void Crisis(float PInterval, float PSpeed)
    {
        interval = PInterval;
        speed = PSpeed;
    }

    void RandomNextPoint()
    {
        destination = MovePoints[Random.Range(0, maxNum)].transform.position;
    }

    public void Pinch()
    {
        maxNum = PinchMovePoint.transform.childCount;
        AddMovePoints(maxNum, PinchMovePoint, PMovePoints);
    }

}
