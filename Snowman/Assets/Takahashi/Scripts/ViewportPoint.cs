using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewportPoint : MonoBehaviour
{
    [SerializeField]
    Camera targetCamera;

    [SerializeField]
    Transform targetObj;

    Vector3 lb;
    Vector3 rt;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GetComponent<Camera>();

        lb = GetRayhitPos(new Vector3(0, 0, 0));
        rt = GetRayhitPos(new Vector3(1, 1, 0));
        Debug.Log(lb);
        Debug.Log(rt);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckInScreen())
        {
            Debug.Log("GamenNai");
        }
        else
        {
            Debug.Log("GamenGai");
        }
    }

    Vector3 GetRayhitPos(Vector3 targetPos)
    {
        Ray ray = targetCamera.ViewportPointToRay(targetPos);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    bool CheckInScreen()
    {
        if (targetObj.position.x < rt.x || lb.x < targetObj.position.x ||
            targetObj.position.z < lb.z || rt.z < targetObj.position.z)
        {
            return false;
        }

        return true;
    }
}
