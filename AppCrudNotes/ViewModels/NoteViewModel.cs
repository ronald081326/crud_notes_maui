using AppCrudNotes.DataAccess;
using AppCrudNotes.Dtos;
using AppCrudNotes.Models;
using AppCrudNotes.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppCrudNotes.ViewModels
{
    public partial class NoteViewModel : ObservableObject, IQueryAttributable
    {
        private readonly NoteDbContext _dbContext;

        [ObservableProperty]
        private NoteDto noteDto = new NoteDto();

        [ObservableProperty]
        private string pageTittle;

        private int idNote;

        [ObservableProperty]
        private bool isLoadingVisible = false;

        public NoteViewModel(NoteDbContext dbContext)
        {
            _dbContext = dbContext;
            NoteDto.CreationDate = DateTime.Now;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            idNote = id;

            if (idNote == 0)
            {
                PageTittle = "Nueva nota";
            }
            else
            {
                PageTittle = "Editar nota";
                IsLoadingVisible = true;

                await Task.Run(async () =>
                {
                    var noteFound = await _dbContext.Note.FirstAsync(e => e.id == idNote);

                    NoteDto.Id = noteFound.id;
                    NoteDto.Name = noteFound.name;
                    NoteDto.Description = noteFound.description;
                    NoteDto.CreationDate = noteFound.creationDate;


                    MainThread.BeginInvokeOnMainThread(() => { IsLoadingVisible = false; });
                });
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            IsLoadingVisible = true;
            NoteMessage message = new NoteMessage();


            await Task.Run(async () =>
            {
                if (idNote == 0)
                {
                    var tbNote = new Note
                    {
                        name = NoteDto.Name,
                        description = NoteDto.Description,
                        creationDate = NoteDto.CreationDate,
                    };

                    _dbContext.Note.Add(tbNote);
                    await _dbContext.SaveChangesAsync();

                    NoteDto.Id = tbNote.id;
                    message = new NoteMessage()
                    {
                        isCreate = true,
                        noteDto = NoteDto
                    };
                }
                else
                {
                    var noteFound = await _dbContext.Note.FirstAsync(e => e.id == idNote);
                    noteFound.name = NoteDto.Name;
                    noteFound.description = NoteDto.Description;
                    noteFound.creationDate = NoteDto.CreationDate;

                    await _dbContext.SaveChangesAsync();

                    message = new NoteMessage()
                    {
                        isCreate = false,
                        noteDto = NoteDto
                    };

                }

            });

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                IsLoadingVisible = false;
                WeakReferenceMessenger.Default.Send(new NoteMessaging(message));
                await Shell.Current.Navigation.PopAsync();
            });

        }

    }
}
