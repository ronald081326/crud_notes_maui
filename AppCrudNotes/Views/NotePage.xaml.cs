using AppCrudNotes.ViewModels;

namespace AppCrudNotes.Views;

public partial class NotePage : ContentPage
{
	public NotePage(NoteViewModel noteViewModel)
	{
		InitializeComponent();
		BindingContext = noteViewModel;
	}
}