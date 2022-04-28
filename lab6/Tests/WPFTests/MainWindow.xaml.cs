using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTests
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private CancellationTokenSource _ReadingFileCancellation;
        private async void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            await Task.Yield();

            var dialog = new OpenFileDialog
            {
                Title = "Выбор файла для чтения",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != true)
            {
                return;            
            }

            StartAction.IsEnabled = false;
            CancelAction.IsEnabled = true;

            _ReadingFileCancellation = new CancellationTokenSource();

            var cancel = _ReadingFileCancellation.Token;
            IProgress<double> progress = new Progress<double>(p => ProgressInfo.Value = p);
            try
            {
                var count = await GetWordsCountAsync(dialog.FileName, progress, cancel).ConfigureAwait(true);
                Result.Text = $"Число слов {count}";
            }
            catch
            {
                Debug.WriteLine("Операция чтения файла была отменена");                
                progress.Report(0);
            }
            StartAction.IsEnabled = true;
            CancelAction.IsEnabled = false;
        }

        private static async Task<int> GetWordsCountAsync(string FileName, IProgress<double> Progress = null, CancellationToken Cancel = default)
        {
            await Task.Yield();

            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Cancel.ThrowIfCancellationRequested();

            using (var reader = new StreamReader(FileName))
            {
                while (!reader.EndOfStream)
                {
                    Cancel.ThrowIfCancellationRequested();
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var words = line.Split(" ");
                    await Task.Delay(1);

                    foreach (var word in words)
                    {
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else
                            dict.Add(word, 1);
                    }

                    Progress?.Report(reader.BaseStream.Position / (double)reader.BaseStream.Length);
                }
            }
                        
            return dict.Count;
        }

        private void OnCloseFileClick(object sender, RoutedEventArgs e)
        {
            _ReadingFileCancellation?.Cancel();
            StartAction.IsEnabled = true;
            CancelAction.IsEnabled = false;
        }
    }
}
