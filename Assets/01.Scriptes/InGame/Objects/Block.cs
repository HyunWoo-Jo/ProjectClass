///�ۼ��� 21.09.26
///�ۼ��� ������
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public BlockDirection direction;
    private Image _renderer;
    private void Awake() {
        _renderer = GetComponent<Image>();
    }
    public void SetColor(Color color) {
        _renderer.color = color;
    }
}
