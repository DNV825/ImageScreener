using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// 新規フォルダ追加機能。
    /// </summary>
    public class CreateNewFolder
    {
        public void Do(TextBox newFolderName, string currentDirectoryPath)
        {
            try
            {
                Directory.CreateDirectory($"{currentDirectoryPath}/{newFolderName.Text}");
                newFolderName.Text = "";
            }
            catch (Exception e)
            {
                MessageBox.Show($"指定した名前ではフォルダを作成できません。別の名前を指定してください。\n---- 以下、エラー内容 ----\n{e.Message}", "新規フォルダを作成できませんでした", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
