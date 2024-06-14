using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int health;
    private float invulnerabilityDuration = 18f; // Thời gian enemy không bị va chạm với đạn
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
        health = CONSTANT.ENEMY_MAX_HEALTH;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time > spawnTime + invulnerabilityDuration)
        {
            if (other.CompareTag("Bullet"))
            {
                health--;
                other.gameObject.SetActive(false);
                if (health <= 0)
                {
                    // Update score
                    TextManager.Instance.Score += 1;
                    TextManager.Instance.SetScoresText();

                    Destroy(gameObject);
                }
            }
        }
    }
}
