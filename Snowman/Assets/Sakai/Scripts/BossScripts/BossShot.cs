using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    [SerializeField, Header("ボスの弾")]
    private GameObject Bullet = null;

    //private List<GameObject> bulletList;

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

    // Start is called before the first frame update
    void Start()
    {
        shotFlag = false;
        timeCnt = 0;
        timeCnt2 = 0;
        bulletCnt = bulletNum;
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
                if (bulletCnt > 0)
                {
                    shot(Bullet);
                    bulletCnt--;
                }
                else if (bulletCnt <= 0)
                {
                    shotFlag = false;
                    bulletCnt = bulletNum;
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
        //bulletList.Add(bulletInstance);
    }



    //void Fire(GameObject obj)
    //{
    //    obj.transform.position = obj.transform.position += new Vector3(0, 0, -1);
    //    shotFlag = false;
    //}

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
