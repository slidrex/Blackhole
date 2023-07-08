using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IStatProvider
{
    public static Player Instance;
    [SerializeField] private Transform _transform;
    private PlayerNavigator _navigator;

    public ushort Health => throw new System.NotImplementedException();

    public float AttackDistance => 1.5f;

    public float AttackSpeed => throw new System.NotImplementedException();

    public float MovementSpeed => 2;

    private void Awake() => Instance = this;
    private void Start()
    {
        _navigator = new PlayerNavigator(this);
        _navigator.Configure();
    }
    public void Move(Vector2 targetPosition)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, Time.deltaTime * MovementSpeed);
    }
    public Vector2 GetPosition() => _transform.position;
    public bool IsPlayerInPosition(Vector2 position)
    {
        return Mathf.Approximately(_transform.position.x, position.x) && Mathf.Approximately(_transform.position.y, position.y);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            LevelController.Instance.Runner.RunLevel();
        }
        _navigator.Update();
    }
}