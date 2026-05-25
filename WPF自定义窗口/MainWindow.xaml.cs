using System.Windows;
using System.Windows.Input;

namespace WpfCustomButtons;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Maximize_Click(object sender, RoutedEventArgs e)
    {
        ToggleMaximizeRestore();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            ToggleMaximizeRestore();
        }
        else
        {
            DragMove();
        }
    }

    private void Window_StateChanged(object sender, System.EventArgs e)
    {
        UpdateMaximizeIcon();

        if (WindowState == WindowState.Maximized)
        {
            MainBorder.CornerRadius = new CornerRadius(0);
            MainBorder.Margin = new Thickness(8);
        }
        else
        {
            MainBorder.CornerRadius = new CornerRadius(8);
            MainBorder.Margin = new Thickness(0);
        }
    }

    private void ToggleMaximizeRestore()
    {
        WindowState = WindowState == WindowState.Maximized
            ? WindowState.Normal
            : WindowState.Maximized;
    }

    private void UpdateMaximizeIcon()
    {
        bool isMaximized = WindowState == WindowState.Maximized;
        MaximizeIcon.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;
        RestoreIconBack.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
        RestoreIconFront.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
    }
}
