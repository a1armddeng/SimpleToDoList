using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleToDoList.Services;
using SimpleToDoList.ViewModels;
using SimpleToDoList.Views;

namespace SimpleToDoList;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private readonly MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel();

    private async Task InitMainWindowViewModelAsync()
    {
        var itemsLoaded = await ToDoListFileService.LoadFromFileAsync();
        if (itemsLoaded is not null)
        {
            foreach (var item in itemsLoaded)
            {
                _mainWindowViewModel.ToDoItems.Add(new ToDoItemViewModel(item));
            }
        }
    }
    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel
            };
            
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }
        
        base.OnFrameworkInitializationCompleted();
        
        await InitMainWindowViewModelAsync();
    }

    private bool _canClose; // 用于查看 window 是否允许关闭
    private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        e.Cancel = !_canClose; // 取消首次的关闭事件 
        if (!_canClose)
        {
            // 映射 ToDoItemViewModel 更适合 I/O 操作的 ToDoItem Model
            var itemsToSave = _mainWindowViewModel.ToDoItems.Select(item => item.GetToDoItem());
            await ToDoListFileService.SaveToFileAsync(itemsToSave);
            
            _canClose = true;
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
    }
}