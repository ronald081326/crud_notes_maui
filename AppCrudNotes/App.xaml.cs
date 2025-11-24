using AppCrudNotes.DataAccess;

namespace AppCrudNotes
{
    public partial class App : Application
    {
        public App(NoteDbContext db)
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            MainPage = new AppShell();
        }
    }
}
