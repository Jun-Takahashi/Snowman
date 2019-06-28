﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    public int Speed;

    private bool Player;
    private string Enemys;
    private Vector3 velocity;
    private int chargeP;
    
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

        transform.localScale = new Vector3(chargeP * 0.5f, chargeP * 0.5f, chargeP * 0.5f);
        //チャージ量に応じて大きさを変える

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
        if(transform.position.x > Screen.WidthR)
        {
            velocity.x = -velocity.x;
            transform.position = new Vector3(Screen.WidthR, transform.position.y, transform.position.z);
        }
        if(transform.position.x < Screen.WidthL)
        {
            velocity.x = -velocity.x;
            transform.position = new Vector3(Screen.WidthL, transform.position.y, transform.position.z);
        }
        #endregion

        if(chargeP <= 0)
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

    public void Charge(int chargeP)
    {
        this.chargeP = chargeP;
    }

    public int DamageCheck()
    {
        return chargeP;//チャージ量を返す
    }

    public void Damage(int damage)
    {
        chargeP -= damage;//大きさを変える
    }
}
