using NoteKeeper.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace NoteKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            TimePicker24.Time = DateTime.UtcNow.TimeOfDay;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            viewModel = new ItemDetailViewModel();
            BindingContext = viewModel;

            TimePicker24.Time = DateTime.UtcNow.TimeOfDay;
        }

        void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            Navigation.PopModalAsync();
            /*viewModel.NoteHeading = "XXXXXXX";

            DisplayAlert(
                "Cancel option", 
                "Cancel was selected " + viewModel.Note.Heading, 
                "Button 1", 
                "Button 2");*/
        }

        void Save_Clicked(object sender, EventArgs eventArgs)
        {
            var xxx = TimePicker24.Time;
            var date = StartDatePicker.Date;
            var dateTime = new DateTime(date.Year, date.Month, date.Day, xxx.Hours, xxx.Minutes, xxx.Seconds).ToString("yyyy-MM-dd hh:mm");

            MessagingCenter.Send(this, "SaveNote", viewModel.Note);

            Navigation.PopModalAsync();
            /*DisplayAlert(
                "Cancel option", 
                "Cancel was selected", 
                "Button 1", 
                "Button 2");*/
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteNote = await DisplayAlert(
                "Delete option",
                "Do you want to delete Note ?",
                "Yes",
                "No");

            if(deleteNote)
                 MessagingCenter.Send(this, "DeleteNote", viewModel.Note);

            await Navigation.PopModalAsync();
            /*DisplayAlert(
                "Cancel option", 
                "Cancel was selected", 
                "Button 1", 
                "Button 2");*/
        }
    }
}