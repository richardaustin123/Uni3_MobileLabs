using System.Collections.ObjectModel;
namespace Notes.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        //Get the folder where the notes are stored
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files
        IEnumerable<Note> notes = Directory

            // Select the file names from the directory
            .EnumerateFiles(appDataPath, "*.notes.txt")

            // Each file name is used to create a new note
            .Select(filename => new Note()
            {
                Filename = filename,
                Text = File.ReadAllText(filename),
                Date = File.GetCreationTime(filename)
            })

            // With the final collectionof notes, order them by date
            .OrderBy(Note => Note.Date);

        // Add each note into the ObservableCollection
        foreach (Note note in notes)
        {
            Notes.Add(note);
        }
    }
}