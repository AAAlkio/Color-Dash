using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;


    private SpriteRenderer spriteRenderer;
    //private Light2D light2d;
    private int currentColorIndex = 0;
    private int score = 0;
    private int highScore;
    private Material playerMaterial;


    public Color[] avaliableColors;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public ParticleSystem trailEffect;
    public ParticleSystem collectEffect;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //light2d = GetComponent<Light2D>();

        playerMaterial = spriteRenderer.material;
        spriteRenderer.color = avaliableColors[0];
        
        

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        scoreText.text = "Score: " + score;


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            currentColorIndex++;
            
            if (currentColorIndex >= avaliableColors.Length)
            {
                currentColorIndex = 0;
                
            }
            spriteRenderer.color = avaliableColors[currentColorIndex];
            playerMaterial.color = avaliableColors[currentColorIndex];
            //light2d.color = avaliableColors[currentColorIndex];

            var main = trailEffect.main;
            main.startColor = avaliableColors[currentColorIndex];
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            SpriteRenderer otherColor = collision.gameObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer.color != otherColor.color)
            {
                moveSpeed = 0f;
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("HighScore", highScore);
                   
                }

                MusicManager.Instance.PlayDeath();
                MusicManager.Instance.StopMusic();

                // Önce kamera sallasýn
                CameraShake.instance.Shake(0.4f, 0.6f);

                // Hemen durdurma, 0.4 saniye sonra paneli aç
                StartCoroutine(ShowGameOverAfterDelay(0.4f));
            }
            else
            {
                score++;
                scoreText.text = "Score: " + score;
                MusicManager.Instance.PlayCollect();

                CameraShake.instance.Shake(0.15f, 0.2f);

                collectEffect.transform.position = collision.transform.position;

                var main = collectEffect.main;
                main.startColor = avaliableColors[currentColorIndex];

                collectEffect.Play();

                Destroy(collision.gameObject);

            }
        }
        
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        // Kamera sallanmasý için bekleme
        yield return new WaitForSecondsRealtime(delay); // Gerçek zaman beklemesi


        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

    }
}
