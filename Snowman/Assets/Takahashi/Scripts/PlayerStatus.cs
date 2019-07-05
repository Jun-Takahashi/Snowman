using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField,Header("プレイヤーの残機")]
    public int Hp;
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

    private float span;
    private float Charge;
    private int chargeP;
    private Firing script;

    private float x, z;

    void Start()
    {
        span = FireSpeed;
        Charge = 0;
        chargeP = 1;
        x = 0; z = 0;
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
        if (Input.GetKey(KeyCode.W))
        {
            if (z < 0) z += Time.deltaTime;
            if (z < 0.5f) z += Time.deltaTime;
            moveZ = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (z > 0) z -= Time.deltaTime;
            if (z > -0.5f) z -= Time.deltaTime;
            moveZ = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (x < 0) x += Time.deltaTime;
            if (x < 0.5f) x += Time.deltaTime;
            moveX = true;
        }
        if (Input.GetKey(KeyCode.A))
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
        if (transform.IsChildOf(transform))
        {
            GameObject instanceB = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity);
            instanceB.transform.parent = transform;
            GameObject child = transform.GetChild(0).gameObject;
            script = child.GetComponent<Firing>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (span >= FireSpeed)
            {
                script.SetTag(true, null);

                chargeP = 1;
                Charge = 0;
                span = 0;
            }
        }
        #endregion

        #region 残機管理

        if (Hp<=0)
        {
            SceneManager.LoadScene("EndingScene");
            Destroy(gameObject);
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
