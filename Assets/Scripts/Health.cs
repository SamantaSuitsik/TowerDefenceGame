using UnityEngine;

public class Health : MonoBehaviour
{
    public int lives = 3;
    public int goldWorth = 10;
    private void Update()
    {
        
    }

    public void ReduceHealth(int damage)
    {
        lives -= damage;
        if (lives <= 0)
        {
            Events.SetGold(Events.GetGold() + goldWorth);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Projectile projectile = other.GetComponent<Projectile>();
        // if (projectile != null)
        // {
        //     ReduceHealth(1);
        //     Destroy(projectile.gameObject);
        // }
    }
}
























