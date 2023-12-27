using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerLogic : MonoBehaviour
{
    [Header("Stats")]
    public float Damage = 20f;
    public float TurnSpeed = 100f;
    public float Range = 3f;

    [Header("Unity setup")]
    public GameObject Turret;
    public LineRenderer[] Guns;

    GameObject _closestEnemy;
    bool _isAimed;


    AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch *= Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        FindEnemy();

        if (HasEnemy())
            Aim();
        else
            ToggleLasers(false);

        if (CanShoot())
            Shoot();
        else
            ToggleLasers(false);
    }

    void ToggleLasers(bool enabled)
    {
        for (int i = 0; i < Guns.Length; i++)
        {
            ToggleLaser(Guns[i], enabled);
        }
    }

    void Shoot()
    {
        float damagePerFrame = Damage * Time.deltaTime;

        _closestEnemy.GetComponent<EnemyBehaviour>().HurtEnemy(damagePerFrame);

        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetPosition(0, Guns[i].transform.position);

            RaycastHit hit;
            if (Physics.Raycast(Guns[i].transform.position, Guns[i].transform.forward, out hit, LayerMask.GetMask("Enemy")))
            {
                Guns[i].SetPosition(1, hit.point);

                Transform gunParticleTrans = Guns[i].GetComponentInChildren<ParticleSystem>().transform;

                gunParticleTrans.position = hit.point;
                gunParticleTrans.rotation = Quaternion.LookRotation(- Guns[i].transform.forward, Vector3.up);

                ToggleLaser(Guns[i], true);
            }
            else
            {
                ToggleLaser(Guns[i], false);
            }
        }
    }

    void ToggleLaser(LineRenderer lineRend, bool enabled)
    {
        lineRend.enabled = enabled;
        if (enabled)
        {
            ParticleSystem particleSystem = lineRend.GetComponentInChildren<ParticleSystem>();
            if (particleSystem.particleCount == 0)
            {
                particleSystem.Emit(1);
            }
            ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
            emissionModule.enabled = true;
            particleSystem.Play();
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else
        {
            lineRend.GetComponentInChildren<ParticleSystem>().Stop();
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }

    void Aim()
    {
        Vector3 dirToEnemy = (_closestEnemy.transform.position - Turret.transform.position);
        dirToEnemy = new Vector3(dirToEnemy.x, 0f, dirToEnemy.z).normalized;

        Quaternion currentRotation = Turret.transform.rotation;

        Quaternion corrrectRotation = Quaternion.LookRotation(dirToEnemy, Turret.transform.up);

        float maxFrameAngle = TurnSpeed * Time.deltaTime;

        Quaternion thisFrameRotation = Quaternion.RotateTowards(currentRotation, corrrectRotation, maxFrameAngle);

        Turret.transform.rotation = thisFrameRotation;

        float angleDiff = Mathf.DeltaAngle(currentRotation.eulerAngles.y, corrrectRotation.eulerAngles.y);

        if (Mathf.Abs(angleDiff) <= maxFrameAngle)
            _isAimed = true;
        else
            _isAimed = false;
    }
    void FindEnemy()
    {
        float distanceToNearestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in SpawningManager.shared.EnemiesAlive)
        {
            float distance = (transform.position - enemy.transform.position).magnitude;
            if (distance < distanceToNearestEnemy && distance <= Range)
            {
                distanceToNearestEnemy = distance;
                closestEnemy = enemy;
            }
        }

        _closestEnemy = closestEnemy;
    }

    bool HasEnemy()
    {
        return _closestEnemy != null;
    }

    bool CanShoot()
    {
        return HasEnemy() && _isAimed;
    }
}
