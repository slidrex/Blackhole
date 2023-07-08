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

    public ushort MaxHealth { get; set; } = 100;

    public float AttackDistance => 1.5f;

    public float AttackInterval { get; set; } = 1.0f;

    public float MovementSpeed => 2;

    public ushort CurrentHealth { get; private set; }
    public ushort damage = 1;

    private void Awake() => Instance = this;
    private void Start()
    {
        CurrentHealth = MaxHealth;
        _navigator = new PlayerNavigator(this);
        _navigator.Configure();
    }
    public void Move(Vector2 targetPosition) => _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, Time.deltaTime * MovementSpeed);
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
        if (CurrentHealth < damage) Die();
        else CurrentHealth -= damage;
        _healthbar.UpdateHealthbar(CurrentHealth, MaxHealth);
    }
}