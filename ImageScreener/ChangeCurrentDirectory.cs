using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// カレントディレクトリを移動する機能。
    /// </summary>
    public class ChangeCurrentDirectory
    {
        public string GetNewCurrentDirectoryPath(ListBox subFolderesList, string currentDirectoryPath)
        {
            string selectedPath = subFolderesList.SelectedItem.ToString();
            
            return Path.GetFullPath($"{currentDirectoryPath}\\{selectedPath}");
        }
    }
}
