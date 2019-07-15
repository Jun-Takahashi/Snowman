using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCount : MonoBehaviour
{
    //[SerializeField, Header("現在時間タイマー")]
    //private float countTime = 0;

    [SerializeField, Header("制限時間")]
    public float countDown = 180;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //countTime += Time.deltaTime;
        if (countDown > 0)
        {
            countDown = countDown -= Time.deltaTime;
            GetComponent<Text>().text = countDown.ToString("F2");
        }
        //GetComponent<Text>().text = countTime.ToString("F2");
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SceneManager.LoadScene("sakai");
        //}
    }
}
