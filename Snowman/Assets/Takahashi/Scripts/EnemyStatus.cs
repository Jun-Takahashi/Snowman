using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private List<Vector3> moveP;
    public int Speed;
    
    private Vector3 target;
    private int targetC;
    private Vector3 LastPos;

    // Start is called before the first frame update
    public void SetPosition(List<Vector3> moveP)
    {
        this.moveP = moveP;
        targetC = 1;
        target = moveP[targetC];
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == moveP[targetC])
        {
            targetC++;
            target = moveP[targetC];
        }
        
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime* Speed);

        if (transform.position == moveP[moveP.Count -1])
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "BulletP")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
