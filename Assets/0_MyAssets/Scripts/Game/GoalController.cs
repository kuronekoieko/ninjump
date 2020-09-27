using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] Transform standPointLTf;
    [SerializeField] Transform standPointRTf;
    public Vector3 StandPointL => standPointLTf.position;
    public Vector3 StandPointR => standPointRTf.position;

}
