﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private List<Vector3> moveP;//移動する位置
    public int Speed;//移動速度
    [SerializeField, Header("エネミーのHP")]
    public int Hp;
    [SerializeField, Header("エネミーの弾")]
    public GameObject Bullet;

    private Vector3 target;//次に移動する位置
    private int targetC;//次の位置を指定するListの場所
    private Vector3 distination;//最終地点
    private float span;//弾発射の間
    private bool set;//最終地点についたか否か

    // Start is called before the first frame update
    public void SetPosition(List<Vector3> moveP)
    {
        this.moveP = moveP;
        targetC = 1;
        target = moveP[targetC];
        distination = moveP[moveP.Count - 1];
        span = 0;
        set = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region 移動処理
        if (set)
        {
            if(Vector3.Distance(transform.position,distination-new Vector3(1.5f,0,0))<0.001)
            {
                target = distination + new Vector3(1.5f, 0, 0);
            }
            if (Vector3.Distance(transform.position, distination + new Vector3(1.5f, 0, 0)) < 0.001)
            {
                target = distination - new Vector3(1.5f, 0, 0);
            }

            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
        }
        else
        {
            if (Vector3.Distance(transform.position, target) < 0.001)
            {
                targetC++;
                target = moveP[targetC];
            }

            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);

            if (Vector3.Distance(transform.position, distination) < 0.001)//終着点にて削除
            {
                target = distination - new Vector3(1.5f, 0, 0);
                set = true;
            }
        }
        #endregion

        #region 射撃管理
        span += Time.deltaTime;
        if (span >= 1)
        {
            GameObject instanceB = Instantiate(Bullet, this.transform.position, Quaternion.identity);
            Firing script = instanceB.GetComponent<Firing>();
            script.SetTag(false,"Enemy");
            span = 0;
        }
        #endregion

        #region 死亡判定
        if (Hp == 0)//HPが0になり死亡
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "BulletP")
        {
            Destroy(collision.gameObject);
        }
    }
}
