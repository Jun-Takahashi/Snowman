using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    public int Speed;

    private bool Player;
    private string Enemys;
    private Vector3 velocity;
    
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
            if (Enemys == "Enemy")
            {
                velocity = new Vector3(0, 0, -1);
            }
            if (Enemys == "DiagonalR")
            {
                velocity = new Vector3(1, 0, -1);
            }
            if (Enemys == "DiagonalL")
            {
                velocity = new Vector3(-1, 0, -1);
            }
        }

        this.Player = Player;
        this.Enemys = Enemys;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (velocity * Speed) * Time.deltaTime;

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
    }

    void OnTrigerEnter(Collider collision)
    {
        GameObject bullet;

        #region　プレイヤー弾と弾の判定
        if (Player)
        {
            if (collision.gameObject.tag == "BulletE")
            {
                bullet = collision.gameObject;
            }
        }
        #endregion

        #region エネミー弾と弾の判定
        if(!Player)
        {
            if (collision.gameObject.tag == "BulletP")
            {
                bullet = collision.gameObject;
            }
        }
        #endregion
    }
}
