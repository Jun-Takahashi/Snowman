using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public GameObject Respawn;
    [SerializeField,Header("PlayerのHPバー")]
    public Slider slider;

    private ReSpawn script;
    
    void Start()
    {
        script = Respawn.GetComponent<ReSpawn>();
        slider.maxValue = script.Hp;
    }
    
    void Update()
    {
        slider.value = script.Hp;
    }
}
