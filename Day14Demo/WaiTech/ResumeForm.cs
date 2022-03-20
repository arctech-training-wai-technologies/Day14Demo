using Day14Demo.ArctechInfo;
using Day14Demo.ArctechInfo.Controls;

namespace Day14Demo.WaiTech;

internal class ResumeForm : Window
{
    private readonly Label _labelFirstName, _labelLastName, _labelAge, _labelEducation;
    private readonly TextBox _textBoxFirstName, _textBoxLastName, _textBoxAge, _textBoxEducation;
    private readonly Button _buttonSave, _buttonCancel;

    public ResumeForm(string title, int left, int top) : 
        base(title, left, top, 100, 20)
    {
        _labelFirstName = new Label("First Name:", 2, 2);
        _textBoxFirstName = new TextBox(15, 2, 25);

        _labelLastName = new Label("Last Name:", 45, 2);
        _textBoxLastName = new TextBox(58, 2, 25);

        _labelAge = new Label("Age:", 2, 4);
        _textBoxAge = new TextBox(15, 4, 10);

        _labelEducation = new Label("Education:", 45, 4);
        _textBoxEducation = new TextBox(58, 4, 40);

        _buttonSave = new Button("(S)ave Resume", 's', 10, 6);
        _buttonCancel = new Button("(C)ancel", 's', 40, 6);

        InitializeControl();
    }

    private void InitializeControl()
    {
        AddControl(_labelFirstName);
        AddControl(_textBoxFirstName);

        AddControl(_labelLastName);
        AddControl(_textBoxLastName);

        AddControl(_labelAge);
        AddControl(_textBoxAge);

        AddControl(_labelEducation);
        AddControl(_textBoxEducation);

        AddControl(_buttonSave);
        AddControl(_buttonCancel);
    }
}