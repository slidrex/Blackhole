using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonBullet : MonoBehaviour
{
    [SerializeField] private ushort damage;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Destroy(gameObject, 10.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IStatProvider>(out var stat))
        {
            stat.Damage(damage);
            Destroy(gameObject);
        }
    }
}
