using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilMove : MonoBehaviour
{
    GameObject Coil = null;
    Vector3 distance;
    float num;

    public bool SparkFlag;

    bool MoveFlag;
    // Start is called before the first frame update
    void Start()
    {
        SparkFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveFlag)
        {
            transform.position = Vector3.Lerp(transform.position, distance, num);
        }
        if (Vector3.Distance(transform.position, distance) < 0.5f && !SparkFlag)
        {
            SparkFlag = true;
        }
        
    }

    public void Move(Vector3 distance, float num)
    {
        MoveFlag = true;
        this.distance = distance;
        this.num = num;
    }

    public void Spark(int size)
    {
        Debug.Log(555);
        transform.localScale = new Vector3(size, 0.1f, size);
        SparkFlag = false;
    }
}
