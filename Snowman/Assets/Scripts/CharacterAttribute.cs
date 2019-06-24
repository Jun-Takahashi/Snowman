using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharaAttribute
{
    plus,
    minus,
};

public class CharacterAttribute : MonoBehaviour
{
    [SerializeField]
    private List<List<string>> a = new List<List<string>>();




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPosition(Vector3 pos1, Vector3 pos2, Vector3 pos3)
    {
        Vector3 position = pos1;
    }
}
