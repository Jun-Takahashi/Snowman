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
        //HpFlagChange();

    }

    public void HpFlagChange()
    {
        transform.gameObject.SetActive(true);
        hpFlag = true;
    }
}
