﻿using System;
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
        public void Do(
            Grid imageArea,
            ListBox subFolderesList,
            List<CheckBox> checkboxes,
            List<Stream> streams,
            string currentDirectoryPath)
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
                foreach (Stream fs in streams)
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
                        string fileName = cb.Content.ToString();
                        string selectedFolder = subFolderesList.SelectedItem.ToString();

                        string folderPathTo = (selectedFolder == "..") ? Path.GetFullPath($"{currentDirectoryPath}\\..") : $"{currentDirectoryPath}\\{selectedFolder}";
                        string filePathFrom = $"{currentDirectoryPath}\\{fileName}";
                        string filePathCanditate = $"{folderPathTo}\\{fileName}";
                        string filePathTo = "";
                        
                        bool checkingPathCanditate = true;
                        while (checkingPathCanditate == true)
                        {
                            if (File.Exists(filePathCanditate) == true)
                            {
                                filePathCanditate = $"{folderPathTo}\\{Path.GetFileNameWithoutExtension(filePathCanditate)}#{Path.GetExtension(filePathCanditate)}";
                            }
                            else
                            {
                                checkingPathCanditate = false;
                            }
                        }

                        filePathTo = filePathCanditate;

                        File.Move(filePathFrom, filePathTo);
                    }
                }
            }
            else
            {
                MessageBox.Show("移動対象のファイルが選択されていません。\n移動対象ファイルをチェックし、再度［フォルダへ移動］ボタンを\n押下してください。", "移動対象ファイルを選択してください", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
