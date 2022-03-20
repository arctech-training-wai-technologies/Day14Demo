using Day14Demo.ArctechInfo.Controls;

namespace Day14Demo.ArctechInfo;

public interface IWindow
{
    public List<Control> ChildControls { get; }
    void Show();
}