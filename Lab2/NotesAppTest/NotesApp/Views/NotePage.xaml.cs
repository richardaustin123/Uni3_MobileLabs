//using static Android.Content.ClipData;
namespace NotesApp.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    // NotePage()
    public NotePage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    // SaveButton_Clicked(object sender, EventArgs e)
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Notes.Models.Note note)
            File.WriteAllText(note.Filename, TextEditor.Text);
        await Shell.Current.GoToAsync("..");
    }

    // DeleteButton_Clicked(object sender, EventArgs e)
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Notes.Models.Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }
        await Shell.Current.GoToAsync("..");
    }

    // LoadNote(string fileName)
    private void LoadNote(string fileName)
    {
        Notes.Models.Note noteModel = new Notes.Models.Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

    // ItemId
    public string ItemId
    {
        set { LoadNote(value); }
    }

}
