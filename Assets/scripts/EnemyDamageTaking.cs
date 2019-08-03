using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaking : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
          
            GetComponentInParent<Enemy>().MyRigidbody2D.velocity = Vector2.zero;


            if (GetComponentInParent<Enemy>().IsFacingRight)
            {
                GetComponentInParent<Enemy>().MyRigidbody2D.AddForce(new Vector2(-200, 0));
            }
            else
            {
                GetComponentInParent<Enemy>().MyRigidbody2D.AddForce(new Vector2(200, 0));
            }

            GetComponentInParent<Enemy>().MyRigidbody2D.velocity = Vector2.zero;
        }
    }
}
