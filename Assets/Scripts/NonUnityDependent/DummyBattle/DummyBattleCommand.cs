public class DummyBattleCommand
{
    public DummyBattleCommandType commandType;

    public DummyPokemon userPokemon;
    public DummyPokemon targetPokemon;

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

        return command;
    }

}

public enum DummyBattleCommandType
{
    useMove
}