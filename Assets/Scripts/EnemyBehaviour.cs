using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Data")]
    public float Speed;
    public int Reward;
    public int Damage;
    public float MaxHealth;

    [Header("Unity Setup")]
    public GameObject DeathParticles;
    public Color SlowDownColor;
    public Slider HealthSlider;

    int _waypointNumber = 0;
    float _slowdownConstant = 1f;
    List<Color> _defaultMaterialColors;
    MeshRenderer _mesh;
    Animator _animator;
    float _currentHealth;

    void Start()
    {
        _mesh = GetComponentInChildren<MeshRenderer>();
        _animator = GetComponentInChildren<Animator>();

        _currentHealth = MaxHealth;
        UpdateHealthUI();

        _defaultMaterialColors = new List<Color>();

        foreach (Material mat in _mesh.materials)
        {
            _defaultMaterialColors.Add(mat.color);
        }
    }

    public void SlowThisEnemy(float slowDown)
    {
        if (slowDown < _slowdownConstant)
            _slowdownConstant = slowDown;
    }

    void UpdateHealthUI()
    {
        HealthSlider.value = _currentHealth / MaxHealth;

        bool showSlider = HealthSlider.value != 1f;

        HealthSlider.gameObject.SetActive(showSlider);
    }

    void Update()
    {
        Move();
        SlowdownEffectBehaviour();
    }

    private void SlowdownEffectBehaviour()
    {
        ChangeColor();
        ApplySlowAnimation();
        ResetSlowdown();
    }

    private void ResetSlowdown()
    {
        _slowdownConstant = 1f;
    }

    void ApplySlowAnimation()
    {
        _animator.speed = _slowdownConstant;
    }

    void ChangeColor()
    {
        if (_slowdownConstant < 1f)
        {
            for (int i = 0; i < _mesh.materials.Length; i++)
            {
                _mesh.materials[i].color = _defaultMaterialColors[i] * SlowDownColor;
            }
        }
        else
        {
            for (int i = 0; i < _mesh.materials.Length; i++)
            {
                _mesh.materials[i].color = _defaultMaterialColors[i];
            }
        }
    }

    void Move()
    {
        float frameSpeed = _slowdownConstant * Speed * Time.deltaTime;

        Vector3 moveVector = PathwayLogic.Waypoints[_waypointNumber] - transform.position;
        float moveAmount = Mathf.Min(frameSpeed, moveVector.magnitude);
        Vector3 translateVector = moveVector.normalized * moveAmount;
        transform.Translate(translateVector, Space.World);



        if (moveVector.magnitude < frameSpeed)
        {
            _waypointNumber += 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            PlayerData.shared.HurtPlayer(Damage);
            Die();
        }
    }

    public void HurtEnemy(float damage)
    {
        _currentHealth -= damage;
        UpdateHealthUI();

        if (_currentHealth <= 0f)
            KilledByPlayer();
    }

    public void KilledByPlayer()
    {
        if (SpawningManager.shared.EnemiesAlive.Contains(gameObject))
        {
            PlayerData.shared.AddMoney(Reward);
            Die();
        }
    }

    public void Die()
    {
        SpawningManager.shared.EnemiesAlive.Remove(gameObject);

        GameObject deathEffect = Instantiate(DeathParticles, transform.position, Quaternion.identity, null);
        Destroy(deathEffect, 2f);
        Destroy(gameObject);
    }

}

