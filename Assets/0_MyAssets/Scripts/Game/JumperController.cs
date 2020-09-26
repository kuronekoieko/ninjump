using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MeshCutter;

public class JumperController : MonoBehaviour
{
    [SerializeField] Cutter cutter;
    public UnityAction<Collider> SetOnCollisionEnter;
    private void OnCollisionEnter(Collision other)
    {
        // SetOnCollisionEnter(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        //SetOnCollisionEnter(other);
        var cutterTarget = other.GetComponent<CutterTarget>();
        if (cutterTarget == null) return;
        cutter.Cut(cutterTarget, other.ClosestPoint(transform.position), Vector3.up);
    }
}
