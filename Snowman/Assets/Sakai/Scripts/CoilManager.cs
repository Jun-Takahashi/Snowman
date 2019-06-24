using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilManager : MonoBehaviour
{
    [SerializeField,Header("テスラコイルの数")]
    private List<GameObject> coilPositions;

    int childNum; //子オブジェクトの数




    // Start is called before the first frame update
    void Start()
    {
        childNum = transform.childCount;
        AddCoilPositions(childNum);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// コイルリストに追加する処理
    /// </summary>
    /// <param name="num">追加する数</param>
    void AddCoilPositions(int num)
    {
        for (int i = 0; i < num; i++)
        {
            coilPositions.Add(transform.GetChild(i).gameObject);
        }
    }
}
