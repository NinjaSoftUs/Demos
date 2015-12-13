using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Windows;
using System.Windows.Controls;
using NinjaSoft.DirctoryStatsModule.ViewModel;

namespace NinjaSoft.DirctoryStatsModule.Views
{
    [Export]
    public partial class DirStatusView : UserControl
    {
        private DirctoryStatusViewModel ViewModel => this.DataContext as DirctoryStatusViewModel;

        [ImportingConstructor]
        public DirStatusView()
        {
            InitializeComponent();
        }


        private void BtnPath3_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true, Multiselect = false };
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ViewModel.Path3 = dialog.FileName;
            }
        }

        private void BtnPath2_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true, Multiselect = false };
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ViewModel.Path2 = dialog.FileName;
             
            }
        }

        private void BtnPath1_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true, Multiselect = false };
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ViewModel.Path1 = dialog.FileName;
            }
        }
    }
}