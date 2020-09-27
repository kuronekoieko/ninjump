using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState
{
    Waiting,
    Run,
    Jump,
    Dead,
    Goaled,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform jumperTf;
    [SerializeField] JumperController jumperController;
    float speed = 0.2f;
    [System.NonSerialized] public PlayerState playerState = PlayerState.Waiting;
    [System.NonSerialized] public bool isRunRightWall;
    float wallsDistance;
    float GetWallSign => isRunRightWall ? 1 : -1;
    Sequence jumpSequence;
    void Start()
    {
        isRunRightWall = true;
        //jumperController.SetOnCollisionEnter += OnHitWall;
    }

    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.Waiting:
                if (Input.GetMouseButtonDown(0))
                {
                    StartJump();
                }
                break;
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

    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Dead) return;
        if (playerState == PlayerState.Goaled) return;
        if (playerState == PlayerState.Waiting) return;
        transform.Translate(Vector3.up * speed);
    }

    void StartJump()
    {
        float duration = 0.2f;
        DOTween.Sequence()
        .Append(jumperTf.DOLocalMove(new Vector3(7.54f, 3f, 0), duration).SetEase(Ease.Linear))
        .Join(jumperTf.DOLocalRotate(new Vector3(0, 0, 90), duration))
        .OnComplete(() =>
        {
            isRunRightWall = true;
            playerState = PlayerState.Run;
            wallsDistance = Mathf.Abs(jumperTf.position.x) * 2;
        });
    }

    void Jump()
    {
        animator.SetBool("Jump", true);
        playerState = PlayerState.Jump;
        Vector3[] path = new Vector3[]
        {
            new Vector3(wallsDistance * -GetWallSign/2f,0.5f,0),
            new Vector3(wallsDistance * -GetWallSign,0,0),
        };
        float duration = 0.5f;
        Vector3 a = jumperTf.eulerAngles;
        jumperTf.localEulerAngles = Vector3.zero;
        jumpSequence = DOTween.Sequence()
        .Append(jumperTf.DOLocalPath(path, duration, PathType.CatmullRom).SetRelative().SetEase(Ease.Linear))
        //.Join(jumperTf.DOLocalRotate(new Vector3(35f * -GetWallSign, 180f * GetWallSign, 0), duration).SetRelative().SetEase(Ease.InSine))
        .OnComplete(() =>
        {
            jumperTf.eulerAngles = a + new Vector3(35f * -GetWallSign, 180f * GetWallSign, 0);
            isRunRightWall = !isRunRightWall;
            playerState = PlayerState.Run;
            animator.SetBool("Jump", false);
        });
    }

    public void Dead()
    {
        playerState = PlayerState.Dead;
        animator.SetBool("Death", true);
        jumpSequence.Kill();
    }


    public void Goaled()
    {
        jumpSequence.Kill();
        animator.SetBool("Jump", false);
        animator.transform.localEulerAngles = Vector3.zero;
        animator.SetBool("Dance", true);
    }
}
