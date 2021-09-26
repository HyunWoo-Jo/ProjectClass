using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockDirection direction;
    private SpriteRenderer _renderer;
    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void SetColor(Color color) {
        _renderer.color = color;
    }
}
