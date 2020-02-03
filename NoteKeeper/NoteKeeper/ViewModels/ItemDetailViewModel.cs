using System;
using System.Collections.Generic;
using System.Linq;
using NoteKeeper.Models;

namespace NoteKeeper.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Note Note { get; set; }
        public IList<string> CourseList { get; set; }

        public string NoteHeading
        {
            get { return Note.Heading; }
            set
            {
                Note.Heading = value;
                OnPropertyChanged();
            }
        }

        public ItemDetailViewModel(Note note = null)
        {
            Title = "Edit note";
            InitializeCourseList();
            Note = note ?? new Note();
        }

        async void InitializeCourseList()
        {
            CourseList = await PluralsightDataStore.GetCoursesAsync();
        }
    }
}
