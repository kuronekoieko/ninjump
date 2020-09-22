using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    float speed = 0.1f;
    void Start()
    {

    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed);
    }
}
