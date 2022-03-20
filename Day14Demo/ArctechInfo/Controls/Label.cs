namespace Day14Demo.ArctechInfo.Controls;

public class Label : Control
{
    public string Text { get; set; }

    public Label(string text, int left, int top, int width = 0) : 
        base(left, top, width)
    {
        Text = text;
        CanFocus = false;
    }

    protected override void ShowBody()
    {
        Console.Write(Text);
    }
}