﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField,Header("プレイヤーの残機")]
    public int Hp;
    [SerializeField, Header("プレイヤーの弾")]
    public GameObject Bullet;
    [SerializeField, Header("画面端")]
    public float HeightU;
    public float HeightB;
    public float WidthR;
    public float WidthL;
    
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

        if (transform.position.z >= HeightU)
        {
            posZ = HeightU;
        }
        if (transform.position.z <= HeightB)
        {
            posZ = HeightB;
        }
        if (transform.position.x <= WidthL)
        {
            posX = WidthL;
        }
        if (transform.position.x >= WidthR)
        {
            posX = WidthR;
        }

        transform.position = new Vector3(posX, posY, posZ);
        #endregion

        #endregion

        #region 残機管理

        if (Hp==0)
        {
            Destroy(gameObject);
        }

        #endregion

        #region 射撃管理

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject instanceB = Instantiate(Bullet,this.transform.position,Quaternion.identity);
            Firing script = instanceB.GetComponent<Firing>();
            script.SetTag(true);
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
            Destroy(collision.gameObject);
            Hp--;
        }
    }
}
