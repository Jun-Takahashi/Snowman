using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField,Header("プレイヤーの残機")]
    public int Hp;
    [SerializeField, Header("プレイヤーの弾")]
    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region 移動処理

        float x = Input.GetAxis("Horizontal") / 2;
        float z = Input.GetAxis("Vertical") / 2;

        transform.position += new Vector3(x, 0, z);

        #endregion

        #region 残機管理

        if (Hp==10)
        {
            Destroy(gameObject);
        }

        #endregion

        #region 射撃管理

        if(Input.GetKey(KeyCode.Space))
        {
            Instantiate(Bullet,this.transform.position,Quaternion.identity);
        }
        #endregion
    }
}
