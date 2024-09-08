using UnityEngine;

public class FadeDesires : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private float _fadeSpeed = 1.0f;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float newAlpha = Mathf.PingPong(Time.time * _fadeSpeed, 1.0f);

        if (_sprite.color.a != newAlpha)
        {
            Color newColor = _sprite.color;
            newColor.a = newAlpha;
            _sprite.color = newColor;
        }
    }
}
