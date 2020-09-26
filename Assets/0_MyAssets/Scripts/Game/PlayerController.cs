using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState
{
    Run,
    Jump,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform jumperTf;
    [SerializeField] JumperController jumperController;
    float speed = 0.2f;
    [System.NonSerialized] public PlayerState playerState = PlayerState.Run;
    bool isRunRightWall;
    float wallsDistance;
    float GetWallSign => isRunRightWall ? 1 : -1;
    Sequence jumpSequence;
    void Start()
    {
        isRunRightWall = true;
        wallsDistance = Mathf.Abs(jumperTf.position.x) * 2;
        //jumperController.SetOnCollisionEnter += OnHitWall;
    }

    private void Update()
    {
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
            case PlayerState.Dead:
                break;
            default:
                break;
        }
        // animator.SetBool("Jump", playerState == PlayerState.Jump);

    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Dead) return;
        transform.Translate(Vector3.up * speed);
    }

    void Jump()
    {
        playerState = PlayerState.Jump;
        Vector3[] path = new Vector3[]
        {
            new Vector3(wallsDistance * -GetWallSign/2f,0.5f,0),
            new Vector3(wallsDistance * -GetWallSign,0,0),
        };
        float duration = 0.3f;
        jumpSequence = DOTween.Sequence()
        .Append(jumperTf.DOLocalPath(path, duration, PathType.CatmullRom).SetRelative().SetEase(Ease.Linear))
        .Join(jumperTf.DOLocalRotate(new Vector3(35f * -GetWallSign, 180f * GetWallSign, 0), duration).SetRelative().SetEase(Ease.InSine))
        .OnComplete(() =>
        {
            isRunRightWall = !isRunRightWall;
            playerState = PlayerState.Run;
        });
    }

    public void Dead()
    {
        playerState = PlayerState.Dead;
        animator.SetBool("Death", true);
        jumpSequence.Kill();
    }

}
