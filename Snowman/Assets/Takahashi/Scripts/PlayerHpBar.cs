using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public GameObject Player;
    [SerializeField,Header("PlayerのHPバー")]
    public Slider slider;

    private PlayerStatus script;
    
    void Start()
    {
        script = Player.GetComponent<PlayerStatus>();
        slider.maxValue = script.Hp;
    }
    
    void Update()
    {
        slider.value = script.Hp;
    }
}
