using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace ImageScreener
{
    /// <summary>
    /// 画像描画機能。
    /// </summary>
    public class DrawImageArea
    {
        /**
        * 描画した画像クリック時の動作。
        */
        private void Image_Click(object sender, RoutedEventArgs eventArgs)
        {
            Image img = (Image)sender;
            string imagePath = img.Tag.ToString();

            // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.createnowindow?redirectedfrom=MSDN&view=net-5.0#System_Diagnostics_ProcessStartInfo_CreateNoWindow
            // https://stackoverflow.com/questions/5377423/hide-console-window-from-process-start-c-sharp
            Process cmd = new Process();
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = $"/c {imagePath}";
            cmd.Start();
        }

        /**
        * カレントディレクトリに存在するターゲット画像の枚数を返却する。
        */
        private int GetImagesCount(string currentDirectoryPath)
        {
            int imagesCount = 0;

            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.jpg", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.jpeg", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.png", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.gif", SearchOption.TopDirectoryOnly).GetLength(0);

            return imagesCount;
        }

        /**
        * ImageAreaにカレントディレクトリの画像ファイルを描画する。
        */
        public async Task Do(
            Grid imageArea,
            TextBlock imageFilesCount,
            List<CheckBox> checkboxes,
            List<FileStream> filestreams,
            string currentDirectoryPath,
            Grid displayArea,
            ProgressBar drawImageProgress,
            Window mainWindow)
        {
            // カレントディレクトリに存在する画像の件数を表示する。
            imageFilesCount.Text = this.GetImagesCount(currentDirectoryPath).ToString();

            // チェックボックスリストのクリア（画像を移動したのでチェックボックスリストを作り直す必要がある。）
            checkboxes.Clear();

            // 開いているファイルのFileStreamを閉じ、リストをクリアする。
            foreach (FileStream fs in filestreams)
            {
                fs.Close();
                fs.Dispose();
            }
            filestreams.Clear();

            // ImageAreaをクリア（初期化）する。
            imageArea.Children.Clear();

            // ターゲット画像の描画を開始。
            string[] files = Directory.GetFiles(currentDirectoryPath, "*", SearchOption.TopDirectoryOnly);
            int targetImagesCount = 0;                      // ターゲット行に描画中のターゲット画像の件数。
            int rowCount = 0;                               // Grid.Rowのカウント。
            StackPanel currentTargetRow = new StackPanel(); // 現在のターゲット行を指し示す変数。
            const int MAX_DRAWED_IMAGES_COUNT = 100;        // 描画するターゲット画像の最大件数。
            int drawedImagesCount = 0;                      // 描画したターゲット画像の件数。

            // 進捗バーを表示する。
            drawImageProgress.Maximum = (files.Length >= 100) ? MAX_DRAWED_IMAGES_COUNT : files.Length;
            drawImageProgress.Value = 0;
            drawImageProgress.Visibility = Visibility.Visible;

            foreach(string imageFileName in files)
            {
                // ファイルが画像ファイル、もしくは動画ファイルであれば描画対象とする。
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

                        chk.Width = 100;
                        chk.Height = 15;
                        chk.Margin = new Thickness(5);
                        chk.Content = Path.GetFullPath(imageFileName).Replace($"{currentDirectoryPath}","").Replace("\\",""); // チェックボックスにはファイル名だけを表示する。
                        targetImage.Children.Add(chk);
                        checkboxes.Add(chk);

                        // https://ameblo.jp/dairy789/entry-11243771768.html
                        // https://ameblo.jp/dairy789/entry-11304440424.html
                        // https://stackoverflow.com/questions/6588974/get-imagesource-from-memorystream-in-c-sharp-wpf
                        // https://dobon.net/vb/dotnet/graphics/drawpicture2.html
                        Image img = new Image();
                        BitmapImage bimg = new BitmapImage();
                        string imagePath = Path.GetFullPath(imageFileName);
                        FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        filestreams.Add(fs); // 描画時点ではFileStreamを開いたままにする。［ファイルを移動］ボタン押下時に、すべてのストリームを閉じる。

                        bimg.BeginInit();
                        bimg.StreamSource = fs;
                        bimg.DecodePixelWidth = 100;
                        bimg.DecodePixelHeight = 100;
                        bimg.EndInit();

                        img.MaxWidth = 100;
                        img.MaxHeight = 100;
                        img.MinWidth = 100;
                        img.MinHeight = 100;
                        img.Margin = new Thickness(5);
                        img.Source = bimg;
                        img.MouseDown += new MouseButtonEventHandler(Image_Click);
                        img.Tag = imagePath;
                        // img.ToolTip = imagePath;
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
                    // case ".mpg":
                    // case ".mpeg":
                    // case ".mp4":
                    // case ".webm":
                    // case ".webp":
                    //     break;
                    default:
                        break;
                }

                // 進捗バーを更新する。
                await Task.Run(() => 
                {
                    mainWindow.Dispatcher.Invoke((Action)(() =>
                    {
                       drawImageProgress.Value++;
                    }));
                });

                // 画像を100件描画した場合、描画処理を終了する（foreachループを抜ける。）
                if (drawedImagesCount == MAX_DRAWED_IMAGES_COUNT) {
                    break;
                }
            }

            // 進捗バーを非表示にする。
            drawImageProgress.Visibility = Visibility.Hidden;
        }
    }
}
