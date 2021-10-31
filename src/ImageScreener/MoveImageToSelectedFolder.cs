using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// 新規フォルダ追加機能。
    /// </summary>
    public class MoveImageToSelectedFolder
    {
        public void Do(ListBox subFolderesList, List<CheckBox> checkboxes, ListBox SubFolderesList)
        {
            if(subFolderesList.SelectedItems.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach(CheckBox cb in checkboxes)
                {
                    if (cb.IsChecked == true)
                    {
                        sb.AppendLine(cb.Content.ToString());
                    }
                }

                MessageBox.Show($".\\{subFolderesList.SelectedItem}\n{sb.ToString()}", "スタブ：リスト選択項目あり");
            }
            else
            {
                MessageBox.Show("", "スタブ：リスト選択項目なし");
            }
        }
    }
}
