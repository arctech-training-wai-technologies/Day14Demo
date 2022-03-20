using System.Diagnostics;
using Day14Demo.ArctechInfo.Controls;
using Day14Demo.ArctechInfo.Utilities;

namespace Day14Demo.ArctechInfo;

public class Window : Control
{
    public string Title { get; }

    private readonly ControlLinkedList _childControls;

    public Window(string title, int left, int top, int width, int height) : base(left, top, width, height)
    {
        Title = title;

        _childControls = new ControlLinkedList();
    }

    private bool _activeInput;

    public void StartInput()
    {
        _activeInput = true;
        _childControls.FocusFirst();

        while (_activeInput)
        {
            var keyInfo = _childControls.HandleConsoleInput();

            if (keyInfo.Key == ConsoleKey.Escape)
                return;

            HandleCommandKeys(keyInfo);
        }
    }

    public void Close()
    {
        Hide();
        _activeInput = false;
    }

    private bool HandleCommandKeys(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.Tab:
                if (keyInfo.ShiftKeyPressed())
                    _childControls.FocusPrevious();
                else
                    _childControls.FocusNext();
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.Enter:
                _childControls.FocusNext();
                break;
            case ConsoleKey.UpArrow:
                _childControls.FocusPrevious();
                break;
            default:
                return false;
        }

        return true;
    }

    public void AddControl(Control control)
    {
        control.AdjustPosition(Left, Top);

        _childControls.Add(control);
    }

    protected override void ShowBody()
    {
        var bottomLine = new string('-', Width);
        var topLine = $"= {Title} {new string('=', Width - Title.Length - 3)}";

        var rowLine = $"|{new string(' ', Width - 2)}|";

        Console.SetCursorPosition(Left, Top);
        Console.WriteLine(topLine);

        for (var row = 1; row < Height - 1; row++)
        {
            Console.SetCursorPosition(Left, Top + row);
            Console.WriteLine(rowLine);
        }

        Console.SetCursorPosition(Left, Top + Height - 1);
        Console.WriteLine(bottomLine);
    }

    public override void Show()
    {
        base.Show();

        _childControls.Show();
    }
}