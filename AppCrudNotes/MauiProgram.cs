using AppCrudNotes.DataAccess;
using AppCrudNotes.ViewModels;
using AppCrudNotes.Views;
using Microsoft.Extensions.Logging;

namespace AppCrudNotes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            SQLitePCL.Batteries.Init();

            /*var dbContext = new NoteDbContext();
          //  dbContext.Database.EnsureCreated();
            dbContext.Dispose();*/

            builder.Services.AddDbContext<NoteDbContext>();

            builder.Services.AddTransient<NotePage>();
            builder.Services.AddTransient<NoteViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));  

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
