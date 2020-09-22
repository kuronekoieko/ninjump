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
    float speed = 0.2f;
    PlayerState playerState = PlayerState.Run;
    bool isRunRightWall;
    float wallsDistance;

    void Start()
    {
        isRunRightWall = true;
        wallsDistance = Mathf.Abs(transform.position.x) * 2;
    }


    private void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Run:
                transform.Translate(Vector3.right * speed);
                if (Input.GetMouseButtonDown(0))
                {

                }
                break;
            case PlayerState.Jump:
                break;
            default:
                break;
        }
    }
}
