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
        * グリッド「ImageArea」ロード時の動作。
        */
        private void ImageArea_Loaded(object sender, RoutedEventArgs eventArgs)
        {
            DrawImageArea dia = new DrawImageArea();
            dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams);
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
        private void OnKeyDownHander(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.Key == Key.Return)
            {
                CreateNewFolder cnf = new CreateNewFolder();
                cnf.Do(NewFolderName);

                CreateSubFolderesList csfl = new CreateSubFolderesList();
                csfl.Do(SubFolderesList);
            }
        }

        /**
        * ［フォルダ作成］ボタン押下時の動作。
        */
        private void CreateNewFolder_Click(object sender, RoutedEventArgs eventArgs)
        {
            CreateNewFolder cnf = new CreateNewFolder();
            cnf.Do(NewFolderName);

            CreateSubFolderesList csfl = new CreateSubFolderesList();
            csfl.Do(SubFolderesList);
        }

        /**
        * リストボックス「SubFolderesList」ロード時の動作。
        */
        private void SubFolderesList_Loaded(object sender, RoutedEventArgs eventArgs)
        {
            CreateSubFolderesList csfl = new CreateSubFolderesList();
            csfl.Do(SubFolderesList);
        }

        /**
        * ［フォルダへ移動］ボタン押下時の動作。
        */
        private void MoveImageToSelectedFolder_Click(object sender, RoutedEventArgs eventArgs)
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
                    misf.Do(ImageArea, SubFolderesList, _checkboxes, _filestreams);

                    DrawImageArea dia = new DrawImageArea();
                    dia.Do(ImageArea, ImageFilesCount, _checkboxes, _filestreams);
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
