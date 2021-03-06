﻿public class BattleCommand
{
    public BattleCommandType commandType;

    public IndividualPokemon userPokemon;
    public IndividualPokemon targetPokemon;

    public string text = "";

    //UseMove information
    public int moveToUse = -1;

    //Factories/constructors
    private BattleCommand()
    {
        //Only let the factories work
    }

    public static BattleCommand CreateUseCommand(IndividualPokemon user, IndividualPokemon target, int moveToUse)
    {
        //Constructs a use move command.

        BattleCommand command = new BattleCommand();

        command.commandType = BattleCommandType.useMove;
        command.moveToUse = moveToUse;
        command.userPokemon = user;
        command.targetPokemon = target;

        MovedexEntry moveEntry = user.GetMove(moveToUse).entry;
        command.text = user.displayName + " used " + moveEntry.moveName + "!";

        return command;
    }

}

public enum BattleCommandType
{
    useMove
}