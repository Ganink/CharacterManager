using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(HitBoxItem))]
public class EnemyManager : NPCManagerBase
{
    public float Distance = 3f;
    private Transform player;

    void FixedUpdate () {
        FindHero();
    }

    private void FindHero ()
    {
        if(player == null)
            player = FindObjectOfType<NPCManager>().transform;
        
        if (Vector2.Distance(player.transform.position, transform.position) < Distance) {
            //when the distance between the character and the enemy is less than the distance
            //we have set, the enemy will look at our character's position.

            //Vector3 direccion = player.position - transform.position;
            //float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
            //transform.Rotate(0, 0, -90);

            transform.position = Vector2.MoveTowards(transform.position, player.position, GetCharacterAttributes().speedUser * Time.fixedDeltaTime);
        } 
    }

    private void OnDrawGizmos () {
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}
