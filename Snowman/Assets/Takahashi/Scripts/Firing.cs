using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField, Header("ジャンクオブジェクト")]
    public List<GameObject> JunkObject;
    [SerializeField, Header("移動速度")]
    public int Speed;
    [SerializeField, Header("Playerとの距離感")]
    public float distanceP = 0.5f;
    [SerializeField, Header("弾のサイス倍率")]
    public float Size = 0.5f;
    [SerializeField, Header("威力に対してJunkいくつ？")]
    public int JunkNum = 1;

    private bool Player;
    private string Enemys;
    private Vector3 velocity;
    private int chargeP;

    private float lieScale;
    private bool setScale;
    
    private int childCheck;//子オブジェクトが初期でいくつあるか

    private AudioSource AbsorbSE;//吸収SE

    public void Start()
    {
        setScale = false;
    }
    
    /// <summary>
    /// タグ設定
    /// </summary>
    /// <param name="Player">Playerか否か</param>
    /// <param name="Enemys">Enemyの種類は？</param>
    public void SetTag(bool Player,string Enemys)
    {
        #region 移動量設定
        if(Player)
        {
            gameObject.tag = "BulletP";
            velocity = new Vector3(0, 0, 1);
            AbsorbSE = GetComponent<AudioSource>();
            transform.parent = null;
        }
        if(!Player)
        {
            gameObject.tag = "BulletE";
            chargeP = 1;
            if (Enemys == "Enemy")//正面撃ち
            {
                velocity = new Vector3(0, 0, -1);
            }
            if (Enemys == "DiagonalR")//ツーウェイの右撃ち
            {
                velocity = new Vector3(1, 0, -1);
            }
            if (Enemys == "DiagonalL")//ツーウェイの左撃ち
            {
                velocity = new Vector3(-1, 0, -1);
            }
            if (Enemys == "Boss")//ニコラ・テスラ
            {
                velocity = new Vector3(0, 0, -1);
            }
        }
        #endregion

        this.Player = Player;
        this.Enemys = Enemys;
        
        childCheck = transform.childCount;
    }
    
    void Update()
    {
        transform.position += (velocity * Speed) * Time.deltaTime;//移動

        #region スケール設定
        if(setScale)
        {/*下記を飛ばします*/}
        else if (gameObject.tag == "Untagged")//チャージ段階か
        {
            if (lieScale < chargeP)
            {
                lieScale += 0.1f;
                transform.localPosition += new Vector3(0, 0, distanceP) * 0.1f;
                transform.localScale += new Vector3(Size, Size, Size) * 0.1f;
            }
            else
            {
                transform.localScale = new Vector3(Size, Size, Size) * chargeP;
                //チャージ量に応じて大きさを変える
            }
        }
        else if(gameObject.tag == "BulletP")//Playerの弾だったら
        {
            if (lieScale < chargeP)
            {
                lieScale += 0.1f;
                transform.localPosition += new Vector3(0, 0, distanceP) * 0.1f;
                transform.localScale += new Vector3(Size, Size, Size) * 0.1f;
            }
            else
            {
                transform.localScale = new Vector3(Size, Size, Size) * chargeP;
                //チャージ量に応じて大きさを変える
                setScale = true;//スケール設定完了
            }
        }
        else//Enemyの弾だったら
        {
            transform.localScale = new Vector3(Size, Size, Size) * chargeP;
            //チャージ量に応じて大きさを変える
            //gameObject.GetComponent<MeshRenderer>().enabled = false;

            if (Enemys == "Enemy")
            {
                for (int i = 0; i < chargeP * JunkNum; i++)//ジャンクを威力分生成
                {
                    GameObject Junk = JunkObject[Random.Range(0, JunkObject.Count)];
                    Vector3 settingPos = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));//ジャンクの位置決め
                    GameObject InstanceJ = Instantiate(Junk, settingPos, Quaternion.identity, transform);//生成時位置指定
                    InstanceJ.transform.Rotate(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)), Random.Range(0, 361));//ジャンクの回転量決め

                    InstanceJ.transform.localScale = InstanceJ.transform.localScale / chargeP;//スケールをジャンク自体に戻す。
                }
            }

            setScale = true;//スケール設定完了
        }
        #endregion

        #region 画面端判定
        //上画面端ぃ→消す
        if (transform.position.z >= Screen.HeightU)
        {
            Destroy(gameObject);
        }
        //下画面端ぃ→消す
        if (transform.position.z <= Screen.HeightB)
        {
            Destroy(gameObject);
        }
        //左右画面端ぃ→反射ぁ
        if (transform.position.x > Screen.WidthR)
        {
            velocity.x = -velocity.x;
            transform.position = new Vector3(Screen.WidthR, transform.position.y, transform.position.z);
        }
        if (transform.position.x < Screen.WidthL)
        {
            velocity.x = -velocity.x;
            transform.position = new Vector3(Screen.WidthL, transform.position.y, transform.position.z);
        }
        #endregion

        if (chargeP <= 0 && gameObject.tag != null)
        {
            Destroy(gameObject);//大きさが0になったら消えよる
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        #region　当たり判定
        if(gameObject.tag=="Untagged")
        { /*下飛ばし*/}
        else{
            if (Player == true)//プレイヤーの弾
            {
                if (collision.gameObject.tag == "BulletE")
                {
                    GameObject bullet = collision.gameObject;
                    Firing script = bullet.GetComponent<Firing>();
                    if (script.DamageCheck() < chargeP)//自分より小さな弾に当たったら
                    {
                        chargeP += script.DamageCheck();//大きくなって
                        setScale = false;
                        Destroy(bullet);//相手を消す

                        AbsorbSE.PlayOneShot(AbsorbSE.clip);//吸収音を流す
                    }
                }
            }
            if (Player == false)//エネミーの弾
            {
                if (collision.gameObject.tag == "BulletP")
                {
                    GameObject bullet = collision.gameObject;
                    Firing script = bullet.GetComponent<Firing>();

                    if (script.DamageCheck() <= chargeP)//自分より小さな弾に当たったら
                    {
                        int damage = script.DamageCheck();
                        Destroy(collision.gameObject);//プレイヤーの弾を消す
                        chargeP -= damage;//小さくなって
                        setScale = false;
                        
                        if(Enemys == "Enemy")
                        for (int i = 0; i < damage * JunkNum; i++)
                        {
                            GameObject Junk = transform.GetChild(childCheck).gameObject;
                            Junk.transform.parent = null;

                            damage = 0;
                        }
                    }
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 弾の威力を設定
    /// </summary>
    /// <param name="chargeP">威力</param>
    public void Charge(int chargeP)
    {
        this.chargeP = chargeP;
    }

    /// <summary>
    /// 威力を確認
    /// </summary>
    /// <returns>威力</returns>
    public int DamageCheck()
    {
        return chargeP;//チャージ量を返す
    }

    /// <summary>
    /// 弾自身の大きさを変化
    /// </summary>
    /// <param name="damage">変化量</param>
    public void Damage(int damage)
    {
        chargeP -= damage;//大きさを変える
    }
}
