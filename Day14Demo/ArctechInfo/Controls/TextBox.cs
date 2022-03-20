namespace Day14Demo.ArctechInfo.Controls;

public class TextBox : Control
{
    private const string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-=[];',.\"!@#$%^&*()_+{}:<>? ";

    private static readonly ConsoleKey[] ExitKeys =
    {
        ConsoleKey.UpArrow, ConsoleKey.DownArrow,
        ConsoleKey.Enter, ConsoleKey.Escape, ConsoleKey.Tab
    };

    public string Text { get; set; }

    public TextBox(int left, int top, int width) :
        base(left, top, width)
    {
        ForeColor = ConsoleColor.White;
        BackColor = ConsoleColor.Magenta;
    }

    protected override void ShowBody()
    {
        Console.Write(DisplayText);
    }

    private string DisplayText
    {
        get
        {
            var text = Text;

            if (string.IsNullOrEmpty(text))
                text = "";
            else if (text.Length > Width)
                text = text[..Width];

            return text.PadRight(Width);
        }
    }

    public override ConsoleKeyInfo HandleConsoleInput()
    {
        SendColorToConsole();

        TextBoxCursor textBoxCursor = new();

        var characters = DisplayText.ToCharArray();

        while (true)
        {
            Console.SetCursorPosition(Left + textBoxCursor, Top);
            var keyInfo = Console.ReadKey(true);

            if (textBoxCursor < Width && ValidChars.Contains(keyInfo.KeyChar))
            {
                characters[textBoxCursor++] = keyInfo.KeyChar;
                Console.Write(keyInfo.KeyChar);
            }
            else switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        textBoxCursor.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        textBoxCursor.MoveRight();
                        break;
                    default:
                        {
                            if (textBoxCursor > 0 && keyInfo.Key == ConsoleKey.Backspace)
                            {
                                characters[--textBoxCursor] = ' ';
                                Console.Write("\b ");
                            }
                            else if (ExitKeys.Contains(keyInfo.Key))
                            {
                                Text = new string(characters).TrimEnd();
                                Console.ResetColor();
                                return keyInfo;
                            }
                            else
                                Console.Beep();

                            break;
                        }
                }
        }
    }

    public class TextBoxCursor
    {
        private int _cursorPosition;
        private int _inputTextLength;

        public static implicit operator int(TextBoxCursor textBoxCursor) =>
            textBoxCursor._cursorPosition;

        public static TextBoxCursor operator ++(TextBoxCursor textBoxCursor)
        {
            var newTextBoxCursor = new TextBoxCursor
            {
                _cursorPosition = textBoxCursor._cursorPosition + 1,
                _inputTextLength = textBoxCursor._inputTextLength
            };

            return newTextBoxCursor;
        }

        public static TextBoxCursor operator --(TextBoxCursor textBoxCursor)
        {
            var newTextBoxCursor = new TextBoxCursor
            {
                _cursorPosition = textBoxCursor._cursorPosition - 1,
                _inputTextLength = textBoxCursor._inputTextLength
            };

            return newTextBoxCursor;
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