using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFac : MonoBehaviour
{
    [SerializeField,Header("パターン1")]
    public List<Vector3> pettern1;
    [SerializeField,Header("パターン2")]
    public List<Vector3> pettern2;
    [SerializeField,Header("パターン3")]
    public List<Vector3> pettern3;

    [SerializeField, Header("パターンを選ぶ　１～３")]
    public List<int> selectPettern;

    [SerializeField,Header("敵オブジェクト")]
    public GameObject Enemy;
    
    private List<GameObject> enemys;
    private float time = 0;
    private int count = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 

        if(time >= 5f)
        {
            Select(count);
            time = 0.0f;
            count++;
        }
    }

    void Select(int num)
    {
        if (selectPettern[num] == 1)
        {
            GameObject instanceE = Instantiate(Enemy, pettern1[0], Quaternion.identity);
            EnemyStatus script = instanceE.GetComponent<EnemyStatus>();
            script.SetPosition(pettern1);
        }
        if (selectPettern[num] == 2)
        {
            GameObject instanceE = Instantiate(Enemy, pettern2[0], Quaternion.identity);
            EnemyStatus script = instanceE.GetComponent<EnemyStatus>();
            script.SetPosition(pettern2);
        }
        if (selectPettern[num] == 3)
        {
            GameObject instanceE = Instantiate(Enemy, pettern3[0], Quaternion.identity);
            EnemyStatus script = instanceE.GetComponent<EnemyStatus>();
            script.SetPosition(pettern3);
        }
    }
}
