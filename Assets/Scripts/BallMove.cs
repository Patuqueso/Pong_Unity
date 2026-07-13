using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocityBeforeCollision;

    public float speed = 5f;

    public static event System.Action<Player> OnGoalScored;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void ResetBall()
    {
        rb.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        float xDirection = Random.Range(0, 2) == 0 ? -1f : 1f;
        float yDirection = Random.Range(-1f, 1f);

        Vector2 direction = new Vector2(xDirection, yDirection).normalized;

        rb.linearVelocity = direction * speed;
    }

    private void FixedUpdate()
    {
        // Save the incoming velocity before Unity resolves a collision.
        velocityBeforeCollision = rb.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            float offset = (rb.position.y - other.transform.position.y) / other.collider.bounds.size.y;

            Vector2 dir = new Vector2(
            Mathf.Sign(velocityBeforeCollision.x), // reverse X
            offset
            ).normalized;

            rb.linearVelocity = dir * speed;

            dir.x *= -1f;

            dir.y = Mathf.Sign(dir.y) *
                    Mathf.Max(Mathf.Abs(dir.y), 0.3f);

            rb.linearVelocity = dir.normalized * speed;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = other.GetContact(0).normal;

            Vector2 reflectedDirection =
                Vector2.Reflect(velocityBeforeCollision.normalized, normal);

            rb.linearVelocity = reflectedDirection * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal_1"))
        {
            OnGoalScored?.Invoke(Player.Left);
            ResetBall();
        }
        else
        {
            OnGoalScored?.Invoke(Player.Right);
            ResetBall();
        }
    }


}