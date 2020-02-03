﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using NoteKeeper.Models;
using NoteKeeper.Views;

namespace NoteKeeper.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Notes = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "SaveNote", async (sender, note) =>
            {
                Notes.Add(note);
                if(note.Id != null)
                {
                    await PluralsightDataStore.UpdateNoteAsync(note);
                }
                else
                {
                    await PluralsightDataStore.AddNoteAsync(note);
                }

                LoadItemsCommand.Execute(null);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "DeleteNote", async (sender, note) =>
            {
                await PluralsightDataStore.DeleteNoteAsync(note);

                LoadItemsCommand.Execute(null);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Notes.Clear();
                var notes = await PluralsightDataStore.GetNotesAsync();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}