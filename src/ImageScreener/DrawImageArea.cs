﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageScreener
{
    /// <summary>
    /// 画像描画機能。
    /// </summary>
    public class DrawImageArea
    {
        /**
        * カレントディレクトリに存在するターゲット画像の枚数を返却する。
        */
        private int GetImagesCount()
        {
            int imagesCount = 0;

            imagesCount += Directory.GetFiles(".", "*.jpg", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(".", "*.jpeg", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(".", "*.png", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(".", "*.gif", SearchOption.TopDirectoryOnly).GetLength(0);

            return imagesCount;
        }
        public void Do(Grid imageArea, TextBlock imageFilesCount, List<CheckBox> chexboxes)
        {
            // カレントディレクトリに存在する画像の件数を表示する。
            imageFilesCount.Text = this.GetImagesCount().ToString();

            // チェックボックスのクリア（画像を移動したのでチェックボックスリストを作り直す必要がある。）
            chexboxes.Clear();

            // ターゲット画像の描画を開始。
            string[] files = Directory.GetFiles(".", "*", SearchOption.TopDirectoryOnly);

            if(imageArea.Children.Count > 0)
            {
                imageArea.Children.Clear();
            }

            int targetImagesCount = 0;                      // ターゲット行に描画中のターゲット画像の件数。
            int rowCount = 0;                               // Grid.Rowのカウント。
            StackPanel currentTargetRow = new StackPanel(); // 現在のターゲット行を指し示す変数。
            const int MAX_DRAWED_IMAGES_COUNT = 100;        // 描画するターゲット画像の最大件数。
            int drawedImagesCount = 0;                      // 描画したターゲット画像の件数。

            foreach(string imageFileName in files)
            {
                // ファイルが画像ファイルであれば描画対象とする。
                switch(Path.GetExtension(imageFileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                        // 初めてターゲット画像を表示する場合、もしくは現在のターゲット行に5つのターゲット画像を表示した後の場合、
                        // 新しくターゲット行を追加する。
                        if (targetImagesCount == 0)
                        {
                            currentTargetRow = new StackPanel();
                            currentTargetRow.Orientation = Orientation.Horizontal;
                            currentTargetRow.SetValue(Grid.RowProperty, rowCount++);
                            imageArea.Children.Add(currentTargetRow);
                        }

                        // チェックボックスとイメージを縦並びのStackPanelとして追加する。
                        StackPanel targetImage = new StackPanel();
                        CheckBox chk = new CheckBox();
                        Image img = new Image();

                        chk.Width = 100;
                        chk.Height = 15;
                        chk.Margin = new Thickness(5);
                        chk.Content = imageFileName.Replace(".\\","");
                        targetImage.Children.Add(chk);
                        chexboxes.Add(chk);

                        img.MaxWidth = 100;
                        img.MaxHeight = 100;
                        img.MinWidth = 100;
                        img.MinHeight = 100;
                        img.Margin = new Thickness(5);
                        img.Source = new BitmapImage(new Uri(Path.GetFullPath(imageFileName).Replace("\\","/")));
                        targetImage.Children.Add(img);

                        currentTargetRow.Children.Add(targetImage);
                        targetImagesCount++; // ターゲット画像をセットしたのでカウントを次に進める。
                        drawedImagesCount++;

                        // ターゲット画像を5枚表示した場合、カウントを0に戻す。
                        if (targetImagesCount == 5)
                        {
                            targetImagesCount = 0;
                        }
                        break;
                    default:
                        break;
                }

                // 画像を100件描画した場合、描画処理を終了する（foreachループを抜ける。）
                if (drawedImagesCount == MAX_DRAWED_IMAGES_COUNT) {
                    break;
                }
            }
        }
    }
}