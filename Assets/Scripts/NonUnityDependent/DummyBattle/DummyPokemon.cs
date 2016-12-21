using System;

public class DummyPokemon
{
    public int maxHealth = 30;
    public int currentHealth;

    public StatusCondition statusCondition = StatusCondition.none;

    public DummyPokemon()
    {
        currentHealth = maxHealth;
    }

    public void Tackle(DummyPokemon other)
    {
        //Tackles the other pokemon

        //Deal a random amount of damage
        Random randGen = new Random();
        int damage = randGen.Next(6, 10);

        other.currentHealth -= damage;
    }

    public void PoisonSting(DummyPokemon other)
    {
        //Poisons the target if it doesn't already have a status condition

        if (other.statusCondition != StatusCondition.none)
        {
            return;
        }

        other.statusCondition = StatusCondition.poisoned;
    }
}
