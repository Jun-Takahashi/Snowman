using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }
}
