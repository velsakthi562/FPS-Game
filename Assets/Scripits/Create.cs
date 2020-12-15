using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    float radius = 5f;
    public float health = 50f;
    public GameObject explosiveEffect;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health< 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Instantiate(explosiveEffect, transform.position, transform.rotation);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToDestroy)
        {

            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destory();
            }
        }


        Destroy(gameObject);
    }
}
