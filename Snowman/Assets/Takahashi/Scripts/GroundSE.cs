using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSE : MonoBehaviour
{
    private AudioSource JunkDropSE;

    // Start is called before the first frame update
    void Start()
    {
        JunkDropSE = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        JunkDropSE.PlayOneShot(JunkDropSE.clip);
    }
}
