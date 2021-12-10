///작성일 21.09.26
///작성자 조현우
using UnityEngine;
using UnityEngine.UI;


public enum Block_Animation_Trigger {
    ScaleUp,
    ScaleDown,
    Idle,
}
public class Block : MonoBehaviour
{
    public BlockDirection direction;
    private Image _renderer;

    public Animator anim;

    private void Awake() {
        _renderer = GetComponent<Image>();
    }
    public void SetColor(Color color) {
        _renderer.color = color;
    }

    public void Anim(Block_Animation_Trigger trigger) {
        anim.SetTrigger(trigger.ToString());
    }
}
