using UnityEngine;

public class LightAlphaChanger : MonoBehaviour
{
    private Light _light;
    private float intensity;
    private float timer;
    private void Awake()
    {
        _light = GetComponent<Light>();
        intensity = _light.intensity;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            _light.intensity = Random.Range(0, intensity);

            float timerMax = 1f;
            timer = timerMax;
        }
    }

}
