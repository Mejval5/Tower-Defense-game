using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public GameObject DeathParticle;
    
    protected float _damage = 0f;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HitEnemy(other);
        }
    }

    private void HitEnemy(Collider other)
    {
        HitEffects();
        HurtEnemy(other);

        Destroy(gameObject);
    }

    public virtual void HurtEnemy(Collider other)
    {
        other.GetComponent<EnemyBehaviour>()?.HurtEnemy(_damage);
    }

    public virtual void HitEffects()
    {
        GameObject deathParticles = Instantiate(DeathParticle, transform.position, transform.rotation, null);
        Destroy(deathParticles, 3f);
    }

}
