namespace Day14Demo.ArctechInfo.Controls;

public class TextBox : Control
{
    private const string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-=[];',.\"!@#$%^&*()_+{}:<>? ";
    
    private static readonly ConsoleKey[] ExitKeys =
    {
        ConsoleKey.UpArrow, ConsoleKey.DownArrow,
        ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Tab
    };

    private readonly char[] _characters;
    public string Text => new(_characters);

    public TextBox(int left, int top, int width) : 
        base(left, top, width)
    {
        _characters = new char[width];

        for (var i = 0; i < _characters.Length; i++)
        {
            _characters[i] = ' ';
        }

        ForeColor = ConsoleColor.White;
        BackColor = ConsoleColor.Magenta;
    }

    protected override void ShowBody()
    {
        Console.Write(Text);
    }

    public override ConsoleKeyInfo? SendKey(ConsoleKeyInfo keyInfo)
    {
        SetConsoleColor();

        TextBoxCursor textBoxCursor = new();

        while (true)
        {
            if (textBoxCursor < Width && ValidChars.Contains(keyInfo.KeyChar))
            {
                _characters[textBoxCursor++] = keyInfo.KeyChar;
                Console.Write(keyInfo.KeyChar);
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                textBoxCursor.MoveLeft();
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                textBoxCursor.MoveRight();
            }
            else if (textBoxCursor > 0 && keyInfo.Key == ConsoleKey.Backspace)
            {
                _characters[--textBoxCursor] = ' ';
                Console.Write("\b ");
            }
            else if (ExitKeys.Contains(keyInfo.Key))
                break;
            else
                Console.Beep();

            Console.SetCursorPosition(Left + textBoxCursor, Top);
            keyInfo = Console.ReadKey(true);
        }
        
        Console.ResetColor();

        return keyInfo;
    }

    public class TextBoxCursor
    {
        private int _cursorPosition;
        private int _inputTextLength;

        public static implicit operator int(TextBoxCursor textBoxCursor) =>
            textBoxCursor._cursorPosition;

        public static TextBoxCursor operator ++(TextBoxCursor textBoxCursor)
        {
            textBoxCursor._inputTextLength = ++textBoxCursor._cursorPosition;
            return textBoxCursor;
        }

        public static TextBoxCursor operator --(TextBoxCursor textBoxCursor)
        {
            textBoxCursor._inputTextLength = --textBoxCursor._cursorPosition;
            return textBoxCursor;
        }

        public void MoveLeft()
        {
            if (_cursorPosition == 0)
            {
                Console.Beep();
                return;
            }

            --_cursorPosition;
        }

        public void MoveRight()
        {
            if (_cursorPosition == _inputTextLength)
            {
                Console.Beep();
                return;
            }

            ++_cursorPosition;
        }
    }
}