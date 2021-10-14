///�ۼ��� 21.10.14
///�ۼ��� ������

public class Combo
{
    private int comboCount = 0;
    
    /// <summary>
    /// ComboCount �߰� 
    /// </summary>
    /// <param name="count"></param>

    public void AddCombo(int count) {
        comboCount += count;
    }
    
    /// <summary>
    /// Get ComboCount
    /// </summary>
    /// <returns></returns>
    public int GetCombo() {
        return comboCount;
    }
    /// <summary>
    /// ComboCount �ʱ�ȭ
    /// </summary>
    public void ResetCombo() {
        comboCount = 0;
    }

    /// <summary>
    /// Player�� Damage�� Combo�� ���� �ջ��� ����
    /// </summary>
    /// <returns></returns>
    public float GetComboDamage() {
        float damage = Player.instance.Damage;
        if (20 > comboCount) {
            //damage *= 1.0f;
        } else if(40 > comboCount) {
            damage *= 1.1f;
        } else if(60 > comboCount) {
            damage *= 1.2f;
        } else if(80 > comboCount) {
            damage *= 1.3f;
        } else if(100 > comboCount) {
            damage *= 1.4f;
        } else {
            damage *= 1.5f;
        }
        return damage;
    }
}
