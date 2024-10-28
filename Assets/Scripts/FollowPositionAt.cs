using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositionAt : MonoBehaviour
{
    // target
    public Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
