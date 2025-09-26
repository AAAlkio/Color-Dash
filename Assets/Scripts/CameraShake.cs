using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Transform camTransform;
    private Vector3 initalpos;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        camTransform = transform;
        initalpos = camTransform.localPosition;
    }

    public void Shake (float duraiton , float magnitude)
    {
        StartCoroutine(DoShake(duraiton, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range (-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camTransform.localPosition = new Vector3 (initalpos.x+x, initalpos.y+y, initalpos.z);

            elapsed += Time.unscaledDeltaTime;

            yield return null;

        }

        camTransform.localPosition = initalpos;

    }
}
