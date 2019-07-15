using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilManager : MonoBehaviour
{
    [SerializeField,Header("テスラコイルを飛ばす位置")]
    private List<GameObject> coilPositions = null;

    private int childNum; //子オブジェクトの数
    
    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトの数を参照
        childNum = transform.childCount;
        //参照した数だけ自動でリストに追加
        AddCoilPositions(childNum);
    }

    //// Update is called once per frame
    
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
