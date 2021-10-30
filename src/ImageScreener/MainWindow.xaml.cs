using System;
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
        * テキストボックス「NewFolderName」でエンターキーを押下した場合の動作。
        * 動作内容は［フォルダ作成］ボタン押下時と同様。
        */
        private void OnKeyDownHander(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.Key == Key.Return)
            {
                CreateNewFolder cnf = new CreateNewFolder();
                cnf.Do(NewFolderName);

                CreateSubFolderList csfl = new CreateSubFolderList();
                csfl.Do(SubFolderList);
            }
        }

        /**
        * ［フォルダ作成］ボタン押下時の動作。
        */
        private void CreateNewFolder_Click(object sender, RoutedEventArgs eventArgs)
        {
            CreateNewFolder cnf = new CreateNewFolder();
            cnf.Do(NewFolderName);

            CreateSubFolderList csfl = new CreateSubFolderList();
            csfl.Do(SubFolderList);
        }

        /**
        * リストボックス「SubFolderList」ロード時の動作。
        */
        private void SubFolderList_Loaded(object sender, RoutedEventArgs eventArgs)
        {
            CreateSubFolderList csfl = new CreateSubFolderList();
            csfl.Do(SubFolderList);
        }

        /**
        * ［フォルダへ移動］ボタン押下時の動作。
        */
        private void MoveImageToSelectedFolder_Click(object sender, RoutedEventArgs eventArgs)
        {
            MoveImageToSelectedFolder misf = new MoveImageToSelectedFolder();
            misf.Do(SubFolderList);
        }

    }
}
