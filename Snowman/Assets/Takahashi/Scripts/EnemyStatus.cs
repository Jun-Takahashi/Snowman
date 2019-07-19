using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private List<Vector3> moveP;//移動する位置
    public float Speed;//移動速度
    [SerializeField, Header("エネミーのHP")]
    public int Hp;
    [SerializeField, Header("エネミーの弾")]
    public GameObject Bullet;
    public List<int> BulletPower;
    [SerializeField, Header("エネミーの連射速度")]
    public float FireSpeed = 1;
    public float ReloadTime = 2.5f;

    private Vector3 target;//次に移動する位置
    private int targetC;//次の位置を指定するListの場所
    private Vector3 distination;//最終地点
    private float span;//弾発射の間
    private float wavespan;//弾のパターンを繰り返す間
    private int nextPower;//次の弾の威力のListの要素番号
    
    // Start is called before the first frame update
    public void SetPosition(List<Vector3> moveP)
    {
        this.moveP = moveP;
        targetC = 1;
        target = moveP[targetC];
        distination = moveP[moveP.Count - 1];
        span = 0;
        wavespan = ReloadTime;
        nextPower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #region 移動処理

        #region　傑作
            if (Vector3.Distance(transform.position, target) < 0.8)
            {
                targetC++;
                target = moveP[targetC];
            }
            if (Vector3.Distance(transform.position, distination) < 1)//終着点にてループ
            {
                targetC = 1;
                target = moveP[targetC];
            }

            transform.position = Vector3.Lerp(transform.position, target, Vector3.Distance(transform.position, target) / Speed * Time.deltaTime);
        #endregion

        #region　駄作
        //if (set)
        //{
        //    if(Vector3.Distance(transform.position,distination-new Vector3(1.5f,0,0))<0.001)
        //    {
        //        target = distination + new Vector3(1.5f, 0, 0);
        //    }
        //    if (Vector3.Distance(transform.position, distination + new Vector3(1.5f, 0, 0)) < 0.001)
        //    {
        //        target = distination - new Vector3(1.5f, 0, 0);
        //    }

        //    transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
        //}
        //else
        //{
        //    if (Vector3.Distance(transform.position, target) < 0.001)
        //    {
        //        targetC++;
        //        target = moveP[targetC];
        //    }

        //    transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);

        //    if (Vector3.Distance(transform.position, distination) < 0.001)//終着点にてループ
        //    {
        //        target = distination - new Vector3(1.5f, 0, 0);
        //        set = true;
        //    }
        //}
        #endregion

        #endregion

        #region 射撃管理
        span += Time.deltaTime * 2;
        wavespan += Time.deltaTime * 2;
        if (span >= FireSpeed && wavespan >= ReloadTime)
        {
            GameObject instanceB = Instantiate(Bullet, this.transform.position, Quaternion.identity);
            Firing script = instanceB.GetComponent<Firing>();
            script.SetTag(false,"Enemy");

            script.Charge(BulletPower[nextPower]);//チャージ弾発射
            nextPower++;

            span = 0;

            if (nextPower >= BulletPower.Count)
            {
                nextPower = 0;
                wavespan = 0;
            }
        }
        #endregion

        #region 死亡判定
        if (Hp <= 0)//HPが0になり死亡s
        {
            GetComponent<InstanceEffect>().EffectInstance(transform.position);
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemyFac>().Count();
            Destroy(gameObject);
        }
        #endregion
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "BulletP")
        {
            Firing script = collision.gameObject.GetComponent<Firing>();
            int dm = script.DamageCheck();
            script.Damage(Hp);
            Hp -= dm;
            //Destroy(collision.gameObject);//当たった弾は消える
        }
    }
}
