namespace Day14Demo.ArctechInfo.Controls;

public class Button : Control
{
    private const int TextOffset = 1;

    private static readonly ConsoleKey[] ExitKeys =
    {
        ConsoleKey.LeftArrow, ConsoleKey.RightArrow,
        ConsoleKey.UpArrow, ConsoleKey.DownArrow,
        ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Tab
    };

    public string Text { get; }

    public event EventHandler? OnClicked;

    public Button(string text, int left, int top, int width = 0): 
        base(left, top, Math.Max(width, text.Length + 2))
    {
        Text = $"[{text}]";
        Left = left;
        Top = top;

        ForeColor = ConsoleColor.Black;
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

        SendColorToConsole(ConsoleColor.Black, ConsoleColor.Black);
        Console.Write(Text);
        Thread.Sleep(100);

        Console.ResetColor();
    }

    public override ConsoleKeyInfo HandleConsoleInput()
    {
        SendColorToConsole();

        while (true)
        {
            Console.SetCursorPosition(Left + TextOffset, Top);
            var keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    Click();
                    break;
                default:
                {
                    if (ExitKeys.Contains(keyInfo.Key))
                    {
                        Console.ResetColor();
                        return keyInfo;
                    }
                    
                    Console.Beep();
                    break;
                }
            }
        }
    }
}