namespace Day14Demo.ArctechInfo.Utilities;

public static class Extensions
{
    //public static bool AltKeyPressed(this ConsoleKeyInfo keyInfo)
    //{
    //    return (keyInfo.Modifiers & ConsoleModifiers.Alt) != 0;
    //}

    public static bool ShiftKeyPressed(this ConsoleKeyInfo keyInfo)
    {
        return (keyInfo.Modifiers & ConsoleModifiers.Shift) != 0;
    }
}