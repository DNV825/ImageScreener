using System;
using System.Threading.Tasks;
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
                // https://divakk.co.jp/aoyagi/csharp_tips_using.html
                foreach (FileStream fs in filestreams)
                {
                    fs.Close();
                    fs.Dispose();
                }

                // Task.Delay(200);  // ストリームを閉じてもファイルへの接続が維持されているようなので、試しに200ms待機して完全なクローズを待ってみる。

                foreach (CheckBox cb in checkboxes)
                {
                    if (cb.IsChecked == true)
                    {
                        // 移動対象のターゲット画像が存在していることを記録する。
                        isCheckedExists = true;

                        // 同名のファイルが移動先に存在している場合、ファイル名の末尾に"="を付与して移動する。
                        // 例えば、"abdc.jpg"は"abcd=.jpg"に改名とする。
                        string pathFrom = $"./{cb.Content.ToString()}";
                        string pathCanditate = $"./{subFolderesList.SelectedItem}/{cb.Content.ToString()}";
                        string pathTo = "";
                        
                        bool checkingPathCanditate = true;
                        while (checkingPathCanditate == true)
                        {
                            if (File.Exists(pathCanditate) == true)
                            {
                                pathCanditate = $"./{subFolderesList.SelectedItem}/{Path.GetFileNameWithoutExtension(pathCanditate)}={Path.GetExtension(pathCanditate)}";
                            }
                            else
                            {
                                checkingPathCanditate = false;
                            }
                        }

                        pathTo = pathCanditate;

                        File.Move(pathFrom, pathTo);
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
