using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// 選択した画像を指定したフォルダへ移動させる機能。
    /// </summary>
    public class MoveImageToSelectedFolder
    {
        public void Do(Grid imageArea, ListBox subFolderesList, List<CheckBox> checkboxes, List<FileStream> filestreams)
        {
            // チェックボックスがチェックされているか確認する。
            // checkboxesを2回ループすることになるので効率が悪いが、とりあえず他に方法が思いつかない。
            bool isCheckedExists = false;
            foreach (CheckBox cb in checkboxes)
            {
                if (cb.IsChecked == true)
                {
                    isCheckedExists = true;
                    break;
                }
            }

            if (isCheckedExists == true)
            {
                // 表示中の画像のストリームを閉じる（閉じなければファイルを移動できない。）
                foreach (FileStream fs in filestreams)
                {
                    fs.Close();
                }

                foreach (CheckBox cb in checkboxes)
                {
                    if (cb.IsChecked == true)
                    {
                        isCheckedExists = true;
                        File.Move($"./{cb.Content.ToString()}", $"./{subFolderesList.SelectedItem}/{cb.Content.ToString()}");
                    }
                }

                imageArea.Children.Clear();
            }
            else
            {
                MessageBox.Show("移動対象のファイルが選択されていません。\n移動対象ファイルをチェックし、再度［フォルダへ移動］ボタンを\n押下してください。", "移動対象ファイルを選択してください", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
