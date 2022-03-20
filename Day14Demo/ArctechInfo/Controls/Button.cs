using System.Diagnostics;

namespace Day14Demo.ArctechInfo.Controls;

public class Button : Control
{
    private static readonly ConsoleKey[] ExitKeys =
    {
        ConsoleKey.LeftArrow, ConsoleKey.RightArrow,
        ConsoleKey.UpArrow, ConsoleKey.DownArrow,
        ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Tab
    };

    public string Text { get; }
    public char Key { get; }

    public event EventHandler? OnClicked;

    public Button(string text, char key, int left, int top, int width = 0): 
        base(left, top, Math.Max(width, text.Length + 2))
    {
        Text = $"[{text}]";
        Left = left;
        Top = top;
        Key = key;

        ForeColor = ConsoleColor.White;
        BackColor = ConsoleColor.Cyan;
    }

    protected override void ShowBody()
    {
        Console.Write(Text);
    }

    public void Click()
    {
        BlinkButtonForVisualEffect();

        Show();

        OnClicked?.Invoke(this, EventArgs.Empty);
    }

    private void BlinkButtonForVisualEffect()
    {
        Console.SetCursorPosition(Left, Top);

        SetConsoleColor(ConsoleColor.Black, ConsoleColor.Black);
        Console.Write(Text);
        Thread.Sleep(100);

        Console.ResetColor();
    }

    public override ConsoleKeyInfo? SendKey(ConsoleKeyInfo keyInfo)
    {
        if(keyInfo.Key == ConsoleKey.Spacebar)
            Click();


    }

    //public static bool ProcessKey(IEnumerable<Button> buttons, ConsoleKeyInfo keyInfo)
    //{
    //    foreach (var button in buttons)
    //    {
    //        Debug.Assert(button != null, nameof(button) + " != null");

    //        if (button.Key == keyInfo.KeyChar)
    //        {
    //            button.Click();
    //            return true;
    //        }
    //    }

    //    return false;
    //}
}