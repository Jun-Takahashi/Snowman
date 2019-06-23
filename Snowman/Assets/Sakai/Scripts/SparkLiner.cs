using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkLiner : MonoBehaviour
{
    public GameObject coilPositions;

    public BossManager bossManager;

    private float timeElapsed;

    [SerializeField, Header("テスラコイル")]
    public GameObject coil;

    GameObject coilInstance = null;

    [SerializeField, Header("サンダーパワー")]
    public int ThunderPower = 1;

    [SerializeField, Header("放電開始")]
    public float SparkTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        coilPositions = null;
        bossManager = GetComponent<BossManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (coilPositions == null && bossManager.sparkFlag)
        {
            coilPositions = GameObject.FindGameObjectWithTag("CoilPos");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            NewCoil();
        }
        if (coilInstance != null && coilInstance.GetComponent<CoilMove>().SparkFlag)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > SparkTime)
            {
                Debug.Log("放電せよ！");
                Sparking(coilInstance);
                if (timeElapsed > (SparkTime * 1.5))
                {
                    Destroy(coilInstance);
                    timeElapsed = 0.0f;
                }
            }
        }
    }

    void Shot(GameObject coil)
    {
        coil.GetComponent<CoilMove>().Move(coilPositions.transform.GetChild(Random.Range(0, 2)).transform.position, 0.2f);
    }

    void Sparking(GameObject coil)
    {
        coil.GetComponent<CoilMove>().Spark(ThunderPower);
    }

    void NewCoil()
    {
        coilInstance = Instantiate(coil);
        coilInstance.transform.position = transform.position;
        Shot(coilInstance);
    }

}
