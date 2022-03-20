using Day14Demo.ArctechInfo.Controls;

namespace Day14Demo.ArctechInfo.Utilities;

public class ControlLinkedList
{
    private LinkedListNode<Control>? _selectedControlNode;

    private readonly LinkedList<Control> _childControls;

    public bool HasActiveControl => _selectedControlNode != null;

    public ControlLinkedList()
    {
        _childControls = new LinkedList<Control>();
    }

    public void FocusFirst()
    {
        SetFirstFocusNode();

        Focus();
    }

    public void FocusNext()
    {
        SetNextFocusNode();
        Focus();
    }

    public void FocusPrevious()
    {
        SetPreviousFocusNode();
        Focus();
    }

    private void Focus()
    {
        _selectedControlNode?.Value.Focus();
    }

    public ConsoleKeyInfo HandleConsoleInput()
    {
        return _selectedControlNode == null ? ConsoleHelper.DefaultKeyInfo : _selectedControlNode.Value.HandleConsoleInput();
    }

    public void Add(Control control)
    {
        _childControls.AddLast(control);
    }

    public void Show()
    {
        var node = _childControls.First;

        while (node != null)
        {
            node.Value.Show();

            node = node.Next;
        }
    }

    private void SetFirstFocusNode()
    {
        var node = _childControls.First;
        FindFirstAvailableControlToFocus(node);
    }

    private void SetNextFocusNode()
    {
        var node = _selectedControlNode?.Next ?? _childControls.First;

        FindFirstAvailableControlToFocus(node);
    }

    private void FindFirstAvailableControlToFocus(LinkedListNode<Control>? node)
    {
        do
        {
            if (node == null || node.Value.CanFocus)
                break;

            node = node.Next ?? _childControls.First;
        } while (true);

        _selectedControlNode = node;
    }

    private void SetPreviousFocusNode()
    {
        var node = _selectedControlNode?.Previous ?? _childControls.Last;

        do
        {
            if (node == null || node.Value.CanFocus)
                break;

            node = node.Previous ?? _childControls.Last;
        } while (true);

        _selectedControlNode = node;
    }
}