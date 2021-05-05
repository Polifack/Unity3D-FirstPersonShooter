using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public EnemyController ec;

    public abstract void onStateEnter();
    public abstract void onStateExit();
    public abstract void stateUpdate();

    public EnemyState toState(EnemyState newState)
    {
        onStateExit();
        
        newState.ec = ec;
        newState.onStateEnter();

        return newState;
    }
}

public class EnemyIdleState : EnemyState
{
    public override void onStateEnter()
    {
        //
    }

    public override void onStateExit()
    {
        //
    }

    public override void stateUpdate()
    {
        //
    }
}

public class EnemyShootState : EnemyState
{
    public override void onStateEnter()
    {
        ec.animator.SetBool("ShootingState", true);
    }

    public override void onStateExit()
    {
        ec.animator.SetBool("ShootingState", false);
    }

    public override void stateUpdate()
    {
        //
    }
}

public class EnemyWalkState : EnemyState
{
    private Vector3[] path;
    private Vector3 currentWaypoint;
    private int pathIndex = 0;

    public override void onStateEnter()
    {
        ec.animator.SetBool("WalkState", true);
    }
    public override void onStateExit()
    {
        ec.animator.SetBool("WalkState", false);
    }
    private void onPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess && newPath.Length>0)
        {
            path = newPath;
            pathIndex = 0;
            currentWaypoint = path[pathIndex];
        }
        else
        {
            path = null;
        }
        ec.pathRequest.isProcessingPath = false;
    }
    private void checkPath()
    {
        if (path == null)
        {
            ec.pathRequest.RequestPath(ec.transform.position, ec.enemyTarget.transform.position, onPathFound);
        }
        else
        {
            if (ec.transform.position == currentWaypoint)
            {
                currentWaypoint = path[pathIndex];
                pathIndex++;
                if (pathIndex >= path.Length)
                {
                    ec.pathRequest.RequestPath(ec.transform.position, ec.enemyTarget.transform.position, onPathFound);
                    pathIndex = 0;
                }
            }
            ec.transform.position = Vector3.MoveTowards(ec.transform.position, currentWaypoint, ec.movementSpeed * Time.deltaTime);
        }
    }

    public override void stateUpdate()
    {
        ec.checkShoot();
        checkPath();
    }
}

public class EnemyDieState : EnemyState
{
    public override void onStateEnter()
    {
        ec.animator.SetBool("DyingState", true);
    }

    public override void onStateExit()
    {
        //
    }

    public override void stateUpdate()
    {
        //
    }
}
