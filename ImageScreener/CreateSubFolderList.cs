using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageScreener
{
    /// <summary>
    /// サブフォルダのリストを生成する機能。
    /// </summary>
    public class CreateSubFolderesList
    {
        public void Do(ListBox subFolderesList, string currentDirectoryPath)
        {
            string[] dirs = Directory.GetDirectories(currentDirectoryPath, "*", SearchOption.TopDirectoryOnly);

            if(subFolderesList.Items.Count > 0)
            {
                subFolderesList.Items.Clear();
            }

            subFolderesList.Items.Add("..");

            foreach(string subFolderName in dirs)
            {
                FileAttributes fa = File.GetAttributes(subFolderName);

                if ((fa & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                {
                    // ショートカットの場合は何もしない。
                }
                else
                {
                    subFolderesList.Items.Add(subFolderName.Replace($"{currentDirectoryPath}", "").Replace("\\",""));
                }
            }
        }
    }
}
