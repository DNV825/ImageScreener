using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// 新規フォルダ追加機能。
    /// </summary>
    public class CreateSubFolderList
    {
        public void Do(ListBox subFolderList)
        {
            String[] dirs = Directory.GetDirectories(".", "*", SearchOption.TopDirectoryOnly);

            if(subFolderList.Items.Count > 0)
            {
                subFolderList.Items.Clear();
            }

            foreach(String subFolderName in dirs)
            {
                subFolderList.Items.Add(subFolderName.Replace(".\\", ""));
            }
        }
    }
}
