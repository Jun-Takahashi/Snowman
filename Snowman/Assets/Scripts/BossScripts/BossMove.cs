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
    [SerializeField] //移動する目的地のリスト
    private List<Transform> MovePoints;

    private Vector3 destination; //次の目的地

    private Vector3 direction; //向かう方向

    [SerializeField, Header("移動速度")]
    private float speed = 3.0f;

    private Vector3 velocity; //移動量

    private Rigidbody bossRb;

    [SerializeField]
    public bool arrived; //到着フラグ

    [SerializeField, Header("移動までの待機時間")] //次の行動までの待ち時間
    private float interval = 6f;
    [SerializeField] //経過時間
    private float tmpTime = 0;

    [SerializeField,Header("目的地の最大数")]
    int MaxNum = 5;

    int count; //目的地を通過したら記録

    int mF; //移動を止めるよう（0を掛ける）

    // Start is called before the first frame update
    void Start()
    {
        AddMovePoints(MaxNum);
        bossRb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        arrived = false;
        BossMoveState = MoveState.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector3.zero;
        //Debug.Log(arrived);
        //Debug.Log(count);
        //Debug.Log(destination);
        if (count >= MaxNum) //最大値を超えたらリセット
            count = 0;
        tmpTime += Time.deltaTime; //経過時間計測

        switch (BossMoveState)
        {
            case MoveState.Move:
                GoToPoint(destination);
                break;
            case MoveState.Stop:

                break;
            default:
                break;
        }

        if (tmpTime >= interval) //経過時間が待ち時間を超えたか
        {
            arrived = false; //目的地に着いてない
            NextPoint(); //次の目的地
            if (!arrived) //着いてなければ
                BossMoveState = MoveState.Move; //動く
            else
                BossMoveState = MoveState.Stop; //止まる
            tmpTime = 0;
        }

        if (Vector3.Distance(transform.position, destination) < 0.5f) //目的地に着いたか
        {
            arrived = true; //着いた

        }
    }

    /// <summary>
    /// リストに巡回場所を入れる
    /// </summary>
    /// <param name="MaxNum">巡回場所の最大値</param>
    void AddMovePoints(int MaxNum)
    {
        MovePoints = new List<Transform>();
        for (int i = 0; i < MaxNum; i++)
        {
            MovePoints.Add(BossMovePoint.transform.GetChild(i));
        }
    }

    /// <summary>
    /// 目的地に向かう
    /// </summary>
    void GoToPoint(Vector3 destination)
    {
        if (!arrived)
            mF = 1;
        else
            mF = 0;

        direction = (destination - transform.position).normalized;
        velocity = direction * speed;
        transform.position += (velocity * mF) * Time.deltaTime;
    }

    /// <summary>
    /// 次に向かう場所
    /// </summary>
    void NextPoint()
    {
        destination = MovePoints[count].transform.position;
        count++;
    }



}
