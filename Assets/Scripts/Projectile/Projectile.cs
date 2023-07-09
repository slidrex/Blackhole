using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ushort damage;
    [SerializeField] private Rigidbody2D rb;
    public void Direct(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerTransform>() != null)
        {
            Player.Instance.Damage(damage);
            Destroy(gameObject);
        }
    }
}
