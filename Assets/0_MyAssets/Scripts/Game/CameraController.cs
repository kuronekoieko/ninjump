using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject] PlayerController playerController;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - playerController.transform.position;
    }

    private void LateUpdate()
    {
        if (playerController.playerState == PlayerState.Goaled) return;
        transform.position = playerController.transform.position + offset;
    }

    public void ZoomOnGoaled(Transform targetTf)
    {
        transform.DOMove(targetTf.position + offset / 2f, 1.5f);
    }
}
