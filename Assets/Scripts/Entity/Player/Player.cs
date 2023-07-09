using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IStatProvider
{
    public static Player Instance;
    [SerializeField] private Transform _transform;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private Animator anim;
    private GameObject mobBlood;
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
    private float baseAttackSpeed;
    private ushort baseDamage;
    public class PlayerLevelStats
    {
        public int level;
        public int exp;
        public ushort damage;
        public float attackInterval;
        public PlayerLevelStats(int level, int exp, ushort damage, float attackInterval)
        {
            this.level = level;
            this.exp = exp;
            this.damage = damage;
            this.attackInterval = attackInterval;
        }
    }
    private PlayerLevelStats statsBeforeRun;

    private void Start()
    {
        OnMoveNext();
        baseDamage = damage;
        baseAttackSpeed = AttackInterval;
        mobBlood = Resources.Load<GameObject>("Blood");
        CurrentHealth = MaxHealth;
        _navigator = new PlayerNavigator(this);
        _navigator.Configure();
    }
    private void OnEnable()
    {
        LevelController.Instance.Runner.OnGameStart += OnGameStart;
        LevelController.Instance.Runner.OnLevelRun += OnLevelRun;
        LevelController.Instance.Runner.OnMoveNext += OnMoveNext;
    }
    private void OnDisable()
    {
        LevelController.Instance.Runner.OnLevelRun -= OnLevelRun;
        LevelController.Instance.Runner.OnGameStart -= OnGameStart;
        LevelController.Instance.Runner.OnMoveNext -= OnMoveNext;
    }
    private void OnMoveNext()
    {
        statsBeforeRun = new PlayerLevelStats(PlayerLevelController.CurrentLevel, PlayerLevelController.currentExp, damage, AttackInterval);
    }
    private void OnGameStart()
    {
        ResetPlayer();
        if (damage != 0 && baseAttackSpeed != 0)
        {
            damage = baseDamage;
            AttackInterval = baseAttackSpeed;
        }
        PlayerLevelController.ResetLevel();
    }
    private void OnLevelRun(bool running)
    {
        ResetPlayer();
        print("On leve run");
        if (!running)
        {
            damage = statsBeforeRun.damage;
            AttackInterval = statsBeforeRun.attackInterval;
            PlayerLevelController.FeedStats(statsBeforeRun);
        }
        else statsBeforeRun = new PlayerLevelStats(PlayerLevelController.CurrentLevel, PlayerLevelController.currentExp, damage, AttackInterval);
    }
    private void ResetPlayer()
    {
        SpeedAmplification = 0;
        DamageAmplification = 0;
        CurrentHealth = MaxHealth;
        

        _healthbar.UpdateHealthbar(CurrentHealth, MaxHealth);

        anim.SetInteger("moveX", 0);
    }
    public void Move(Vector2 targetPosition)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, Time.deltaTime * MovementSpeed * (1 + SpeedAmplification));
        float distX = GetPosition().x - targetPosition.x;
        anim.SetInteger("moveX", (int)distX);
        if (distX < 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (distX > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
    public Vector2 GetPosition() => _transform.position;
    public bool IsPlayerInPosition(Vector2 position) => Mathf.Approximately(_transform.position.x, position.x) && Mathf.Approximately(_transform.position.y, position.y);
    private void Update()
    {
        _navigator.Update();
    }
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetPosition(Vector2 position) => _transform.position = position;
    public void OnLevelUp()
    {
        AttackInterval -= 0.05f;
        damage += 1;
    }
    public void Damage(ushort damage)
    {
        if (CurrentHealth <= damage) Die();
        else CurrentHealth -= damage;
        _healthbar.UpdateHealthbar(CurrentHealth, MaxHealth);
        OnPlayerDamaged?.Invoke();
    }
    public void Heal(ushort heal)
    {
        CurrentHealth = (ushort)Mathf.Clamp(CurrentHealth + heal, 0, MaxHealth);
        _healthbar.UpdateHealthbar(CurrentHealth, MaxHealth);
    }
    public Animator GetAnimator() => anim;
    public GameObject GetBloodParticles() => mobBlood;
}