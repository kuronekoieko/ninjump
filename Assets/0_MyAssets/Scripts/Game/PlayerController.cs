using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState
{
    Run,
    Jump,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform jumperTf;
    [SerializeField] JumperController jumperController;
    float speed = 0.2f;
    PlayerState playerState = PlayerState.Run;
    bool isRunRightWall;
    float wallsDistance;
    Tween jumpTween;
    float GetSign => isRunRightWall ? 1 : -1;

    void Start()
    {
        isRunRightWall = true;
        wallsDistance = Mathf.Abs(jumperTf.position.x) * 2;
        jumperController.SetOnCollisionEnter += OnHitWall;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed);

        switch (playerState)
        {
            case PlayerState.Run:
                if (Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case PlayerState.Jump:
                break;
            default:
                break;
        }
    }

    void Jump()
    {
        playerState = PlayerState.Jump;
        Vector3[] path = new Vector3[]
        {
            new Vector3(wallsDistance * -GetSign/2f,2f,0),
            new Vector3(wallsDistance * -GetSign,0,0),
        };
        jumpTween = jumperTf
            .DOLocalPath(path, 0.5f, PathType.CatmullRom)
            .SetRelative()
            .SetEase(Ease.Linear);
    }

    void OnHitWall(Collider other)
    {
        if (!other.gameObject.CompareTag("Wall")) return;
        if (playerState != PlayerState.Jump) return;
        playerState = PlayerState.Run;
        isRunRightWall = !isRunRightWall;
        //jumpTween.Kill();
        jumperTf.RotateAround(jumperTf.position, Vector3.up, 180 * GetSign);
        Debug.Log(0);
    }

}
