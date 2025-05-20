using CommunityToolkit.Mvvm.ComponentModel;
using SimpleToDoList.Models;

namespace SimpleToDoList.ViewModels;

public partial class ToDoItemViewModel:ViewModelBase
{
    /// <summary>
    /// 创建一个新的空白构造函数
    /// </summary>
    public ToDoItemViewModel()
    {
         
    }

    /// <summary>
    /// 为给定的模型创建新的构造函数
    /// </summary>
    /// <param name="toDoItem">要加载的项目</param>
    public ToDoItemViewModel(ToDoItem toDoItem)
    {
        IsChecked = toDoItem.IsChecked;
        Content = toDoItem.Content;
    }
    
    /// <summary>
    /// 获取或设置每项的选中状态
    /// </summary>
    [ObservableProperty]
    private bool _isChecked;

    /// <summary>
    /// 获取或设置待办事项的内容
    /// </summary>
    [ObservableProperty]
    private string? _content;

    public ToDoItem GetToDoItem()
    {
        return new ToDoItem()
        {
            IsChecked = this.IsChecked,
            Content = this.Content
        };
    }        
}