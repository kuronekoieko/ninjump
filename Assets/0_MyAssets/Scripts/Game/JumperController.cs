using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MeshCutter;
using Zenject;

public class JumperController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Cutter cutter;
    [SerializeField] Rigidbody rb;
    
    [Inject] GoalController goalController;
    [Inject] CameraController cameraController;
    public UnityAction<Collider> SetOnCollisionEnter;

    private void OnTriggerEnter(Collider other)
    {
        //SetOnCollisionEnter(other);
        HitTarget(other);
        HitObstacle(other);
        HitGoal(other);
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
        if (Variables.screenState != ScreenState.Game) return;
        Variables.screenState = ScreenState.Failed;
        playerController.Dead();
        rb.useGravity = true;
        Vector3 vel = Vector3.zero;
        vel.x = Mathf.Sign(other.transform.position.x) * -1 * 15f;
        vel.y = -10f;
        rb.velocity = vel;
    }

    void HitGoal(Collider other)
    {
        if (other.gameObject != goalController.gameObject) return;
        if (playerController.playerState == PlayerState.Goaled) return;
        if (playerController.playerState == PlayerState.Dead) return;
        if (Variables.screenState != ScreenState.Game) return;
        playerController.playerState = PlayerState.Goaled;
        transform.position = playerController.isRunRightWall ? goalController.StandPointR : goalController.StandPointL;
        Vector3 forward = cameraController.transform.position - transform.position;
        forward.y = 0;
        transform.forward = forward;
        playerController.Goaled();
        cameraController.ZoomOnGoaled(transform);
        Variables.screenState = ScreenState.Clear;
    }
}
