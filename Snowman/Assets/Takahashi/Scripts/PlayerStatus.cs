﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField,Header("プレイヤーの残機")]
    public int Hp;
    [SerializeField, Header("プレイヤーの弾")]
    public GameObject Bullet;

    private float Charge;
    private int chargeP = 1;

    // Update is called once per frame
    void Update()
    {
        #region 移動処理

        float x = Input.GetAxis("Horizontal") / 2;
        float z = Input.GetAxis("Vertical") / 2;

        transform.position += new Vector3(x, 0, z);

        #region　画面内におさめる
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        if (transform.position.z >= Screen.HeightU)
        {
            posZ = Screen.HeightU;
        }
        if (transform.position.z <= Screen.HeightB)
        {
            posZ = Screen.HeightB;
        }
        if (transform.position.x <= Screen.WidthL)
        {
            posX = Screen.WidthL;
        }
        if (transform.position.x >= Screen.WidthR)
        {
            posX = Screen.WidthR;
        }

        transform.position = new Vector3(posX, posY, posZ);
        #endregion

        #endregion

        #region 残機管理

        if (Hp<=0)
        {
            Destroy(gameObject);
        }

        #endregion

        #region 射撃管理
        Charge += Time.deltaTime;
        if(chargeP == 4)//チャージ最大溜め
        {

        }
        else if(Charge >= 2.0)//チャージ一段階強化
        {
            chargeP += 1;
            Charge = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject instanceB = Instantiate(Bullet,this.transform.position,Quaternion.identity);
            Firing script = instanceB.GetComponent<Firing>();
            script.SetTag(true,null);

            script.Charge(chargeP);//チャージ弾発射
            chargeP = 1;
            Charge = 0;
        }
        #endregion
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Hp--;
        }
        if(collision.gameObject.tag == "BulletE")
        {
            Firing script = collision.gameObject.GetComponent<Firing>();
            Hp -= script.DamageCheck();
            Destroy(collision.gameObject);
        }
    }
}
