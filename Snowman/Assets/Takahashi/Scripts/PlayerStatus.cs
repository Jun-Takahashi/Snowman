using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの移動速度")]
    public float Speed = 1;
    [SerializeField, Header("プレイヤーの弾")]
    public GameObject Bullet;
    [SerializeField, Header("最大チャージ")]
    public int MaxCharge = 5;
    [SerializeField, Header("チャージ速度")]
    public float ChargeSpeed = 2.0f;
    [SerializeField, Header("プレイヤーの連射速度")]
    public float FireSpeed = 1;
    public float Lv1FireSpeed = 1;

    private float span;
    private float Charge;
    private int chargeP;
    private Firing script;

    private GameObject ReSpawnGO;
    private bool Invincibly;
    private float InvinciblyTime;
    private float high;

    private bool active;
    private float activeTime;

    private int childCheck;//子オブジェクトが初期でいくつあるか
    private GameObject nuton;

    private float x, z;

    void Start()
    {
        span = FireSpeed;
        Charge = 0;
        chargeP = 1;
        x = 0; z = 0;

        ReSpawnGO = GameObject.Find("PlayerReSpawn");

        childCheck = transform.childCount;
        nuton = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        #region 移動処理
        #region　慣性のある移動、だが斜め移動が加速
        //float x = Input.GetAxis("Horizontal") / 2 * Speed;
        //float z = Input.GetAxis("Vertical") / 2 * Speed;

        //transform.position += new Vector3(x, 0, z);
        #endregion

        bool moveX = false, moveZ = false;

        #region 加速
        if (/*Input.GetKey(KeyCode.W)||//Wキー
            Input.GetKey(KeyCode.UpArrow) ||//アローキー*/
            Input.GetAxisRaw("Vertical") > 0)
        {
            if (z < 0) z += Time.deltaTime;
            if (z < 0.5f) z += Time.deltaTime;
            moveZ = true;
        }
        if (/*Input.GetKey(KeyCode.S)||//Sキー
            Input.GetKey(KeyCode.DownArrow) ||//アローキー*/
            Input.GetAxisRaw("Vertical") < 0)
        {
            if (z > 0) z -= Time.deltaTime;
            if (z > -0.5f) z -= Time.deltaTime;
            moveZ = true;
        }
        if (/*Input.GetKey(KeyCode.D) ||//Dキー
            Input.GetKey(KeyCode.RightArrow) ||//アローキー*/
            Input.GetAxisRaw("Horizontal") > 0)
        {
            if (x < 0) x += Time.deltaTime;
            if (x < 0.5f) x += Time.deltaTime;
            moveX = true;
        }
        if (/*Input.GetKey(KeyCode.A)||//Aキー
            Input.GetKey(KeyCode.LeftArrow) ||//アローキー*/
            Input.GetAxisRaw("Horizontal") < 0)
        {
            if (x > 0) x -= Time.deltaTime;
            if (x > -0.5f) x -= Time.deltaTime;
            moveX = true;
        }
        #endregion

        #region　減速
        if (!moveX)
        {
            if (x > 0)
            {
                x -= Time.deltaTime * 2;
                if (x < 0) x = 0;
            }
            else if (x < 0)
            {
                x += Time.deltaTime * 2;
                if (x > 0) x = 0;
            }
        }
        if (!moveZ)
        {
            if (z > 0)
            {
                z -= Time.deltaTime * 2;
                if (z < 0) z = 0;
            }
            else if (z < 0)
            {
                z += Time.deltaTime * 2;
                if (z > 0) z = 0;
            }
        }
        #endregion

        Vector3 move = new Vector3(x, 0, z);

        #region　正規化
        if (!(z == 0) && !(x == 0))//斜め移動の時正規化する
        {
            move = move / 1.18f;//ただのごり押し、雑魚
        }
        #endregion

        transform.position += move * Speed;

        #region　画面内におさめる
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        if (transform.position.z >= Screen.HeightU)
        {
            posZ = Screen.HeightU;
            z = 0;
        }
        if (transform.position.z <= Screen.HeightB)
        {
            posZ = Screen.HeightB;
            z = 0;
        }
        if (transform.position.x <= Screen.WidthL)
        {
            posX = Screen.WidthL;
            x = 0;
        }
        if (transform.position.x >= Screen.WidthR)
        {
            posX = Screen.WidthR;
            x = 0;
        }

        transform.position = new Vector3(posX, posY, posZ);
        #endregion

        #endregion

        #region 射撃管理
        if (transform.childCount == childCheck)
        {
            GameObject instanceB = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity);
            instanceB.transform.parent = transform;
            GameObject child = transform.GetChild(childCheck).gameObject;
            script = child.GetComponent<Firing>();
            script.Charge(chargeP);
        }

        span += Time.deltaTime * 2;
        Charge += Time.deltaTime * 2;
        if (chargeP == MaxCharge)//チャージ最大溜め
        {

        }
        else if (Charge >= ChargeSpeed)//チャージ一段階強化
        {
            chargeP += 1;
            Charge = 0;
            
            script.Charge(chargeP);//チャージ
        }

        if (Input.GetKeyDown(KeyCode.Space)||
            Input.GetButtonDown("Fire1"))
        {
            if(chargeP == 1)
            {
                if(span >= Lv1FireSpeed)
                {
                    script.SetTag(true, null);

                    chargeP = 1;
                    Charge = 0;
                    span = 0;
                }
            }
            else if (span >= FireSpeed)
            {
                script.SetTag(true, null);

                chargeP = 1;
                Charge = 0;
                span = 0;
            }
        }
        #endregion

        #region　復活処理
        if (Invincibly)
        {
            if(activeTime >= 0.5f)
            {
                if(active)
                {
                    active = false;
                }
                else
                {
                    active = true;
                }

                nuton.SetActive(active);
                activeTime = 0;
            }
            high += Time.deltaTime;
            activeTime += Time.deltaTime;
        }
        if(high>= InvinciblyTime)
        {
            Invincibly = false;
            high = 0;

            nuton.SetActive(true);
        }
        #endregion
    }

    void OnTriggerStay(Collider collision)
    {
        if (Invincibly == false)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                ReSpawn script = ReSpawnGO.GetComponent<ReSpawn>();
                script.SetPlace(transform.position);
                script.Death();
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "BulletE")
            {
                Destroy(collision.gameObject);

                ReSpawn script = ReSpawnGO.GetComponent<ReSpawn>();
                script.SetPlace(transform.position);
                script.Death();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 無敵時間設定
    /// </summary>
    /// <param name="invincibly"></param>
    public void ReSpawn(float invincibly)
    {
        InvinciblyTime = invincibly;
        Invincibly = true;
    }
}
