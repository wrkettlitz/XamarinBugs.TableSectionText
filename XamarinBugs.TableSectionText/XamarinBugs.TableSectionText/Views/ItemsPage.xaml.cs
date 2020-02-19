using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinBugs.TableSectionText.Models;
using XamarinBugs.TableSectionText.Views;
using XamarinBugs.TableSectionText.ViewModels;

namespace XamarinBugs.TableSectionText.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        public int MaxLenght { get; } = 300;
        private bool IsNotifyingFocusLoss { get; set; }
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }


        

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void EditorTextChangedLabel(object sender, TextChangedEventArgs e)
        {
            var currentLength = e.NewTextValue?.Length ?? 0;
            LabelExample.Text = $"This is a Label. ({currentLength}/{MaxLenght})";
        }
        private void EditorTextChangedTableSection(object sender, TextChangedEventArgs e)
        {
            var currentLength = e.NewTextValue?.Length ?? 0;
            TableSectionExample.Title = $"This is a TableSection. ({currentLength}/{MaxLenght})";
        }

        private async void EditorUnfocused(object sender, FocusEventArgs e)
        {
            if (IsNotifyingFocusLoss) return;
            try
            {
                IsNotifyingFocusLoss = true;
                await DisplayAlert("Focus loss", "Focus loss", "Ok");
            }
            finally
            {
                IsNotifyingFocusLoss = false;
            }
        }
        
    }
}