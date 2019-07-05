using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFac : MonoBehaviour
{
    [SerializeField,Header("Enemyの移動指定")]
    public List<ValueListVector3> MoveList;

    [SerializeField, Header("十の位：Enemyの種類、一の位：移動petternの指定")]
    public List<ValueListInt> selectPattern;

    [SerializeField,Header("敵オブジェクト")]
    public List<GameObject> Enemys;

    [SerializeField, Header("ボスオブジェクト")]
    public GameObject Boss;

    private float time = 0;
    private int nextEnemy = 0;//次に生成されるEnemy

    //waveを進める変数たち
    private List<int> waveList;
    private int waveCount = 0;//何wave目か
    private int enemyCount;//今のwaveにいるEnemyは何体？
    private int deathCount = 0;//倒されたEnemyの数は？
    private int BossWave = 1;//ボスWaveへGO
    
    void Start()
    {
        waveList = selectPattern[waveCount].List();
        enemyCount = waveList.Count;
    }

    void Update()
    {
        time += Time.deltaTime; 

        if(time >= 0.5f && nextEnemy < enemyCount)
        {
            Select(waveList[nextEnemy]);
            time = 0.0f;
            nextEnemy++;
        }

        if(enemyCount == deathCount)
        {
            waveCount++;
            waveList = selectPattern[waveCount].List();
            enemyCount = waveList.Count;
            nextEnemy = 0;
            deathCount = 0;

            BossWave++;
        }

        if(BossWave == selectPattern.Count)
        {
            Instantiate(Boss, new Vector3(0, 1, 10), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Select(int num)
    {
        GameObject instanceE = Instantiate(Enemys[num/100], MoveList[num%100].Select(0), Quaternion.identity);
        EnemyStatus script = instanceE.GetComponent<EnemyStatus>();
        script.SetPosition(MoveList[num%100].List());
    }

    public void Count()
    {
        deathCount++;
    }
}

#region 二次元配列作成クラス(Vector3)
[System.SerializableAttribute]
public class ValueListVector3
{
    public List<Vector3> pettern = new List<Vector3>();

    public ValueListVector3(List<Vector3> list)
    {
        pettern = list;
    }

    /// <summary>
    /// List自体を呼び出す
    /// </summary>
    /// <returns></returns>
    public List<Vector3> List()
    {
        return pettern;
    }

    /// <summary>
    /// Listの中身を直接呼び出す
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public Vector3 Select(int num)
    {
        return pettern[num];
    }
}
#endregion

#region 二次元配列作成クラス(int)
[System.SerializableAttribute]
public class ValueListInt
{
    public List<int> pettern = new List<int>();

    public ValueListInt(List<int> list)
    {
        pettern = list;
    }

    /// <summary>
    /// List自体を呼び出す
    /// </summary>
    /// <returns></returns>
    public List<int> List()
    {
        return pettern;
    }
}
#endregion
