using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkScript : MonoBehaviour
{
    int num;
    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null && num==0)
        {
            gameObject.AddComponent<Rigidbody>();//重力を与える
            num++;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Ground")//床だったら
        {
            Destroy(gameObject);//消える
        }
    }
}
