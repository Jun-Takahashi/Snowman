using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    [SerializeField, Header("ボスの弾")]
    private GameObject Bullet;

    private List<GameObject> bulletList;

    int cnt;

    float timeCnt;

    [SerializeField]
    float reCast;

    int bulletNum = 6;

    int timeNum = 2;

    float timeCnt2;

    bool shotFlag;

    int cnte = 0;

    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
        shotFlag = false;
        timeCnt = 0;
        timeCnt2 = 0;
        cnte = bulletNum;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            shotFlag = false;
        }

        timeCnt += Time.deltaTime;
        if (reCast <= timeCnt)
        {
            shotFlag = true;
            timeCnt = 0;
        }

        Debug.Log(shotFlag);
        if (shotFlag)
        {
            timeCnt2 += Time.deltaTime;
            if (0.3f <= timeCnt2)
            {
                if (cnte > 0)
                {
                    shot(Bullet);
                    cnte--;
                }
                else if (cnte <= 0)
                {
                    shotFlag = false;
                    cnte = bulletNum;
                }
                timeCnt2 = 0;
            }

        }

    }

    void shot(GameObject bullet)
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        Firing script = bulletInstance.GetComponent<Firing>();
        script.SetTag(false,"Enemy");
        //bulletList.Add(bulletInstance);
    }

    //void Fire(GameObject obj)
    //{
    //    obj.transform.position = obj.transform.position += new Vector3(0, 0, -1);
    //    shotFlag = false;
    //}
}
