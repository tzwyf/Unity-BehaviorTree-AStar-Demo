using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform targetPos;
    private Seeker seeker;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        //添加回调
        seeker.pathCallback += OnPathComplete;
        Path path = seeker.StartPath(transform.position, targetPos.position);
    }

    private void OnPathComplete(Path p)
    {
        if(p != null)
        {
            Debug.Log("我找到路径了");
        }
    }

    private void OnDestroy()
    {
        //移除回调
        seeker.pathCallback -= OnPathComplete;
    }
}
