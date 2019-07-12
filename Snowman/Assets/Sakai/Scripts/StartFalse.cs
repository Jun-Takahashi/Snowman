using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFalse : MonoBehaviour
{
    public bool hpFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(false);
        hpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpFlag)
        {
            transform.gameObject.SetActive(true);
        }

    }

    public bool HpFlagChange()
    {
        hpFlag = true;
        return hpFlag;
    }
}
