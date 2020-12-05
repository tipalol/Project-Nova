using UnityEngine;

public class Destroying : MonoBehaviour
{
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var oldColor = _renderer.color;
        _renderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - 0.0015f);
    }
}
