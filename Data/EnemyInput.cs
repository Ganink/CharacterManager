using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    public Transform target;
    public float horizontal { get { return dirToTarget.x; } }
    public float vertical { get { return dirToTarget.y; } }
    public float rangePlayer { get { return dirToTarget.magnitude; } } //magnitude
    public Vector2 dirToTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NPCManager>())
        {
            target = collision.transform;
            DefineDirToTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }


    private void DefineDirToTarget()
    {
        dirToTarget = target.position - transform.position;
    }
}