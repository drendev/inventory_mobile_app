namespace inventory_mobile_app.Pages;
using inventory_mobile_app.ViewModels;
using inventory_mobile_app.Models;
using System.ComponentModel;
using System.Diagnostics;

public partial class BottomNavBar : ContentView
{
    private readonly BottomNavBarViewModel bottomNavBarViewModel;

    public BottomNavBar()
    {
        InitializeComponent();
        bottomNavBarViewModel = new BottomNavBarViewModel();
        BindingContext = bottomNavBarViewModel;
    }

    private async void OnTabClicked(object sender, EventArgs e)
    {
        try
        {
            var tappedView = sender as View;
            var selectedTab = tappedView?.BindingContext as string ?? (tappedView as StackLayout)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter as string;

            if (!string.IsNullOrEmpty(selectedTab))
            {
                bottomNavBarViewModel.SelectedTab = selectedTab;
                Debug.WriteLine($"SelectedTab updated to: {bottomNavBarViewModel.SelectedTab}");

                await Shell.Current.GoToAsync($"//{selectedTab}Page");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}