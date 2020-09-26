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
    float GetWallSign => isRunRightWall ? 1 : -1;

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
            default:
                break;
        }
        // animator.SetBool("Jump", playerState == PlayerState.Jump);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed);
    }

    void Jump()
    {
        playerState = PlayerState.Jump;
        Vector3[] path = new Vector3[]
        {
            new Vector3(wallsDistance * -GetWallSign/2f,2f,0),
            new Vector3(wallsDistance * -GetWallSign,0,0),
        };
        Sequence sequence = DOTween.Sequence()
        .Append(jumperTf.DOLocalPath(path, 0.5f, PathType.CatmullRom).SetRelative().SetEase(Ease.Linear))
        .Join(jumperTf.DOLocalRotate(new Vector3(0, 180 * GetWallSign, 0), 0.5f).SetRelative().SetEase(Ease.InSine))
        .OnComplete(() =>
        {
            isRunRightWall = !isRunRightWall;
            playerState = PlayerState.Run;
        });
    }

}
