using UnityEngine;

public class Health : MonoBehaviour
{
    public int Lives;
    public int GoldWorth;
    
    //TODO: awake kutsutakse valja aga wavestart event ei jõua siia
    // ja meetod ei käivitu
    private void Awake()
    {
        Events.OnWaveStart += WaveStart;

    }

    private void OnDestroy()
    {
        Events.OnWaveStart -= WaveStart;
    }

    private void WaveStart(WaveData data)
    {
        print("health wave data INFO: " + data);




    }

    public void InitializeEnemyInfo(EnemyData data) {


        GoldWorth = data.GoldWorth;
        Lives = data.Health;
        print("ENEMY INFO INITIALIZED GOLD" + GoldWorth);
    }

    public void ReduceHealth(int damage)
    {
        Lives -= damage;
        if (Lives <= 0)
        {
            Events.SetGold(Events.GetGold() + GoldWorth);
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
























