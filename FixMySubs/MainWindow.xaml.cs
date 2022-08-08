using FixMySubs.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace FixMySubs;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        navMenu.SelectedItem = navMenu.MenuItems.OfType<NavigationViewItem>().First();
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            sender.Header = "Settings";
            contentFrame.Navigate(typeof(SettingsPage));
        }
        else
        {
            sender.Header = "Home";
            contentFrame.Navigate(Type.GetType("FixMySubs.Pages." + ((string)((NavigationViewItem)args.SelectedItem).Tag)));
        }
    }
}