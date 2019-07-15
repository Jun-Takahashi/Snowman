using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDelete : MonoBehaviour
{
    [SerializeField, Header("パーティクルが消える時間")]
    public float deathTime = 3;
    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount = TimeCounter(timeCount);
        if (timeCount >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    float TimeCounter(float Count)
    {
        Count += Time.deltaTime;
        return Count;
    }
}
