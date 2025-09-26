using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public PlayerController playerController;

    public float baseSpawn�nterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float diffulctySpeed = 0.05f;

    public float baseMoveSpeed = 2f;
    public float maxMoveSpeed = 15f;
    public float moveSpeedIncrease = 0.1f; // ne kadar yava� h�zlans�n

    private float nextSpawnTime;
    void Start()
    {
        
        nextSpawnTime = Time.time + baseSpawn�nterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Yeni obstacle olu�tur
            GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, transform.rotation);

            // Rastgele renk ata
            int randomColor�ndex = Random.Range(0, playerController.avaliableColors.Length);
            SpriteRenderer newObstacleRenderer = newObstacle.GetComponent<SpriteRenderer>();
            newObstacleRenderer.color = playerController.avaliableColors[randomColor�ndex];

            // Obstacle�a h�z ver
            ObstacleMovement obstacleMovement = newObstacle.GetComponent<ObstacleMovement>();
            obstacleMovement.speed = baseMoveSpeed;

            // Spawn interval�� s�reye g�re k���lt
            float elapsedInterval = Time.timeSinceLevelLoad;
            float newInterval = Mathf.Max(minSpawnInterval, baseSpawn�nterval - elapsedInterval * diffulctySpeed);
            nextSpawnTime = Time.time + newInterval;

            //h�z� zamanla artt�r.
            baseMoveSpeed = Mathf.Min(maxMoveSpeed, baseMoveSpeed + moveSpeedIncrease);
        }
    }
}
