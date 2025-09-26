using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public PlayerController playerController;

    public float baseSpawnÝnterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float diffulctySpeed = 0.05f;

    public float baseMoveSpeed = 2f;
    public float maxMoveSpeed = 15f;
    public float moveSpeedIncrease = 0.1f; // ne kadar yavaþ hýzlansýn

    private float nextSpawnTime;
    void Start()
    {
        
        nextSpawnTime = Time.time + baseSpawnÝnterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Yeni obstacle oluþtur
            GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, transform.rotation);

            // Rastgele renk ata
            int randomColorÝndex = Random.Range(0, playerController.avaliableColors.Length);
            SpriteRenderer newObstacleRenderer = newObstacle.GetComponent<SpriteRenderer>();
            newObstacleRenderer.color = playerController.avaliableColors[randomColorÝndex];

            // Obstacle’a hýz ver
            ObstacleMovement obstacleMovement = newObstacle.GetComponent<ObstacleMovement>();
            obstacleMovement.speed = baseMoveSpeed;

            // Spawn interval’ý süreye göre küçült
            float elapsedInterval = Time.timeSinceLevelLoad;
            float newInterval = Mathf.Max(minSpawnInterval, baseSpawnÝnterval - elapsedInterval * diffulctySpeed);
            nextSpawnTime = Time.time + newInterval;

            //hýzý zamanla arttýr.
            baseMoveSpeed = Mathf.Min(maxMoveSpeed, baseMoveSpeed + moveSpeedIncrease);
        }
    }
}
