using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumperController : MonoBehaviour
{
    public UnityAction<Collider> SetOnCollisionEnter;
    private void OnCollisionEnter(Collision other)
    {
        // SetOnCollisionEnter(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        //SetOnCollisionEnter(other);
    }
}
