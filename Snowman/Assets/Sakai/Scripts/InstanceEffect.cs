using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public void EffectInstance(Vector3 pos)
    {
        if (!startEffect)
        {
            particle.transform.position = pos;
            Instantiate(particle);
            startEffect = true;
        }
    }
    
}
