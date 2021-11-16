
public class Ability 
{
    public string name;
    public float chance;
    public float increasedAmount;
    public AbilityType type;

    public Ability(string name, float chance, float increasedAmount, AbilityType type) {
        this.name = name;
        this.chance = chance;
        this.increasedAmount = increasedAmount;
        this.type = type;
    }
}
