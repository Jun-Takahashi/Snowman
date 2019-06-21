using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    public int Speed;
    [SerializeField, Header("画面の上限・下限")]
    public int HeightU;
    public int HeightB;

    private bool Player;
    
    public void SetTag(bool Player)
    {
        if(Player)
        {
            gameObject.tag = "BulletP";
        }
        if(!Player)
        {
            gameObject.tag = "BulletE";
        }

        this.Player = Player;
    }

    // Update is called once per frame
    void Update()
    {
        #region プレイヤー弾移動処理
        if (Player)
        {
            //上へGO
            transform.position += (new Vector3(0, 0, 1) * Speed) * Time.deltaTime;
            
            //画面端ぃ→消す
            if (transform.position.z >= HeightU)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region エネミー弾移動処理
        if(!Player)
        {
            //下へGO
            transform.position += (new Vector3(0, 0, -1) * Speed) * Time.deltaTime;

            //画面端ぃ→消す
            if (transform.position.z <= HeightB)
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }

    void OnTrigerEnter(Collider collision)
    {
        GameObject bullet;
        if (Player)
        {
            if (collision.gameObject.tag == "BulletE")
            {
                bullet = collision.gameObject;
            }
        }

        if(!Player)
        {
            if (collision.gameObject.tag == "BulletP")
            {
                bullet = collision.gameObject;
            }
        }
    }
}
