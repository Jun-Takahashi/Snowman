using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoilMove : MonoBehaviour
{
    [SerializeField]
    private GameObject particle = null;

    Vector3 distance; //目的地（ローカル）
    float num; //速度（ローカル）

    public bool SparkFlag; //放電してるかしてないか

    private bool MoveFlag; //動かすフラグ

    AudioSource audioSource;
    [SerializeField]
    public AudioClip coilSE = null;

    // Start is called before the first frame update
    void Start()
    {
        SparkFlag = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveFlag)
        {
            GoToPoint(transform.root.gameObject, distance, num);
        }
        if (DistanceObject(transform.root.gameObject, distance) && !SparkFlag)
        {
            SparkFlag = true;
        }

    }

    /// <summary>
    /// 何処へ向かわせるか
    /// </summary>
    /// <param name="distance">目的地</param>
    /// <param name="num">速度</param>
    public void Move(Vector3 distance, float num)
    {
        MoveFlag = true;
        this.distance = distance;
        this.num = num;
    }

    /// <summary>
    /// 放電させる
    /// </summary>
    /// <param name="size">放電のサイズ</param>
    public void Spark(int size, float Psize)
    {
        transform.localScale = new Vector3(size, 1f, size);
        particle.transform.localScale = new Vector3(Psize, 0.1f, Psize);
        audioSource.PlayOneShot(coilSE);
        SparkFlag = false;
    }

    /// <summary>
    /// その場所へ向かわせる
    /// </summary>
    /// <param name="obj">向かわせるオブジェクト</param>
    /// <param name="point">向かわせる場所</param>
    /// <param name="number">向かわせる速度</param>
    void GoToPoint(GameObject obj, Vector3 point, float number)
    {
        obj.transform.position = Vector3.Lerp(transform.position, point, number);
    }

    /// <summary>
    /// オブジェクトの距離を測る（目的地に着いているか）
    /// </summary>
    /// <param name="obj">オブジェクト</param>
    /// <param name="point">目的の場所</param>
    /// <returns>距離が一致していたらtrueを返す</returns>
    bool DistanceObject(GameObject obj, Vector3 point)
    {
        bool flag = Vector3.Distance(obj.transform.position, point) < 1f;
        return flag;
    }
}
