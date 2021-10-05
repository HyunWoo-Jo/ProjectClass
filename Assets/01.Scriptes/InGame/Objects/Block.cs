///�ۼ��� 21.09.26
///�ۼ��� ������
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
