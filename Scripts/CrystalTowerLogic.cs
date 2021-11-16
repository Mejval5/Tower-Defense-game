using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTowerLogic : MonoBehaviour
{
    public float Slowdown;
    public float Range;

    CapsuleCollider _collider;
    List<EnemyBehaviour> _slowedEnemies = new List<EnemyBehaviour>();


    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch *= Random.Range(0.8f, 1.2f);

        _collider = GetComponent<CapsuleCollider>();
        _collider.radius = Range;

        ParticleSystem.ShapeModule shape = GetComponentInChildren<ParticleSystem>().shape;
        shape.radius = Range;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _slowedEnemies.Add(other.GetComponent<EnemyBehaviour>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _slowedEnemies.Remove(other.GetComponent<EnemyBehaviour>());
        }
    }

    void Update()
    {
        foreach (EnemyBehaviour enemy in _slowedEnemies)
        {
            enemy.SlowThisEnemy(Slowdown);
        }
    }
}
