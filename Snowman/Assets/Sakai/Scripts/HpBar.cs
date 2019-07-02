using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField, Header("ボスの体力ゲージ")]
    public Slider hpBar = null;

    [SerializeField]
    public BossManager bossManager = null;

    private float hp { get { return bossManager.BossHp; } }

    // Start is called before the first frame update
    void Start()
    {
        hpBar = FindObjectOfType<Slider>();
        hpBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp;
    }
}

