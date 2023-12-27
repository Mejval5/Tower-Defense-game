using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallLogic : ProjectileLogic
{
    public GameObject FireballEffect;
    public float ExplosionRadius;

    public override void HitEffects()
    {
        base.HitEffects();

        ParticleSystem.ShapeModule shape = FireballEffect.GetComponent<ParticleSystem>().shape;
        shape.radius = ExplosionRadius;

        GameObject fireballParticles = Instantiate(FireballEffect, transform.position, transform.rotation, null);
        Destroy(fireballParticles, 3f);
    }

    public override void HurtEnemy(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider col in hitColliders)
        {
            base.HurtEnemy(col);
        }
    }

    public void SetExplosionRadius(float explosionRadius)
    {
        ExplosionRadius = explosionRadius;
    }
}
