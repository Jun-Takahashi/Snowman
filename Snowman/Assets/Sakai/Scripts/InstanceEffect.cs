using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceEffect : MonoBehaviour
{
    [SerializeField, Header("表示したいパーティクル")]
    public ParticleSystem particle = null;

    public bool startEffect = false;


    // Start is called before the first frame update
    void Start()
    {
        startEffect = false;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (startEffect)
    //    {

    //    }
    //}

    public void EffectInstance(Vector3 pos)
    {
        if (!startEffect)
        {
            particle.transform.position = pos;
            Instantiate(particle);
            //particle.Play();
            startEffect = true;
        }
    }

}
