using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IStatProvider
{
    public static Player Instance;
    [SerializeField] private Transform _transform;
    [SerializeField] private Healthbar _healthbar;
    public PlayerLevelController PlayerLevelController;
    private PlayerNavigator _navigator;
    public Action OnPlayerDamaged;
    public ushort MaxHealth { get; set; } = 50;

    public float AttackDistance => 1.5f;

    public float AttackInterval { get; set; } = 1.0f;

    public float MovementSpeed => 3;

    public ushort CurrentHealth { get; private set; }
    public ushort damage = 1;
    public float DamageAmplification;
    public float SpeedAmplification;

    private void Awake() => Instance = this;
    private void Start()
    {
        CurrentHealth = MaxHealth;
        _navigator = new PlayerNavigator(this);
        _navigator.Configure();
    }
    public void Move(Vector2 targetPosition) => _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, Time.deltaTime * MovementSpeed * (1 + SpeedAmplification));
    public Vector2 GetPosition() => _transform.position;
    public bool IsPlayerInPosition(Vector2 position) => Mathf.Approximately(_transform.position.x, position.x) && Mathf.Approximately(_transform.position.y, position.y);
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            LevelController.Instance.Runner.RunLevel();
        }
        _navigator.Update();
    }
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnLevelUp()
    {
        AttackInterval -= 0.05f;
        damage += 1;
    }
    public void Damage(ushort damage)
    {
        damage = (ushort)(damage * (1 + DamageAmplification));
        if (CurrentHealth < damage) Die();
        else CurrentHealth -= damage;
        _healthbar.UpdateHealthbar(CurrentHealth, MaxHealth);
        OnPlayerDamaged.Invoke();
    }
    public void Heal(ushort heal)
    {
        CurrentHealth = (ushort)Mathf.Clamp(CurrentHealth + heal, 0, MaxHealth);
        _healthbar.UpdateHealthbar(CurrentHealth, MaxHealth);
    }
}