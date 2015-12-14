using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using NinjaSoft.DirctoryStatsModule.ViewModel;

namespace NinjaSoft.DirctoryStatsModule.Views
{
    [Export]
    public partial class DirStatusView : UserControl
    {
        private DispatcherTimer _searchTextBoxTimer;
        private TimeSpan _timeSpanDelay = TimeSpan.FromSeconds(20);
        private bool _isStartingFlage;


        private DirctoryStatusViewModel ViewModel => this.DataContext as DirctoryStatusViewModel;

        [ImportingConstructor]
        public DirStatusView()
        {
            InitializeComponent();
        }


        private void BtnPath3_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog {IsFolderPicker = true, Multiselect = false};
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ViewModel.Path3 = dialog.FileName;
            }
        }

        private void BtnPath2_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog {IsFolderPicker = true, Multiselect = false};
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ViewModel.Path2 = dialog.FileName;

            }
        }

        private void BtnPath1_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog {IsFolderPicker = true, Multiselect = false};
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                ViewModel.Path1 = dialog.FileName;
            }
        }


        private void TextBoxPath_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text.Length > 2)
            {
                switch (textBox.Name)
                {
                    case "TextBoxPath1":
                        ViewModel.Path1 = TextBoxPath1.Text;
                        break;
                    case "TextBoxPath2":
                        ViewModel.Path2 = TextBoxPath2.Text;
                        break;
                    case "TextBoxPath3":
                        ViewModel.Path3 = TextBoxPath3.Text;
                        break;

                }
            }
        }
    }
}
