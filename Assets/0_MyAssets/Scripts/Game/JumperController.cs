using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MeshCutter;

public class JumperController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Cutter cutter;
    [SerializeField] Rigidbody rb;
    public UnityAction<Collider> SetOnCollisionEnter;
    private void OnCollisionEnter(Collision other)
    {
        // SetOnCollisionEnter(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        //SetOnCollisionEnter(other);
        HitTarget(other);
        HitObstacle(other);
    }

    void HitTarget(Collider other)
    {
        if (playerController.playerState != PlayerState.Jump) return;
        var cutterTarget = other.GetComponent<CutterTarget>();
        if (cutterTarget == null) return;
        cutter.Cut(cutterTarget, other.ClosestPoint(transform.position), Vector3.up);
    }

    void HitObstacle(Collider other)
    {
        if (!other.gameObject.CompareTag("Obstacle")) return;
        playerController.Dead();
        rb.useGravity = true;
        Vector3 vel = Vector3.zero;
        vel.x = playerController.GetWallSign * -1 * 15f;
        vel.y = -10f;
        rb.velocity = vel;
    }
}
