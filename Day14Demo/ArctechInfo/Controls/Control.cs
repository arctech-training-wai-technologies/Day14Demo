using Day14Demo.ArctechInfo.Exception;
using Day14Demo.ArctechInfo.Utilities;

namespace Day14Demo.ArctechInfo.Controls;

public abstract class Control
{
    protected int Left, Top, Width, Height;
    protected ConsoleColor ForeColor, BackColor;
    public bool CanFocus { get; protected set; } = true;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left">Column number of the control's left edge</param>
    /// <param name="top">Column number of the control's top edge</param>
    /// <param name="width">0 - Auto Width</param>
    /// <param name="height">1 - Default Height</param>
    protected Control(int left, int top, int width = 0, int height = 1)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;

        ForeColor = ConsoleColor.Black;
        BackColor = ConsoleColor.White;
    }

    protected abstract void ShowBody();

    public void AdjustPosition(int left, int top)
    {
        Left += left;
        Top += top;
    }

    public void Focus()
    {
        if (!CanFocus)
            throw new ControlFocusNotAllowedException();

        Console.SetCursorPosition(Left, Top);
    }

    public virtual void Show()
    {
        Console.SetCursorPosition(Left, Top);
        SendColorToConsole();

        ShowBody();

        Console.ResetColor();
    }

    public void Hide()
    {
        Console.SetCursorPosition(Left, Top);
        SendColorToConsole(ConsoleColor.Black, ConsoleColor.Black);

        ShowBody();

        Console.ResetColor();
    }

    public virtual ConsoleKeyInfo HandleConsoleInput()
    {
        // Label does not handle keyboard input so ignore this method
        return ConsoleHelper.DefaultKeyInfo;
    }

    public void SetColor(ConsoleColor foreColor, ConsoleColor backColor)
    {
        ForeColor = foreColor;
        BackColor = backColor;
    }

    public static void SendColorToConsole(ConsoleColor foreColor, ConsoleColor backColor)
    {
        Console.ForegroundColor = foreColor;
        Console.BackgroundColor = backColor;
    }

    public void SendColorToConsole()
    {
        Console.ForegroundColor = ForeColor;
        Console.BackgroundColor = BackColor;
    }
}
