using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerLogic : MonoBehaviour
{
    [Header("Stats")]
    public float Damage = 100f;
    public float TurnSpeed = 100f;
    public float Range = 3f;
    public float ProjectileSpeed = 2f;
    public float ReloadTime = 0.5f;
    public float ExplosionRadius = 0f;

    [Header("Unity setup")]
    public GameObject Turret;
    public GameObject ProjectilePrefab;
    public Transform ProjectileSpawnLocation;
    public GameObject CurrentProjectile;

    Animator _animator;
    AudioSource _audioSource;
    GameObject _closestEnemy;
    bool _loaded = true;
    bool _isAimed;
    float _lastTimeShot;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch *= Random.Range(0.8f, 1.2f);

        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _lastTimeShot = - 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy();

        if (HasEnemy())
            Aim();

        if (CanShoot())
            Shoot();

        if (CanReload())
            Reload();
    }

    bool CanReload()
    {
        return !_loaded && _lastTimeShot + ReloadTime / 2 < Time.time;
    }

    void Reload()
    {
        CurrentProjectile = Instantiate(ProjectilePrefab, ProjectileSpawnLocation);
        _loaded = true;
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
        return _loaded && HasEnemy() && _isAimed && _lastTimeShot + ReloadTime < Time.time;
    }

    void Shoot()
    {
        _audioSource.Play();

        _animator.SetTrigger("Shoot");

        _lastTimeShot = Time.time;
        _loaded = false;

        CurrentProjectile.transform.parent = null;

        Vector3 shootVelocity = Vector3.forward * ProjectileSpeed;
        CurrentProjectile.GetComponent<Rigidbody>().ResetInertiaTensor();
        CurrentProjectile.GetComponent<Rigidbody>().AddRelativeForce(shootVelocity, ForceMode.VelocityChange);

        CurrentProjectile.GetComponent<CannonBallLogic>()?.SetExplosionRadius(ExplosionRadius);

        CurrentProjectile.GetComponent<ProjectileLogic>()?.SetDamage(Damage);

        Destroy(CurrentProjectile, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Turret.transform.position, Range);
    }

}
