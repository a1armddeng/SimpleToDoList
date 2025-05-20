using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SimpleToDoList.ViewModels;

/// <summary>
/// MainViewModel 定义了 View 和 ToDoItems 之间的交互逻辑
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        if (Design.IsDesignMode)
        {
            ToDoItems = new ObservableCollection<ToDoItemViewModel>(new[]
            {
                new ToDoItemViewModel(){ Content = "Hello, Avalonia!",IsChecked = true},
                new ToDoItemViewModel(){ Content = "Finish Avalonia ToDo-List Application" }
            });
        }
    }

    /// <summary>
    /// 获取一个可以添加或移除 ToDoItem 的集合
    /// </summary>
    public ObservableCollection<ToDoItemViewModel> ToDoItems { get; } = new ObservableCollection<ToDoItemViewModel>();

    /// <summary>
    /// 这个命令用于添加一个新的 ToDoItem 到 ToDoItems
    /// </summary>
    [RelayCommand (CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        // 添加一个新的 ToDoItem 到 ToDoItems 
        ToDoItems.Add(new ToDoItemViewModel(){Content = NewItemContent});
        
        // 重置 NewItemContent
        NewItemContent = null;
    }

    /// <summary>
    /// 从 ToDoItems 中移除给定的 ToDoItem
    /// </summary>
    [RelayCommand]
    private void RemoveItem(ToDoItemViewModel item)
    {
        ToDoItems.Remove(item);
    }
    
    /// <summary>
    /// 获取或设置新添加的 ToDoItem 的内容，如果字符串不为空，则将启用名为 AddItemCommand 的命令
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))]
    private string? _newItemContent;
    
    /// <summary>
    /// 如果可以添加新 item 则返回 true
    /// </summary>
    private bool CanAddItem() => !string.IsNullOrEmpty(NewItemContent);
}