using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); 
    }
}
