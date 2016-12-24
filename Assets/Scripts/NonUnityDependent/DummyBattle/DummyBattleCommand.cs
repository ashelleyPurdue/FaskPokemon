public class DummyBattleCommand
{
    public DummyBattleCommandType commandType;

    public DummyPokemon userPokemon;
    public DummyPokemon targetPokemon;

    public string text = "";

    //UseMove information
    public int moveToUse = -1;

    //Factories/constructors
    private DummyBattleCommand()
    {
        //Only let the factories work
    }

    public static DummyBattleCommand CreateUseCommand(DummyPokemon user, DummyPokemon target, int moveToUse)
    {
        //Constructs a use move command.

        DummyBattleCommand command = new DummyBattleCommand();

        command.commandType = DummyBattleCommandType.useMove;
        command.moveToUse = moveToUse;
        command.userPokemon = user;
        command.targetPokemon = target;
        command.text = "used " + moveToUse + "!";

        return command;
    }


    //Interface

    public void StartAnimation()
    {
        //TODO: Start the move's animation
    }

    public float GetAnimationTime()
    {
        //Gets the length of the animation
        //TODO: Change the length based on the move used

        return 0.5f;
    }
}

public enum DummyBattleCommandType
{
    useMove
}