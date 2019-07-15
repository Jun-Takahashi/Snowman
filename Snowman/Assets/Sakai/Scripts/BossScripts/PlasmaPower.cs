using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaPower : MonoBehaviour
{
    public ParticleSystem particle = null;
    
    public float plasmaPower = 1;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float TamaSize = transform.localScale.x / 10;
        particle.transform.localScale = new Vector3(TamaSize, TamaSize, TamaSize);
        particle.transform.GetChild(0).localScale = new Vector3(TamaSize * 40, TamaSize * 40,TamaSize * 40);
        
    }
}

