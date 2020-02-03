using NoteKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public AddItemPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            TimePicker24.Time = DateTime.UtcNow.TimeOfDay;
        }

        public AddItemPage()
        {
            InitializeComponent();

            viewModel = new ItemDetailViewModel();
            BindingContext = viewModel;

            TimePicker24.Time = DateTime.UtcNow.TimeOfDay;
        }

        void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            Navigation.PopModalAsync();
        }

        void Save_Clicked(object sender, EventArgs eventArgs)
        {
            var xxx = TimePicker24.Time;
            var date = StartDatePicker.Date;
            var dateTime = new DateTime(date.Year, date.Month, date.Day, xxx.Hours, xxx.Minutes, xxx.Seconds).ToString("yyyy-MM-dd hh:mm");

            MessagingCenter.Send(this, "SaveNote", viewModel.Note);

            Navigation.PopModalAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteNote = await DisplayAlert(
                "Delete option",
                "Do you want to delete Note ?",
                "Yes",
                "No");

            if (deleteNote)
                MessagingCenter.Send(this, "DeleteNote", viewModel.Note);

            await Navigation.PopModalAsync();
        }
    }
}