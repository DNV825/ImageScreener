using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// 新規フォルダ追加機能。
    /// </summary>
    public class MoveImageToSelectedFolder
    {
        public void Do(ListBox subFolderList)
        {
            if(subFolderList.SelectedItems.Count > 0)
            {
                MessageBox.Show($".\\{subFolderList.SelectedItem}", "スタブ：リスト選択項目あり");
            }
            else
            {
                MessageBox.Show("", "スタブ：リスト選択項目なし");
            }
        }
    }
}
