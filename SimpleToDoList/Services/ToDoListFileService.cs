using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleToDoList.Models;

namespace SimpleToDoList.Services;

public static class ToDoListFileService
{
    private static string _jsonFileName = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Avalonia.SimpleToDoList","MyToDoList.txt");
    
    public static async Task SaveToFileAsync(IEnumerable<ToDoItem> itemsToSave)
    {
        // 确保所有目录存在
        Directory.CreateDirectory(Path.GetDirectoryName(_jsonFileName)!);
        
        // 使用 FileStream 写入所有 items
        using (var fs = File.Create(_jsonFileName))
        {
            await JsonSerializer.SerializeAsync(fs,itemsToSave);
        }
    }

    public static async Task<IEnumerable<ToDoItem>?> LoadFromFileAsync()
    {
        try
        {
            using (var fs = File.OpenRead(_jsonFileName))
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<ToDoItem>>(fs);
            }
        }
        catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
        {
            // 防止文件无法找到
            return null;
        }
    }
}