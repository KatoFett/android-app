namespace ScriptureJournal.Views;

public partial class AllScriptureJournalPage : ContentPage
{
    public AllScriptureJournalPage()
    {
        InitializeComponent();

        BindingContext = new Models.AllScriptureJournal();
    }

    protected override void OnAppearing()
    {
        ((Models.AllScriptureJournal)BindingContext).LoadScriptureJournal();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePage));
    }

    private async void scripturejournalCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.Note)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.scripturejournal.txt"
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");

            // Unselect the UI
            scripturejournalCollection.SelectedItem = null;
        }
    }
}