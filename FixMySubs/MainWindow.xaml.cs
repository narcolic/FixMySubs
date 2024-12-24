using FixMySubs.Helper;
using FixMySubs.Pages;
using Microsoft.UI.Windowing;
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
        ExtendsContentIntoTitleBar = true;
        Title = AppTitleText;
        SetTitleBar(this.AppTitleBar);

        AppWindow appWindow = WindowHelper.GetAppWindow(this);
        appWindow.SetIcon("Assets/Tiles/GalleryIcon.ico");
        appWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems.OfType<NavigationViewItem>().First();
    }

    private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            contentFrame.Navigate(typeof(SettingsPage));
        }
        else
        {
            contentFrame.Navigate(Type.GetType("FixMySubs.Pages." + ((string)((NavigationViewItem)args.SelectedItem).Tag)));
        }
    }

    public string AppTitleText => "Rename Subs";
}