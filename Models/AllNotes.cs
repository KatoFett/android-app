using System.Collections.ObjectModel;

namespace ScriptureJournal.Models;

internal class AllScriptureJournal
{
    public ObservableCollection<Note> ScriptureJournal { get; set; } = new ObservableCollection<Note>();

    public AllScriptureJournal() =>
        LoadScriptureJournal();

    public void LoadScriptureJournal()
    {
        ScriptureJournal.Clear();

        // Get the folder where the scripturejournal are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.scripturejournal.txt files.
        IEnumerable<Note> scripturejournal = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.scripturejournal.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new Note()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetCreationTime(filename)
                                    })

                                    // With the final collection of scripturejournal, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (Note note in scripturejournal)
            ScriptureJournal.Add(note);
    }
}