using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageScreener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _currentDirectoryPath = System.IO.Path.GetFullPath(".");
        }

        /**
        * チェックボックスの情報を保存するリスト。
        */
        List<CheckBox> _checkboxes = new List<CheckBox>();

        /**
        * イメージをオープンするFileStreamへの参照を保存するリスト。
        */
        List<FileStream> _filestreams = new List<FileStream>();

        /**
        * 画像表示中のカレントディレクトリのパス。
        */
        string _currentDirectoryPath = ".";

        /**
        * ウィンドウのレンダリング完了時の動作。
        */
        private async void Window_ContentRendered(object sender, EventArgs eventArgs)
        {
            DrawImageArea dia = new DrawImageArea();
            await dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams, _currentDirectoryPath, DisplayArea, DrawImageProgress, this);

            CreateSubFolderesList csfl = new CreateSubFolderesList();
            csfl.Do(SubFolderesList, _currentDirectoryPath);
        }

        /**
        * グリッド「ImageArea」ロード時の動作。
        * ウィンドウのレンダリング完了時に描画するようにしたので、中身は空っぽ。
        */
        private void ImageArea_Loaded(object sender, RoutedEventArgs eventArgs)
        {
            // DrawImageArea dia = new DrawImageArea();
            // dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams, _currentDirectoryPath, DisplayArea, DrawImageProgress);
        }

        /**
        * F5を押下した場合の動作。
        * 表示画像とフォルダリストを再描画する。
        * 動作内容は『グリッド「ImageArea」ロード時の動作』と『リストボックス「SubFolderesList」ロード時の動作。』と同じ。
        */
        private async void Window_OnKeyDownHander(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.Key == Key.F5)
            {
                DrawImageArea dia = new DrawImageArea();
                await dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams, _currentDirectoryPath, DisplayArea, DrawImageProgress, this);

                CreateSubFolderesList csfl = new CreateSubFolderesList();
                csfl.Do(SubFolderesList, _currentDirectoryPath);
            }
        }

        /**
        * ［全チェック選択］ボタン押下時の動作。
        */
        private void CheckOnAll_Click(object sender, RoutedEventArgs eventArgs)
        {
            foreach (CheckBox cb in _checkboxes)
            {
                cb.IsChecked = true;
            }
        }

        /**
        * ［全チェック解除］ボタン押下時の動作。
        */
        private void CheckOffAll_Click(object sender, RoutedEventArgs eventArgs)
        {
            foreach (CheckBox cb in _checkboxes)
            {
                cb.IsChecked = false;
            }
        }

        /**
        * テキストボックス「NewFolderName」でエンターキーを押下した場合の動作。
        * 動作内容は［フォルダ作成］ボタン押下時と同様。
        */
        private void NewFolderName_OnKeyDownHander(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.Key == Key.Return)
            {
                CreateNewFolder cnf = new CreateNewFolder();
                cnf.Do(NewFolderName, _currentDirectoryPath);

                CreateSubFolderesList csfl = new CreateSubFolderesList();
                csfl.Do(SubFolderesList, _currentDirectoryPath);
            }
        }

        /**
        * ［フォルダ作成］ボタン押下時の動作。
        */
        private void CreateNewFolder_Click(object sender, RoutedEventArgs eventArgs)
        {
            CreateNewFolder cnf = new CreateNewFolder();
            cnf.Do(NewFolderName, _currentDirectoryPath);

            CreateSubFolderesList csfl = new CreateSubFolderesList();
            csfl.Do(SubFolderesList, _currentDirectoryPath);
        }

        /**
        * リストボックス「SubFolderesList」ロード時の動作。
        * ウィンドウのレンダリング完了時に描画するようにしたので、中身は空っぽ。
        */
        private void SubFolderesList_Loaded(object sender, RoutedEventArgs eventArgs)
        {
            // CreateSubFolderesList csfl = new CreateSubFolderesList();
            // csfl.Do(SubFolderesList, _currentDirectoryPath);
        }

        /**
        * リストボックス「SubFolderesList」のアイテムをダブルクリックした時の動作。
        */
        private async void SubFolderesList_MouseDoubleClick(object sender, RoutedEventArgs eventArgs)
        {
            ChangeCurrentDirectory ccd = new ChangeCurrentDirectory();
            _currentDirectoryPath = ccd.GetNewCurrentDirectoryPath(SubFolderesList, _currentDirectoryPath);

            CreateSubFolderesList csfl = new CreateSubFolderesList();
            csfl.Do(SubFolderesList, _currentDirectoryPath);

            DrawImageArea dia = new DrawImageArea();
            await dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams, _currentDirectoryPath, DisplayArea, DrawImageProgress, this);

            ImageAreaScrollViewer.ScrollToTop();
        }

        /**
        * ［フォルダへ移動］ボタン押下時の動作。
        */
        private async void MoveImageToSelectedFolder_Click(object sender, RoutedEventArgs eventArgs)
        {
            // チェックボックスがチェックされているか確認する。
            // checkboxesを2回ループすることになるので効率が悪いが、とりあえず他に方法が思いつかない。
            bool isCheckedExists = false;
            foreach (CheckBox cb in _checkboxes)
            {
                if (cb.IsChecked == true)
                {
                    isCheckedExists = true;
                    break;
                }
            }

            // チェックボックスがチェックされている場合。
            if (isCheckedExists == true)
            {
                // 移動先フォルダが選択されている場合、選択したファイルを移動し、
                // ターゲット画像を再描画する。
                if(SubFolderesList.SelectedItems.Count == 1)
                {
                    MoveImageToSelectedFolder misf = new MoveImageToSelectedFolder();
                    misf.Do(ImageArea, SubFolderesList, _checkboxes, _filestreams, _currentDirectoryPath);

                    DrawImageArea dia = new DrawImageArea();
                    await dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams, _currentDirectoryPath, DisplayArea, DrawImageProgress, this);
                }
                else
                {
                    MessageBox.Show("移動先フォルダが選択されていません。\n移動先フォルダを選択し、再度［フォルダへ移動］ボタンを\n押下してください。", "移動先フォルダを選択してください", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("移動対象のファイルが選択されていません。\n移動対象ファイルをチェックし、再度［フォルダへ移動］ボタンを\n押下してください。", "移動対象ファイルを選択してください", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
