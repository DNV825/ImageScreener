using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
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
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.mpg", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.mpeg", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.mp4", SearchOption.TopDirectoryOnly).GetLength(0);
            imagesCount += Directory.GetFiles(currentDirectoryPath, "*.webm", SearchOption.TopDirectoryOnly).GetLength(0);

            return imagesCount;
        }

        /**
        * ImageAreaにカレントディレクトリの画像ファイルを描画する。
        */
        public async Task Do(
            Grid imageArea,
            TextBlock imageFilesCount,
            List<CheckBox> checkboxes,
            List<Stream> streams,
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
            foreach (Stream fs in streams)
            {
                fs.Close();
                fs.Dispose();
            }
            streams.Clear();

            // ImageAreaをクリア（初期化）する。
            imageArea.Children.Clear();

            // ターゲット画像の描画を開始。
            string[] files = Directory.GetFiles(currentDirectoryPath, "*", SearchOption.TopDirectoryOnly);
            int targetImagesCount = 0;                      // ターゲット行に描画中のターゲット画像の件数。
            int rowCount = 0;                               // Grid.Rowのカウント。
            StackPanel currentTargetRow = new StackPanel(); // 現在のターゲット行を指し示す変数。
            const int MAX_DRAWED_IMAGES_COUNT = 100;        // 描画するターゲット画像の最大件数。
            int drawedImagesCount = 0;                      // 描画したターゲット画像の件数。
            bool isDrawTarget = false;                      // ファイルが描画対象であるか。true:描画対象である、false:描画対象でない。
            bool isVideoFile = false;                       // ファイルが動画ファイルであるか。true:動画ファイルである、false:動画ファイルでない。

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
                        isDrawTarget = true;
                        isVideoFile = false;
                        break;
                    case ".mpg":
                    case ".mpeg":
                    case ".mp4":
                    case ".webm":
                        isDrawTarget = true;
                        isVideoFile = true;
                        break;
                    default:
                        isDrawTarget = false;
                        isVideoFile = false;
                        break;
                }

                if (isDrawTarget)
                {
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
                    Stream imageStream;

                    if (isVideoFile)
                    {
                        // https://www.codeproject.com/Tips/496864/Getting-Thumbnail-from-Video-using-MediaPlayer-Cla
                        // https://csharp.hotexamples.com/jp/examples/System.Windows.Media.Imaging/BitmapEncoder/Save/php-bitmapencoder-save-method-examples.html
                        // https://water2litter.net/rye/post/c_graphic_save/
                        MediaPlayer player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };
                        player.Open(new Uri(imagePath));
                        player.Pause();
                        player.Position = TimeSpan.FromSeconds(0);
                        //We need to give MediaPlayer some time to load. 
                        //The efficiency of the MediaPlayer depends                 
                        //upon the capabilities of the machine it is running on and 
                        //would be different from time to time
                        // 1.5秒くらい待てば大丈夫そうなので、とりあえず1.5秒待ってみる。
                        // System.Threading.Thread.Sleep(waitTime * 1000);
                        System.Threading.Thread.Sleep(1500);

                        //120 = thumbnail width, 90 = thumbnail height and 96x96 = horizontal x vertical DPI
                        //In an real application, you would not probably use hard coded values!
                        RenderTargetBitmap rtb = new RenderTargetBitmap(120, 90, 96, 96, PixelFormats.Pbgra32);
                        DrawingVisual dv = new DrawingVisual();
                        using (DrawingContext dc = dv.RenderOpen())
                        {
                            dc.DrawVideo(player, new Rect(0, 0, 120, 90));
                        }
                        rtb.Render(dv);
                        Duration duration = player.NaturalDuration;
                        int videoLength = 0;
                        if (duration.HasTimeSpan)
                        {
                            videoLength = (int)duration.TimeSpan.TotalSeconds;
                        }
                        BitmapFrame frame = BitmapFrame.Create(rtb).GetCurrentValueAsFrozen() as BitmapFrame;
                        // BitmapEncoder encoder = new JpegBitmapEncoder();
                        BitmapEncoder encoder = new PngBitmapEncoder(); // Jpegだとなぜか認識されなかったのでpngにした。
                        encoder.Frames.Add(frame as BitmapFrame);

                        imageStream = new MemoryStream();
                        encoder.Save(imageStream);
                        streams.Add(imageStream);

                        //Here we have the thumbnail in the MemoryStream!
                        player.Close();
                    }
                    else
                    {
                        imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        streams.Add(imageStream); // 描画時点ではFileStreamを開いたままにする。［ファイルを移動］ボタン押下時に、すべてのストリームを閉じる。
                    }

                    bimg.BeginInit();
                    bimg.StreamSource = imageStream;
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
