using UnityEngine;
using System.Collections;
using Pathfinding;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class MyAStarPlayer : Action
{
    //目标位置;
    public Transform target;

    Seeker seeker;
    //CharacterController characterController;

    //计算出来的路线;
    Path path;

    //移动速度;
    float playerMoveSpeed = 10f;

    //当前点
    int currentWayPoint = 0;

    bool stopMove = true;

    //Player中心点;
    float playerCenterY = 1.0f;

    private float doorX = -12;
    private float doorZ = -11;

    public override void OnAwake()
    {
        
    }

    // Use this for initialization
    public override void OnStart()
    {
        seeker = GetComponent<Seeker>();

        playerCenterY = transform.localPosition.y;

        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }
    //寻路结束;
    public void OnPathComplete(Path p)
    {
        Debug.Log("OnPathComplete error = " + p.error);

        if (!p.error)
        {
            Debug.Log("有找到路径");
            currentWayPoint = 0;
            path = p;
            stopMove = false;
        }

        for (int index = 0; index < path.vectorPath.Count; index++)
        {
            Debug.Log("path.vectorPath[" + index + "]=" + path.vectorPath[index]);
        }
    }

    public override TaskStatus OnUpdate()
    {
        //Debug.Log("transform.localEulerAngles.y:" + transform.localEulerAngles.y);
        if (transform.position.z < doorZ)//房间A
        {
            if (transform.position.x < doorX)
            {
                if (transform.localEulerAngles.y > 180)//进门
                {
                    Debug.Log("进门");
                    GlobalVariables.Instance.SetVariableValue("isRoomAEmpty", false);
                }
            }else if (transform.position.x > doorX)
            {
                if (transform.localEulerAngles.y < 180)//出门
                {
                    Debug.Log("出门");
                    GlobalVariables.Instance.SetVariableValue("isRoomAEmpty", true);
                }
            }
        }
        else//房间B
        {
            if (transform.position.x < doorX)
            {
                if (transform.localEulerAngles.y > 180)//进门
                {
                    Debug.Log("进门");
                    GlobalVariables.Instance.SetVariableValue("isRoomBEmpty", false);
                }
            }else if (transform.position.x > doorX)
            {
                if (transform.localEulerAngles.y < 180)//出门
                {
                    Debug.Log("出门");
                    GlobalVariables.Instance.SetVariableValue("isRoomBEmpty", true);
                }
            }
        }


        //依据Player当前位置和 下一个寻路点的位置，计算方向;
        if (path == null) return TaskStatus.Running;
        Vector3 currentWayPointV = new Vector3(path.vectorPath[currentWayPoint].x, path.vectorPath[currentWayPoint].y + playerCenterY, path.vectorPath[currentWayPoint].z);
        Vector3 dir = (currentWayPointV - transform.position).normalized;

        //使物体面向移动的方向
        transform.forward = dir;

        //计算这一帧要朝着 dir方向 移动多少距离;
        dir *= playerMoveSpeed * Time.fixedDeltaTime;

        //计算加上这一帧的位移，是不是会超过下一个节点;
        float offset = Vector3.Distance(transform.localPosition, currentWayPointV);

        if (offset < 0.1f)
        {
            transform.localPosition = currentWayPointV;

            currentWayPoint++;

            if (currentWayPoint == path.vectorPath.Count)
            {
                stopMove = true;

                currentWayPoint = 0;
                path = null;
                return TaskStatus.Success;
            }
        }
        else
        {
            if (dir.magnitude > offset)
            {
                Vector3 tmpV3 = dir * (offset / dir.magnitude);
                dir = tmpV3;

                currentWayPoint++;

                if (currentWayPoint == path.vectorPath.Count)
                {
                    stopMove = true;

                    currentWayPoint = 0;
                    path = null;
                    return TaskStatus.Success;
                }
            }
            transform.localPosition += dir;
        }

        return TaskStatus.Running;
    }
}
