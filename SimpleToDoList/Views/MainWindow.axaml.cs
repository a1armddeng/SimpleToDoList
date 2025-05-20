using Avalonia.Controls;
using SimpleToDoList.ViewModels;

namespace SimpleToDoList.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new MainWindowViewModel();
    }
}