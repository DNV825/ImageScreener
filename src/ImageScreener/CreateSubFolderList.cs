using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// 新規フォルダ追加機能。
    /// </summary>
    public class CreateSubFolderesList
    {
        public void Do(ListBox subFolderesList)
        {
            string[] dirs = Directory.GetDirectories(".", "*", SearchOption.TopDirectoryOnly);

            if(subFolderesList.Items.Count > 0)
            {
                subFolderesList.Items.Clear();
            }

            foreach(string subFolderName in dirs)
            {
                subFolderesList.Items.Add(subFolderName.Replace(".\\", ""));
            }
        }
    }
}
