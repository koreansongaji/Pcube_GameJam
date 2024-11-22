public enum StatModType
{
    FLAT = 100,
    PERCENT_ADD = 200,
    PERCENT_MULTIPLY = 300,
}

public class StatModifier
{
    public readonly float value;
    public readonly StatModType type;
    public readonly int order;
    public readonly object source;

    public StatModifier(float value, StatModType type, int order, object source = null)
    {
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}