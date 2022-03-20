using Day14Demo.ArctechInfo;
using Day14Demo.ArctechInfo.Controls;

namespace Day14Demo.WaiTech;

internal class ResumeForm : Window
{
    private const string Heading = "FirstName,LastName,Age,Education\n";
    private const string FilePath = @"C:\test\WorldLineData.csv";

    private readonly Label _labelFirstName, _labelLastName, _labelAge, _labelEducation;
    private readonly TextBox _textBoxFirstName, _textBoxLastName, _textBoxAge, _textBoxEducation;
    private readonly Button _buttonSave, _buttonCancel;

    private readonly Label _labelStatus;

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

        _buttonSave = new Button("Save Resume", 75, 16);
        _buttonSave.OnClicked += ButtonSaveOnOnClicked;
        
        _buttonCancel = new Button("Cancel", 89, 16);
        _buttonCancel.OnClicked += ButtonCancelOnOnClicked;

        _labelStatus = new Label("Resume Form Initialized. Please enter your Resume.", 1, 18, 98);
        _labelStatus.SetColor(ConsoleColor.Black, ConsoleColor.Yellow);

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

        AddControl(_labelStatus);
    }

    private void ButtonCancelOnOnClicked(object? sender, EventArgs e)
    {
        Close();
    }

    private void ButtonSaveOnOnClicked(object? sender, EventArgs e)
    {
        var data = $"{_textBoxFirstName.Text},{_textBoxLastName.Text},{_textBoxAge.Text},{_textBoxEducation.Text}\n";

        if (!File.Exists(FilePath))
            File.WriteAllText(FilePath, Heading);

        try
        {
            File.AppendAllText(FilePath, data);
            _labelStatus.Text = $"File successfully saved at {FilePath}";
        }
        catch (Exception exception)
        {
            _labelStatus.Text = $"Error Saving File. {exception.Message}";
        }

        _labelStatus.Show();
    }
}