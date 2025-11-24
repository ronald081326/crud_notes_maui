using AppCrudNotes.DataAccess;
using AppCrudNotes.Dtos;
using AppCrudNotes.Utils;
using AppCrudNotes.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCrudNotes.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        private readonly NoteDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<NoteDto> noteList = new ObservableCollection<NoteDto>();


        public MainViewModel(NoteDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await GetNotes()));

            WeakReferenceMessenger.Default.Register<NoteMessaging>(this, (r,m) => {

                NoteMessageReceived(m.Value);
            });
        }


        public async Task GetNotes()
        {
            var notes = await _dbContext.Note.ToListAsync();

            foreach (var note in notes)
            {
                NoteList.Add(new NoteDto
                {
                    Id = note.id,
                    Name = note.name,
                    Description = note.description,
                    CreationDate = note.creationDate,

                });
            }
        }

        private void NoteMessageReceived(NoteMessage noteMessage)
        {

            var noteDto = noteMessage.noteDto;

            if (noteMessage.isCreate)
            {
                NoteList.Add(noteDto);
            }
            else
            {
                var noteFound = NoteList.First(e => e.Id == noteDto.Id);

                noteFound.Name = noteDto.Name;
                noteFound.Description = noteDto.Description;
                noteFound.CreationDate = noteDto.CreationDate;
            }

        }

        [RelayCommand]
        private async Task Create()
        {
            var uri = $"{nameof(NotePage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Edit(NoteDto noteDto)
        {
            var uri = $"{nameof(NotePage)}?id={noteDto.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Delete(NoteDto noteDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar esta nota ?", "Si", "No");

            if (answer)
            {
                var noteFound = await _dbContext.Note.FirstAsync(e => e.id == noteDto.Id);

                _dbContext.Note.Remove(noteFound);
                await _dbContext.SaveChangesAsync();
                NoteList.Remove(noteDto);

            }
        }

    }


}
