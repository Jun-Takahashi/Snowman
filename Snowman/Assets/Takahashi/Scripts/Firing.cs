using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    public int Speed;
    [SerializeField, Header("Playerとの距離感")]
    public float distanceP = 0.5f;

    private bool Player;
    private string Enemys;
    private Vector3 velocity;
    private int chargeP;

    private float lieScale;
    
    /// <summary>
    /// タグ設定
    /// </summary>
    /// <param name="Player">Playerか否か</param>
    /// <param name="Enemys">Enemyの種類は？</param>
    public void SetTag(bool Player,string Enemys)
    {
        if(Player)
        {
            gameObject.tag = "BulletP";
            velocity = new Vector3(0, 0, 1);
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
        }

        this.Player = Player;
        this.Enemys = Enemys;
    }
    
    void Update()
    {
        transform.position += (velocity * Speed) * Time.deltaTime;//移動

        if (gameObject.tag == "Untagged" ||//チャージ段階か
            gameObject.tag =="BulletP")//Playerが発射した弾だったら
        {
            if (lieScale < chargeP)
            {
                lieScale += 0.1f;
                transform.localPosition += new Vector3(0, 0, distanceP) * 0.1f;
                transform.localScale += new Vector3(0.5f, 0.5f, 0.5f) * 0.1f;
            }
            else
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * chargeP;
                //チャージ量に応じて大きさを変える
            }
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * chargeP;
            //チャージ量に応じて大きさを変える
        }

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
        if (Player)
        {
            if (collision.gameObject.tag == "BulletE")
            {
                GameObject bullet = collision.gameObject;
                Firing script = bullet.GetComponent<Firing>();
                if(script.DamageCheck() >= chargeP)//自分より大きい又は同じ大きさの弾に当たったら
                {
                    script.Damage(chargeP);//相手の大きさを変える
                    Destroy(gameObject);//自分は消える
                }
                else//自分より小さな弾に当たったら
                {
                    chargeP += script.DamageCheck();//大きくなって
                    Destroy(bullet);//相手を消す
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
