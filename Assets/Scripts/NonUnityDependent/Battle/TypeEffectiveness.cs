public static class TypeEffectiveness
{
    public static float NO_EFFECT = 0;
    public static float NOT_VERY_EFFECTIVE = 0.5f;
    public static float NORMAL_EFFECTIVE = 1;
    public static float SUPER_EFFECTIVE = 2;

    public static float GetEffectiveness(PokemonType moveType, PokemonType targetTypeA, PokemonType targetTypeB = PokemonType.none)
    {
        //TODO: Use a lookup table to find the effectiveness
        return NORMAL_EFFECTIVE;
    }
}
