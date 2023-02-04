public struct SparseSequence
{
    public float Value;

    private const float sqrt5 = 2.23606797749978969640917366873127623f;
    private const float φ = (1 + sqrt5) / 2;

    public float Current => Value;

    public SparseSequence(float seed)
    {
        Value = seed;
    }

    public float Next()
    {
        Value += φ;
        Value %= 1.0f;
        return Value;
    }
}
